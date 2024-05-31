using CashManager.Api.Controllers;
using CashManager.Application.UseCases.Users.Register;
using CashManager.Communication.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace CashManager.Api.Tests.Controllers;

public class UsersControllerTests
{
    [Fact]
    public async Task RegisterUser_ValidRequest_ReturnsCreated()
    {
        // Arrange
        var mockUserUseCase = new Mock<IRegisterUserUseCase>();
        mockUserUseCase.Setup(u => u.Execute(It.IsAny<RequestRegisterUser>())).Returns(Task.CompletedTask);
        var controller = new UsersController();

        // Act
        var result = await controller.RegisterUser(mockUserUseCase.Object, new RequestRegisterUser());

        // Assert
        result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
    }
}
