using _3_Data.Models.Publication;
using _3_Data.Models.Search;

namespace _2_Domain;

public interface ISearchDomain
{
    public Task<List<PublicationModel>> SearchAsync(SearchModel search);
}