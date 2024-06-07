using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using _3_Data;
using _3_Data.Contexts;
using _3_Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace _2_Domain;

public class AuthenticationService : IAuthenticationService
{
    //  @Dependencies
    private readonly IUserAthenticationData _userAuthenticationData;
    private readonly IConfiguration _configuration;
    
    //  @Constructor
    public AuthenticationService(
        IUserAthenticationData userAuthenticationDomain, 
        IConfiguration configuration
    )
    {
        this._userAuthenticationData = userAuthenticationDomain;
        this._configuration = configuration;
    }
    
    //  @Methods
    private string GenerateToken(int userId)
    {
        var key = this._configuration.GetValue<string>("JwtSettings:key");
        var keyBytes = Encoding.ASCII.GetBytes(key);

        var claims = new ClaimsIdentity();
        claims.AddClaim(
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.ToString()
            )
        );

        var credentialsToken = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256Signature
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddSeconds(45),
            SigningCredentials = credentialsToken
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

        string createdToken = tokenHandler.WriteToken(tokenConfig);
        return createdToken;
    }
    private string GenerateRefreshToken()
    {
        var byteArray = new byte[64];
        var refreshToken = "";
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(byteArray);
            refreshToken = Convert.ToBase64String(byteArray);
        }

        return refreshToken;
    }
    private async Task<AuthenticationResults> SaveRefreshTokenRecord(
        int userId,
        string token,
        string refreshToken
    )
    {
        var refreshTokenRecord = new RefreshTokenRecord
        {
            UserId = userId,
            Token = token,
            RefreshToken = refreshToken,
            Expiration = DateTime.UtcNow.AddMinutes(2), //  !Change to a real scenario value.
            Created = DateTime.UtcNow
        };
        
        await this._userAuthenticationData.SaveRefreshTokenAsync(refreshTokenRecord);
        
        return new AuthenticationResults
        {
            token = token,
            refreshToken = refreshToken,
            result = true,
            message = "Refresh Token saved!"
        };
    }
    
    //  @Implementations
    public async Task<AuthenticationResults> GetToken(UserCredentials userCredentials)
    {
        var user = await this._userAuthenticationData.GetUserByCredentialsAsync(userCredentials);

        if (user == null)
        {
            return await Task.FromResult<AuthenticationResults>(null);
        }
        
        string createdToken = GenerateToken(user.Id);
        string createdRefreshToken = GenerateRefreshToken();
        
        return await this.SaveRefreshTokenRecord(
            user.Id,
            createdToken,
            createdRefreshToken
        );
    }
    public async Task<AuthenticationResults> GetRefreshToken(
        RefreshTokenModel refreshToken
    )
    {
        var foundRefreshToken = await this._userAuthenticationData.FindExistingRefreshTokenAsync(refreshToken);

        if (foundRefreshToken == null)
        {
            return new AuthenticationResults()
            {
                result = false,
                message = "Invalid Refresh Token"
            };
        }
        
        var createdRefreshToken = GenerateRefreshToken();
        var crateToken = GenerateToken(refreshToken.UserId);

        return await this.SaveRefreshTokenRecord(refreshToken.UserId, crateToken, createdRefreshToken);
    }
}