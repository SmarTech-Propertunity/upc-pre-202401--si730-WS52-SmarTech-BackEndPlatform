using _2_Domain.Publication.Models.Entities;
using _2_Domain.Publication.Models.Queries;
using _2_Domain.Publication.Repositories;
using _2_Domain.Publication.Services;
using _3_Data;

namespace Application.Publication.QueryServices;

public class PublicationQueryService : IPublicationQueryService
{
    //  @Dependencies
    private readonly IPublicationRepository _publicationRepository;
    private readonly IUserManagerData _userManagerData;
    
    //  @Constructor
    public PublicationQueryService(
        IPublicationRepository publicationRepository,
        IUserManagerData userManagerData
    )
    {
        this._publicationRepository = publicationRepository;
        this._userManagerData = userManagerData;
    }
    
    //  @Methods
    public async Task<PublicationModel?> Handle(GetPublicationQuery query)
    {
        if (query.Id <= 0)
        {
            throw new ArgumentException("Invalid Id!");
        }
        
        return await this._publicationRepository.GetPublicationAsync(query);
    }
}