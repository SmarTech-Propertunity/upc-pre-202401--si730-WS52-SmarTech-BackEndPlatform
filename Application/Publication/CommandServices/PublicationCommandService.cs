using _2_Domain.Publication.Models.Entities;
using _2_Domain.Publication.Repositories;
using _2_Domain.Publication.Services;
using _3_Data;
using _3_Shared.Domain.Models;
using _3_Shared.Middleware.Exceptions;

namespace Application.Publication.CommandServices;

public class PublicationCommandService : IPublicationCommandService
{
    //  @Dependencies
    private readonly IPublicationData _publicationData;
    private readonly IUserManagerData _userManagerData;
    
    //  @Constructor
    public PublicationCommandService(
        IPublicationData publicationData,
        IUserManagerData userManagerData
    )
    {
        this._publicationData = publicationData;
        this._userManagerData = userManagerData;
    }
    
    //  @Methods
    public async Task<int> Handle(PublicationModel publication)
    {
        var result = await this._userManagerData.GetUserByIdAsync(publication.UserId);
        if (result == null)
        {
            throw new ArgumentException("User not found with this Id!");
        }
        
        //  @Validations
        //  1.  Users can't post more than 'UserConstraints.MaxNormalUserPublications' publications.
        //      An account upgrade is required for more publications.
        var userPublications = await this._publicationData.GetUserPublicationsAsync(publication.UserId);
        if (
            (userPublications.Count >= (int)UserConstraints.MaxNormalUserPublications) && 
            (result.UserLevel == (int)UserRole.NormalUser)
        )
        {
            throw new MaxPublicationLimitReachedException("User reached the maximum publication limit!");
        }
        
        return await this._publicationData.PostPublicationAsync(publication);
    }

    public async Task<int> Handle(int id)
    {
        if (id <= 0)
        {
            throw new InvalidIdException("Invalid Id!");
        }
        
        return await this._publicationData.DeletePublicationAsync(id);
    }
}