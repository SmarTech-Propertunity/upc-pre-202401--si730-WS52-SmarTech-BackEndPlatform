using _2_Domain.Publication.Models.Entities;
using _2_Domain.Search.Models.Entities;
using _2_Domain.Search.Models.Queries;
using _2_Domain.Search.Repositories;
using _3_Data.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace _3_Data.Search.Persistence;

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
    public async Task<List<PublicationModel>> SearchAsync(SearchQuery search)
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