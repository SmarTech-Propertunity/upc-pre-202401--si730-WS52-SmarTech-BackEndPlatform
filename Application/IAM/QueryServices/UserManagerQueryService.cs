using _2_Domain.IAM.Models.Entities;
using _2_Domain.IAM.Models.Queries;
using _2_Domain.IAM.Services.Queries;
using _3_Data;

namespace Application.Search.QueryServices;

public class UserManagerQueryService : IUserManagerQueryService
{
    //  @Dependencies
    private readonly IUserManagerData _userManagerData;
    
    //  @Constructor
    public UserManagerQueryService(
        IUserManagerData userManagerData
    )
    {
        this._userManagerData = userManagerData;
    }
    
    //  @Methods
    public async Task<UserInformation?> Handle(GetUserByIdQuery query)
    {
        if (query.Id <= 0)
        {
            throw new Exception("UserId is invalid.");
        }
        
        //  Proceed with your action, human.
        return await this._userManagerData.GetUserByIdAsync(query.Id);
    }
}