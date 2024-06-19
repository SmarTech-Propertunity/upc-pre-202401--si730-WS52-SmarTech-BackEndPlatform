using _2_Domain.IAM.Models.Commands;
using _2_Domain.IAM.Models.Entities;
using _2_Domain.IAM.Models.Queries;
using _2_Domain.IAM.Models.ValueObjects;
using _3_Data.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace _3_Data.IAM.Persistence;

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
    public async Task<User?> GetUserByCredentialsAsync(GetTokenQuery query)
    {
        var result = this._propertunityDataCenterContext.Users.
            FirstOrDefaultAsync(user =>
            user._UserCredentials.Email == query.Email &&
            user._UserCredentials.Password == query.Password
        );

        return await result;
    }
    public async Task<RefreshTokenRecord?> FindExistingRefreshTokenAsync(CreateRefreshTokenCommand refreshToken)
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