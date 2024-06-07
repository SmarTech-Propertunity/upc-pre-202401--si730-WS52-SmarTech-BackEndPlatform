
using _3_Data.Models;

namespace _2_Domain;

public interface IAuthenticationService
{
    public Task<AuthenticationResults> GetToken(UserCredentials credentials);
    
    public Task<AuthenticationResults> GetRefreshToken(RefreshTokenModel refreshToken);
}