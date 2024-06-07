using System.Diagnostics.CodeAnalysis;
using _3_Data.Contexts;
using _3_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3_Data;

public class UserRegisterData : IUserRegisterData
{
    //  @Dependencies
    private readonly PropertunityDataCenterContext _propertunityDataCenterContext;

    //  @Constructor
    public UserRegisterData(
        PropertunityDataCenterContext propertunityDataCenterContext
    )
    {
        this._propertunityDataCenterContext = propertunityDataCenterContext;
    }

    //  @Methods
    public async Task<int> CreateUserAsync(User data)
    {
        var executionStrategy = this._propertunityDataCenterContext.Database.CreateExecutionStrategy();
        
        await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = await this._propertunityDataCenterContext.Database.BeginTransactionAsync())
            {
                try
                {
                    data._UserInformation.IsActive = true;
                    this._propertunityDataCenterContext.Users.Add(data);
                    await this._propertunityDataCenterContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception exception)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(exception.Message);
                }
            }        
        });
        
        return data.Id;
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var result = await this._propertunityDataCenterContext.Users.
            Where(u => u._UserCredentials.Email == email)
            .FirstOrDefaultAsync();
        
        return result;
    }
}