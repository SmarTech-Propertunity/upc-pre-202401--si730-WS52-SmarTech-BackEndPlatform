using _2_Domain.Publication.Models.Entities;
using _2_Domain.Publication.Models.Queries;
using _2_Domain.Publication.Repositories;
using _3_Data.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace _3_Data.Publication.Persistence;

public class PublicationData : IPublicationData
{
    //  @Dependencies
    private readonly PropertunityDataCenterContext _propertunityDataCenterContext;

    //  @Constructor
    public PublicationData(
        PropertunityDataCenterContext propertunityDataCenterContext
    )
    {
        this._propertunityDataCenterContext = propertunityDataCenterContext;
    }

    //  @Methods
    public async Task<PublicationModel?> GetPublicationAsync(GetPublicationQuery query)
    {
        var result = await this._propertunityDataCenterContext.Publication.
            Where(u => 
                (u.Id == query.Id) && 
                (u.IsActive == query.IsActive)
            ).FirstOrDefaultAsync();
        
        return result;
    }
    
    public async Task<int> PostPublicationAsync(PublicationModel publication)
    {
        var executionStrategy = this._propertunityDataCenterContext.Database.CreateExecutionStrategy();
        
        await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = await this._propertunityDataCenterContext.Database.BeginTransactionAsync())
            {
                try
                {
                    publication.IsActive = true;
                    this._propertunityDataCenterContext.Publication.Add(publication);
                    await this._propertunityDataCenterContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception exception)
                {
                    await transaction.RollbackAsync();
                }
            }        
        });
        
        return publication.Id;
    }

    public async Task<List<PublicationModel>> GetUserPublicationsAsync(int userId)
    {
        var result = await this._propertunityDataCenterContext.Publication.
            Where(u => 
                (u.UserId == userId) && 
                (u.IsActive == true)
            ).ToListAsync();

        return result;
    }

    public async Task<int> DeletePublicationAsync(int publicationId)
    {
        var publication = await this._propertunityDataCenterContext.Publication.
            Where(u => 
                (u.Id == publicationId) && 
                (u.IsActive == true)
            ).FirstOrDefaultAsync();

        if (publication == null)
        {
            return -1;
        }

        publication.IsActive = false;
        await this._propertunityDataCenterContext.SaveChangesAsync();

        return publication.Id;
    }
}