using AutoFixture;
using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Entity;
using Challenge.Domain.Enum;
using Challenge.Tests.Common.Common;
using MediatR;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationTest.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class DeleteDeviceHandlerTest : BaseDeviceTest
{
    private readonly DeleteDeviceHandler _handler;

    private readonly Mock<IDeviceRepository> _deviceRepositoryMock;

    public DeleteDeviceHandlerTest()
    {
        _deviceRepositoryMock = new Mock<IDeviceRepository>();

        _handler = new DeleteDeviceHandler(_deviceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_AndNotInUse_ShallDeletesDevice()
    {
        // Arrange
        var request = _fixture.Create<DeleteDeviceUseCase>();

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        _deviceRepositoryMock.Setup(x => x.Remove(It.IsAny<Device>()));
        _deviceRepositoryMock.Setup(x => x.SaveChangesAsync());

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Unit>(result);

        _deviceRepositoryMock.Verify(x => x.Remove(It.IsAny<Device>()), Times.Once);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDeviceExists_AndInUse_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = _fixture.Create<DeleteDeviceUseCase>();

        _fixture.Register(() => Device.Create(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            (int)StateEnum.Inuse));

        _deviceRepositoryMock.Setup(x => x.GetByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _handler.Handle(request, default));

        _deviceRepositoryMock.Verify(x => x.Remove(It.IsAny<Device>()), Times.Never);
        _deviceRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}