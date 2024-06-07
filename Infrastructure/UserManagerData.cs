using _3_Data.Contexts;
using _3_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3_Data;

public class UserManagerData : IUserManagerData
{
    //  @Dependencies
    private readonly PropertunityDataCenterContext _propertunityDataCenterContext;

    //  @Constructor
    public UserManagerData(
        PropertunityDataCenterContext propertunityDataCenterContext
    )
    {
        this._propertunityDataCenterContext = propertunityDataCenterContext;
    }

    //  @Methods
    public async Task<UserInformation?> GetUserByIdAsync(int id)
    {
        var result = await this._propertunityDataCenterContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        return result?._UserInformation;
    }
}