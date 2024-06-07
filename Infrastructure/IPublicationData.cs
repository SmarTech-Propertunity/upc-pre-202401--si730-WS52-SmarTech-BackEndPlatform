using _3_Data.Models.Publication;

namespace _3_Data;

public interface IPublicationData
{
    public Task<PublicationModel?> GetPublicationAsync(GetPublicationModel id);
    
    public Task<int> PostPublicationAsync(PublicationModel publication);
    
    public Task<List<PublicationModel>> GetUserPublicationsAsync(int userId);
    
    public Task<int> DeletePublicationAsync(int publicationId);
}