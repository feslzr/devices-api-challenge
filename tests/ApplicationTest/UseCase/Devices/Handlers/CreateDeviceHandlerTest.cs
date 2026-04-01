using AutoFixture;
using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Common;
using Challenge.Domain.Entity;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationTest.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class CreateDeviceHandlerTest : BaseDeviceTest
{
    private readonly CreateDeviceHandler _handler;

    private readonly Mock<IDeviceRepository> _deviceRepositoryMock;

    public CreateDeviceHandlerTest()
    {
        _deviceRepositoryMock = new Mock<IDeviceRepository>();

        _handler = new CreateDeviceHandler(_deviceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenDeviceNotExists_ShallCreatesDevice()
    {
        // Arrange
        var request = _fixture.Create<CreateDeviceUseCase>();

        _deviceRepositoryMock.Setup(x => x.GetDeviceAsync(request.Name, request.Brand, (int?)request.State))
                             .ReturnsAsync((Device?)null);

        _deviceRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Device>()));
        _deviceRepositoryMock.Setup(x => x.SaveChangesAsync());

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Device>(result);

        _deviceRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Device>()), Times.Once);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = _fixture.Create<CreateDeviceUseCase>();

        _deviceRepositoryMock.Setup(x => x.GetDeviceAsync(request.Name, request.Brand, (int?)request.State))
                             .ReturnsAsync(_fixture.Create<Device>());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _handler.Handle(request, default));

        _deviceRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Device>()), Times.Never);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}