using _2_Domain.Publication.Models.Entities;
using _2_Domain.Publication.Models.Queries;
using _2_Domain.Publication.Repositories;
using _2_Domain.Publication.Services;
using _3_Data;

namespace Application.Publication.QueryServices;

public class PublicationQueryService : IPublicationQueryService
{
    //  @Dependencies
    private readonly IPublicationData _publicationData;
    private readonly IUserManagerData _userManagerData;
    
    //  @Constructor
    public PublicationQueryService(
        IPublicationData publicationData,
        IUserManagerData userManagerData
    )
    {
        this._publicationData = publicationData;
        this._userManagerData = userManagerData;
    }
    
    //  @Methods
    public async Task<PublicationModel?> Handle(GetPublicationQuery query)
    {
        if (query.Id <= 0)
        {
            throw new ArgumentException("Invalid Id!");
        }
        
        return await this._publicationData.GetPublicationAsync(query);
    }
}