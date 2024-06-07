using _3_Data;
using _3_Data.Models;
using _3_Shared.Middleware.Exceptions;

namespace _2_Domain;

public class UserRegisterDomain : IUserRegisterDomain
{
    //   @Dependencies  
    private readonly IUserRegisterData _userRegisterData;

    //   @Constructor
    public UserRegisterDomain(
       IUserRegisterData userRegisterData   
    )
    {
       this._userRegisterData = userRegisterData;
    }
   
    //   @Implementations
    public async Task<int> RegisterUserAsync(User data)
    {
        //  @Validations
        //  1.  A user can't register if it already exists.
        //      The email must be unique while.
        var result = await this._userRegisterData.GetUserByEmailAsync(data._UserCredentials.Email);
        if (result != null)
        {
           throw new UserAlreadyExistsException("User already exists.");
        }
        
        return await this._userRegisterData.CreateUserAsync(data);
    }
}