using AutoFixture;
using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Entity;
using Challenge.Domain.Models;
using Challenge.Tests.Common.Common;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationTest.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class GetDeviceAllHandlerTest : BaseDeviceTest
{
    private readonly GetDeviceAllHandler _handler;

    private readonly Mock<IDeviceRepository> _deviceRepositoryMock;

    public GetDeviceAllHandlerTest()
    {
        _deviceRepositoryMock = new Mock<IDeviceRepository>();

        _handler = new GetDeviceAllHandler(_deviceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShallReturnsPaginationAllDevices()
    {
        // Arrange
        var request = _fixture.Create<GetDeviceAllUseCase>();

        _deviceRepositoryMock.Setup(x => x.GetDevicesAsync(null, null, null, It.IsAny<int>(), It.IsAny<int>()))
                             .ReturnsAsync([.. _fixture.CreateMany<Device>()]);

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Pagination<Device>>(result);

        _deviceRepositoryMock.Verify(x => x.GetDevicesAsync(null, null, null, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}