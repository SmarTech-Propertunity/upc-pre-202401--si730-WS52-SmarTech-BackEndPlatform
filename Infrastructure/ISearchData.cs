using _3_Data.Models.Publication;
using _3_Data.Models.Search;

namespace _3_Data;

public interface ISearchData
{
    public Task<List<PublicationModel>> SearchAsync(SearchModel search);
}