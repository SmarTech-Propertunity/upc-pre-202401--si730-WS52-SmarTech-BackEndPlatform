using _3_Data;
using _3_Data.Models;

namespace _2_Domain;

public class UserManagerDomain : IUserManagerDomain
{
    //  @Dependencies
    private readonly IUserManagerData _userManagerData;
    
    //  @Constructor
    public UserManagerDomain(
        IUserManagerData userManagerData
    )
    {
        this._userManagerData = userManagerData;
    }
    
    //  @Methods
    public async Task<UserInformation?> GetUserByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new Exception("UserId is invalid.");
        }
        
        //  Proceed with your action, human.
        return await this._userManagerData.GetUserByIdAsync(id);
    }
}