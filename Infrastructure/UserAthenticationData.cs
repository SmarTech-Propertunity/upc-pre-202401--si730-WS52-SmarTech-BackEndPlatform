using _3_Data.Contexts;
using _3_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3_Data;

public class UserAthenticationData : IUserAthenticationData
{
    //  @Dependencies
    private readonly PropertunityDataCenterContext _propertunityDataCenterContext;

    //  @Constructor
    public UserAthenticationData(
        PropertunityDataCenterContext propertunityDataCenterContext
    )
    {
        this._propertunityDataCenterContext = propertunityDataCenterContext;
    }
    
    //  @Methods
    public async Task<User?> GetUserByCredentialsAsync(UserCredentials userCredentials)
    {
        var result = this._propertunityDataCenterContext.Users.
            FirstOrDefaultAsync(user =>
            user._UserCredentials.Email == userCredentials.Email &&
            user._UserCredentials.Password == userCredentials.Password
        );

        return await result;
    }
    public async Task<RefreshTokenRecord?> FindExistingRefreshTokenAsync(RefreshTokenModel refreshToken)
    {
        var result = await this._propertunityDataCenterContext.RefreshTokenRecords.
            FirstOrDefaultAsync(r =>
                r.Token == refreshToken.ExpiredToken &&
                r.RefreshToken == refreshToken.RefreshToken &&
                r.UserId == refreshToken.UserId
            );

        return result;
    }
    public async Task<int> SaveRefreshTokenAsync(RefreshTokenRecord refreshTokenRecord)
    {
        await this._propertunityDataCenterContext.RefreshTokenRecords.AddAsync(refreshTokenRecord);
        await this._propertunityDataCenterContext.SaveChangesAsync();

        return 1;
    }
}