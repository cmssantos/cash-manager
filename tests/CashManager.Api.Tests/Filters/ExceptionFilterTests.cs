using CashManager.Api.Filters;
using CashManager.Application.Interfaces;
using CashManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System.Resources;

namespace CashManager.Api.Tests.Filters;

public class ExceptionFilterTests
{
    [Fact]
    public void OnException_OtherException_ReturnsInternalServerError()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var exceptionContext = new ExceptionContext(new ActionContext(context, new(), new()), [])
        {
            Exception = new System.Exception() // Simulate another type of exception
        };

        var mockResourceManagerProvider = new Mock<IResourceManagerProvider>();
        var mockResourceManager = new Mock<ResourceManager>();
        mockResourceManager.Setup(m => m.GetString("InternalServerError")).Returns("INTERNAL_SERVER_ERROR");
        mockResourceManagerProvider.Setup(p => p.GetResourceManager()).Returns(mockResourceManager.Object);

        var exceptionFilter = new ExceptionFilter(mockResourceManagerProvider.Object);

        // Act
        exceptionFilter.OnException(exceptionContext);

        // Assert
        Assert.IsType<ObjectResult>(exceptionContext.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
    }

    [Fact]
    public void OnException_ErrorOnValidationException_ReturnsBadRequestWithErrors()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var exceptionContext = new ExceptionContext(new ActionContext(context, new(), new()), [])
        {
            Exception = new ErrorOnValidationException(["error"])
        };

        var mockResourceManagerProvider = new Mock<IResourceManagerProvider>();
        var mockResourceManager = new Mock<ResourceManager>();
        mockResourceManager.Setup(m => m.GetString("InternalServerError")).Returns("INTERNAL_SERVER_ERROR");
        mockResourceManagerProvider.Setup(p => p.GetResourceManager()).Returns(mockResourceManager.Object);

        var exceptionFilter = new ExceptionFilter(mockResourceManagerProvider.Object);

        // Act
        exceptionFilter.OnException(exceptionContext);

        // Assert
        Assert.IsType<BadRequestObjectResult>(exceptionContext.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
    }
}
