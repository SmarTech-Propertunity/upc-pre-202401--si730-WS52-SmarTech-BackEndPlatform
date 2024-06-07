using _1_API.Controllers;
using _1_API.Request;
using _2_Domain;
using _3_Data.Models.Publication;
using _3_Data.Models.Search;
using _3_Shared;
using _3_Shared.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Presentation.Test;

public class SearchControllerUnitTest
{
    [Fact]
    public async Task SearchMain_ResultOk()
    {
        //  @Arrange
        var mockMapper = new Mock<IMapper>();
        var mockSearchDomain = new Mock<ISearchDomain>();

        var fakeSearchRequest = new SearchRequest()
        {
            SearchInput = "Fake Search Input",
            Type = (int) SearchConstraints.RealStateType,
            PriceMin = 1.0f,
            PriceMax = 2.0f
        };
        var fakeSearchModel = new SearchModel()
        {
            SearchInput = "Fake Search Input",
            Type = (int) SearchConstraints.RealStateType,
            PriceMin = 1.0f,
            PriceMax = 2.0f
        };
        mockMapper.Setup(m => m.Map<SearchModel>(It.IsAny<SearchRequest>())).Returns(fakeSearchModel);        
        var fakeResult = new List<PublicationModel>()
        {
            new PublicationModel()
            {
                _Location = new LocationModel()
                {
                    Address = "Fake Address",
                },
                CreatedDate = DateTime.Now,
                Description = "Fake Description",
                Id = 1,
                IsActive = true,
                Title = "Fake Title",
                Price = 1.0f,
                UpdatedData = null,
                UserId = 1
            },
        };
        mockSearchDomain.Setup(p => p.SearchAsync(fakeSearchModel)).ReturnsAsync(fakeResult);
        
        var controller = new SearchController(
            mockMapper.Object,
            mockSearchDomain.Object
        );
        
        //  @Act
        var result = await controller.SearchMain(fakeSearchRequest);
        
        //  @Assert
        Assert.IsType<OkObjectResult>(result);
    }
}