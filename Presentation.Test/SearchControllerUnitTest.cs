using _1_API.Search.Controllers;
using _2_Domain;
using _2_Domain.Publication.Models.Entities;
using _2_Domain.Search.Models.Entities;
using _2_Domain.Search.Models.Queries;
using _2_Domain.Search.Services;
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
        var mockSearchCommandService = new Mock<ISearchCommandService>();
        var mockSearchQueryService = new Mock<ISearchQueryService>();
        
        var fakeSearchRequest = new SearchQuery()
        {
            SearchInput = "Fake Search Input",
            Type = (int) SearchConstraints.RealStateType,
            PriceMin = 1.0f,
            PriceMax = 2.0f
        };
        var fakeSearchModel = new SearchQuery()
        {
            SearchInput = "Fake Search Input",
            Type = (int) SearchConstraints.RealStateType,
            PriceMin = 1.0f,
            PriceMax = 2.0f
        };
        mockMapper.Setup(m => m.Map<SearchQuery>(It.IsAny<SearchQuery>())).Returns(fakeSearchModel);        
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
                IsDeleted = false,
                Title = "Fake Title",
                Price = 1.0f,
                UpdatedDate = null,
                UserId = 1
            },
        };
        mockSearchQueryService.Setup(p => p.Handle(fakeSearchModel)).ReturnsAsync(fakeResult);
        
        var controller = new SearchController(
            mockMapper.Object,
            mockSearchCommandService.Object,
            mockSearchQueryService.Object
        );
        
        //  @Act
        var result = await controller.SearchMain(fakeSearchRequest);
        
        //  @Assert
        Assert.IsType<OkObjectResult>(result);
    }
}