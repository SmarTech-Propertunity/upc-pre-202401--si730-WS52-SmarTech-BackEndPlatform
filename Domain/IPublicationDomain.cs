using _3_Data.Models.Publication;

namespace _2_Domain;

public interface IPublicationDomain
{
    public Task<PublicationModel?> GetPublicationAsync(GetPublicationModel query);
    
    public Task<int> PostPublicationAsync(PublicationModel publication);

    public Task<int> DeletePublicationAsync(int id);
}