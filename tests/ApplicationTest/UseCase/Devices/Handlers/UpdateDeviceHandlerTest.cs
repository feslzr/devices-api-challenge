using AutoFixture;
using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Common;
using Challenge.Domain.Entity;
using Challenge.Domain.Enum;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationTest.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class UpdateDeviceHandlerTest : BaseDeviceTest
{
    private readonly UpdateDeviceHandler _handler;

    private readonly Mock<IDeviceRepository> _deviceRepositoryMock;

    public UpdateDeviceHandlerTest()
    {
        _deviceRepositoryMock = new Mock<IDeviceRepository>();

        _handler = new UpdateDeviceHandler(_deviceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_ShallUpdatesDevice()
    {
        // Arrange
        var request = _fixture.Create<UpdateDeviceUseCase>();

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        _deviceRepositoryMock.Setup(x => x.GetDeviceAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                             .ReturnsAsync((Device?)null);

        _deviceRepositoryMock.Setup(x => x.Update(It.IsAny<Device>()));
        _deviceRepositoryMock.Setup(x => x.SaveChangesAsync());

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Device>(result);

        _deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_IsTheSameAsRequest_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = _fixture.Create<UpdateDeviceUseCase>();
        var device = _fixture.Create<Device>();

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(device);

        _deviceRepositoryMock.Setup(x => x.GetDeviceAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                             .ReturnsAsync(device);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _handler.Handle(request, default));

        _deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Never);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_AndRequestNameIsNullOrEmpty_ShallUpdatesDevice()
    {
        // Arrange
        var request = _fixture.Build<UpdateDeviceUseCase>()
                              .With(r => r.Name, string.Empty)
                              .Create();

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        _deviceRepositoryMock.Setup(x => x.GetDeviceAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                             .ReturnsAsync((Device?)null);

        _deviceRepositoryMock.Setup(x => x.Update(It.IsAny<Device>()));
        _deviceRepositoryMock.Setup(x => x.SaveChangesAsync());

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Device>(result);

        _deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_AndRequestBrandIsNullOrEmpty_ShallUpdatesDevice()
    {
        // Arrange
        var request = _fixture.Build<UpdateDeviceUseCase>()
                              .With(r => r.Brand, string.Empty)
                              .Create();

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        _deviceRepositoryMock.Setup(x => x.GetDeviceAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                             .ReturnsAsync((Device?)null);

        _deviceRepositoryMock.Setup(x => x.Update(It.IsAny<Device>()));
        _deviceRepositoryMock.Setup(x => x.SaveChangesAsync());

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Device>(result);

        _deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_AndRequestStateIsNull_ShallUpdatesDevice()
    {
        // Arrange
        var request = _fixture.Build<UpdateDeviceUseCase>()
                              .With(r => r.State, (StateEnum?)null)
                              .Create();

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        _deviceRepositoryMock.Setup(x => x.GetDeviceAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                             .ReturnsAsync((Device?)null);

        _deviceRepositoryMock.Setup(x => x.Update(It.IsAny<Device>()));
        _deviceRepositoryMock.Setup(x => x.SaveChangesAsync());

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Device>(result);

        _deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_AndInUse_AndRequestNameHasValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = _fixture.Build<UpdateDeviceUseCase>()
                              .With(r => r.Brand, string.Empty)
                              .Create();

        _fixture.Register(() => Device.Create(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            (int)StateEnum.Inuse));

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _handler.Handle(request, default));

        _deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Never);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_AndInUse_AndRequestBrandHasValue_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = _fixture.Build<UpdateDeviceUseCase>()
                              .With(r => r.Name, string.Empty)
                              .Create();

        _fixture.Register(() => Device.Create(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            (int)StateEnum.Inuse));

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _handler.Handle(request, default));

        _deviceRepositoryMock.Verify(x => x.Update(It.IsAny<Device>()), Times.Never);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}