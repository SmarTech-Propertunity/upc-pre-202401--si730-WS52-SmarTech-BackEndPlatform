using _1_API.Controllers;
using _1_API.Request;
using _2_Domain;
using _3_Data.Models.Publication;
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
        var mockPublicationDomain = new Mock<IPublicationDomain>();

        var fakeGetPublicationRequest = new GetPublicationRequest()
        {
            Id = 1,
            IsActive = 1
        };
        var fakeGetPublicationModel = new GetPublicationModel()
        {
            Id = 1,
            IsActive = true
        };
        mockMapper.Setup(m => m.Map<GetPublicationModel>(It.IsAny<GetPublicationRequest>())).Returns(fakeGetPublicationModel);        
        var fakeResult = new PublicationModel
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
        };
        mockPublicationDomain.Setup(p => p.GetPublicationAsync(fakeGetPublicationModel)).ReturnsAsync(fakeResult);
        
        var controller = new PublicationController(
            mockMapper.Object,
            mockPublicationDomain.Object
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
        var mockPublicationDomain = new Mock<IPublicationDomain>();

        var fakePostPublicationRequest = new PostPublicationRequest()
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
            IsActive = true,
            Title = "Fake Title",
            Price = 1.0f,
            UpdatedData = null,
            UserId = 1
        };
        mockMapper.Setup(m => m.Map<PublicationModel>(It.IsAny<PostPublicationRequest>())).Returns(fakePublicationModel);
        var fakeResult = 1;
        mockPublicationDomain.Setup(p => p.PostPublicationAsync(fakePublicationModel)).ReturnsAsync(fakeResult);
        
        var controller = new PublicationController(
            mockMapper.Object,
            mockPublicationDomain.Object
        );
        
        //  @Act
        var result = await controller.PostPublication(fakePostPublicationRequest);
        
        //  @Assert
        Assert.IsType<OkObjectResult>(result);
    }
}