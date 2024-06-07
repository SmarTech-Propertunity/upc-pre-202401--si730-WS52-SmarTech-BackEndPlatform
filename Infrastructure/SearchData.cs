using _3_Data.Contexts;
using _3_Data.Models.Publication;
using _3_Data.Models.Search;
using Microsoft.EntityFrameworkCore;

namespace _3_Data;

public class SearchData : ISearchData
{
    //  @Dependencies
    private readonly PropertunityDataCenterContext _propertunityDataCenterContext;

    //  @Constructor
    public SearchData(
        PropertunityDataCenterContext propertunityDataCenterContext
    )
    {
        this._propertunityDataCenterContext = propertunityDataCenterContext;
    }

    //  @Methods
    public async Task<List<PublicationModel>> SearchAsync(SearchModel search)
    {
        //  !And advanced searching system must me implemented here.
        //  !For now, we will just search by price and address.
        
        var result = await this._propertunityDataCenterContext.Publication.Where(u =>
                search.PriceMin <= u.Price &&
                u.Price <= search.PriceMax &&
                u._Location.Address.Contains(search.SearchInput) &&
                u.IsActive)
            .ToListAsync();
        
        return result;
    }
}