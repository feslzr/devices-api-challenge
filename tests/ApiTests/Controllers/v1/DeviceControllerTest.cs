using AutoFixture;
using Challenge.Api.Controllers.v1;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Entity;
using Challenge.Domain.Models;
using Challenge.Tests.Common.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApiTests.Controllers.v1;

[ExcludeFromCodeCoverage]
public class DeviceControllerTest : BaseDeviceTest
{
    private readonly DeviceController _deviceController;

    private readonly Mock<IMediator> _mediatorMock;

    public DeviceControllerTest()
    {
        _mediatorMock = new Mock<IMediator>();

        _deviceController = new DeviceController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateDevice_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var useCase = _fixture.Create<CreateDeviceUseCase>();
        var output = _fixture.Create<Device>();

        _mediatorMock.Setup(m => m.Send(useCase, default))
                     .ReturnsAsync(output);

        // Act
        var result = await _deviceController.Create(useCase);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
    }

    [Fact]
    public async Task UpdateDevice_ReturnsOkResult()
    {
        // Arrange
        var useCase = _fixture.Create<UpdateDeviceUseCase>();
        var output = _fixture.Create<Device>();

        _mediatorMock.Setup(m => m.Send(useCase, default))
                     .ReturnsAsync(output);

        // Act
        var result = await _deviceController.Update(It.IsAny<int>(), useCase);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task DeleteDevice_ReturnsNoContentResult()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteDeviceUseCase>(), default))
                     .ReturnsAsync(Unit.Value);

        // Act
        var result = await _deviceController.Delete(It.IsAny<int>());

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }

    [Fact]
    public async Task GetDeviceById_ReturnsOkResult()
    {
        // Arrange
        var output = _fixture.Create<Device>();

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetDeviceUseCase>(), default))
                     .ReturnsAsync(output);

        // Act
        var result = await _deviceController.GetById(It.IsAny<int>());

        // Assert
        var okResult = result as OkObjectResult;

        Assert.NotNull(result);
        Assert.True(result is OkObjectResult);

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        Assert.IsType<Device>(okResult?.Value);
    }

    [Fact]
    public async Task GetDeviceAll_ReturnsOkResult()
    {
        // Arrange
        var useCase = _fixture.Create<GetDeviceAllUseCase>();
        var output = _fixture.Create<Pagination<Device>>();

        _mediatorMock.Setup(m => m.Send(useCase, default))
                     .ReturnsAsync(output);

        // Act
        var result = await _deviceController.GetAll(useCase);

        // Assert
        var okResult = result as OkObjectResult;

        Assert.NotNull(result);
        Assert.True(result is OkObjectResult);

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        Assert.IsType<Pagination<Device>>(okResult?.Value);
    }

    [Fact]
    public async Task GetDeviceList_ReturnsOkResult()
    {
        // Arrange
        var useCase = _fixture.Create<GetDeviceListUseCase>();
        var output = _fixture.Create<List<Device>>();

        _mediatorMock.Setup(m => m.Send(useCase, default))
                     .ReturnsAsync(output);

        // Act
        var result = await _deviceController.GetList(useCase);

        // Assert
        var okResult = result as OkObjectResult;

        Assert.NotNull(result);
        Assert.True(result is OkObjectResult);

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        Assert.IsType<List<Device>>(okResult?.Value);
    }
}