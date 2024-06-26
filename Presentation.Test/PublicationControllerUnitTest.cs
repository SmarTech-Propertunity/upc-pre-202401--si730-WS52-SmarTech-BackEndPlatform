using _1_API.Publication.Controllers;
using _2_Domain;
using _2_Domain.Publication.Models.Commands;
using _2_Domain.Publication.Models.Entities;
using _2_Domain.Publication.Models.Queries;
using _2_Domain.Publication.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Presentation.Test;

public class PublicationControllerUnitTest
{
    [Fact]
    public async Task GetPublicationAsync_ResultOk()
    {
        //  @Arrange
        var mockMapper = new Mock<IMapper>();
        var mockPublicationCommandService = new Mock<IPublicationCommandService>();
        var mockPublicationQueryService = new Mock<IPublicationQueryService>();

        var fakeGetPublicationRequest = new GetPublicationQuery()
        {
            Id = 1
        };
        var fakeGetPublicationModel = new GetPublicationQuery()
        {
            Id = 1
        };
        mockMapper.Setup(m => m.Map<GetPublicationQuery>(It.IsAny<GetPublicationQuery>())).Returns(fakeGetPublicationModel);        
        var fakeResult = new PublicationModel
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
        };
        mockPublicationQueryService.Setup(p => p.Handle(fakeGetPublicationModel)).ReturnsAsync(fakeResult);
        
        var controller = new PublicationController(
            mockMapper.Object,
            mockPublicationCommandService.Object,
            mockPublicationQueryService.Object
        );
        
        //  @Act
        var result = await controller.GetPublication(fakeGetPublicationRequest);
        
        //  @Assert
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task PostPublicationAsync_ResultOk()
    {
        //  @Arrange
        var mockMapper = new Mock<IMapper>();
        var mockPublicationCommandService = new Mock<IPublicationCommandService>();
        var mockPublicationQueryService = new Mock<IPublicationQueryService>();

        var fakePostPublicationRequest = new PostPublicationCommand()
        {
            Title = "Fake Title",
            Description = "Fake Description",
            Price = 1.0f,
            _Location_Address = "Fake Address",
            UserId = 1,
        };
        var fakePublicationModel = new PublicationModel
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
        };
        mockMapper.Setup(m => m.Map<PublicationModel>(It.IsAny<PostPublicationCommand>())).Returns(fakePublicationModel);
        var fakeResult = 1;
        mockPublicationCommandService.Setup(p => p.Handle(fakePublicationModel)).ReturnsAsync(fakeResult);
        
        var controller = new PublicationController(
            mockMapper.Object,
            mockPublicationCommandService.Object,
            mockPublicationQueryService.Object
        );
        
        //  @Act
        var result = await controller.PostPublication(fakePostPublicationRequest);
        
        //  @Assert
        Assert.IsType<OkObjectResult>(result);
    }
}