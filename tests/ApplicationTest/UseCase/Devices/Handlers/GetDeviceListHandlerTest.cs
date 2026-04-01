using AutoFixture;
using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Entity;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationTest.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class GetDeviceListHandlerTest : BaseDeviceTest
{
    private readonly GetDeviceListHandler _handler;

    private readonly Mock<IDeviceRepository> _deviceRepositoryMock;

    public GetDeviceListHandlerTest()
    {
        _deviceRepositoryMock = new Mock<IDeviceRepository>();

        _handler = new GetDeviceListHandler(_deviceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShallReturnsPaginationFilteredDevices()
    {
        // Arrange
        var request = _fixture.Create<GetDeviceListUseCase>();

        _deviceRepositoryMock.Setup(x => x.GetDevicesAsync(request.Name, request.Brand, (int?)request.State, It.IsAny<int>(), It.IsAny<int>()))
                             .ReturnsAsync([.. _fixture.CreateMany<Device>()]);

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<List<Device>>(result);

        _deviceRepositoryMock.Verify(x => x.GetDevicesAsync(request.Name, request.Brand, (int?)request.State, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}