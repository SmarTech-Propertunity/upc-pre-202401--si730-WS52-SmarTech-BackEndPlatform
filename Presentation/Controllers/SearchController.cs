using _1_API.Request;
using _2_Domain;
using _3_Data.Models.Search;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace _1_API.Controllers;

[Route("api/v1/search/")]
[ApiController]
public class SearchController : ControllerBase
{
    //  @Dependencies
    private readonly IMapper _mapper;
    private readonly ISearchDomain _searchDomain;

    //  @Constructor
    public SearchController(
        IMapper mapper,
        ISearchDomain searchDomain
    )
    {
        this._mapper = mapper;
        this._searchDomain = searchDomain;
    }

    
    
    /// <summary>
    ///     Search for publications based on the search criteria.
    /// </summary>
    /// <param name="query">The search criteria.</param>
    /// <returns>
    ///     A list of publications that match the search criteria.
    /// </returns>
    /// <remarks>
    ///     The search criteria is provided in the <i>query</i> parameter which is an object of type <c>SearchRequest</c>.
    ///     <para> <c>SearchRequest</c> has the following properties: </para>
    ///         <para> &#149; <b>SearchInput</b>: The search input; a string. </para>
    ///         <para> &#149; <b>Type</b>: The type of search. </para>
    ///         <para> &#149; <b>PriceMin</b>: The minimum price for the search. </para>
    ///         <para> &#149; <b>PriceMax</b>: The maximum price for the search. </para>
    ///     <para> Each of these parameters represents the filters to find the perfect and suitable publication for the user. </para>
    ///     <para> Although, the algorithm used for searching is still primitive, it will suffice for simple demands by now. </para>
    ///     <para> <c>Type</c> is an integer representing an enumerator, it can be one of the following types representations:</para>
    ///         <para> &#149; <b>RealState</b>: Type of search for real state publications is 0. </para>
    ///         <para> &#149; <b>Location</b>: Type of search for locations is 1. </para>
    ///     <para> About price inputs, note that if <c>PriceMin</c> is greater than <c>PriceMax</c>, the controller will take the greatest value and </para>
    ///     <para> assign it to <c>PriceMax</c> as well as for the lower price to <c>PriceMin</c>. </para>
    /// </remarks>
    /// <response code="200">Returns <b>a list of publications</b> corresponding to the query parameters.</response>
    /// <response code="500"><b>Something wrong</b> appears to be with your query. Did you send a valid query?</response>
    /// <response code="404">Returns <b>an empty list</b> when no publications were found.</response>
    //  [Authorize]
    [HttpGet]
    [Route("main")]
    public async Task<IActionResult> SearchMain([FromQuery] SearchRequest query)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var search = this._mapper.Map<SearchModel>(query);
        var result = await this._searchDomain.SearchAsync(search);
        
        if (result.IsNullOrEmpty())
        {
            return NotFound();
        }

        return Ok(result);
    }
}