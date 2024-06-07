using _3_Data;
using _3_Data.Models.Publication;
using _3_Data.Models.Search;
using _3_Shared.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace _2_Domain;

public class SearchDomain : ISearchDomain 
{
    //  @Dependencies
    private readonly ISearchData _searchData; 
    
    //  @Constructor
    public SearchDomain(
        ISearchData searchData
    )
    {
        this._searchData = searchData;
    }
    
    //  @Methods
    public async Task<List<PublicationModel>> SearchAsync(SearchModel search)
    {
        //  @Validations
        //  1.  'search.Type' must be a valid 'SearchConstraints' enum value.
        //      To avoid hardcoding, we can use Enum.IsDefined.
        if (!Enum.IsDefined(typeof(SearchConstraints), search.Type))
        {
            throw new Exception("Type appears to be invalid.");
        }
        
        //  2.  As a general rule, it doesn't matter if priceMin is higher than priceMax.
        //      It'll just pick the highest value and reorganize the range.
        //      After all, this is a model, not a request anymore.
        if (search.PriceMin > search.PriceMax)
        {
            (search.PriceMin, search.PriceMax) = (search.PriceMax, search.PriceMin);
        }

        var results = await this._searchData.SearchAsync(search);
        return await Task.FromResult(results);
    }
}
