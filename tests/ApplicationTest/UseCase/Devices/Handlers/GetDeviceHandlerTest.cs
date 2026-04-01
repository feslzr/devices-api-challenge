using AutoFixture;
using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Handlers;
using Challenge.Domain.Entity;
using Challenge.Tests.Common.Common;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationTest.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class GetDeviceHandlerTest : BaseDeviceTest
{
    private readonly GetDeviceHandler _handler;

    private readonly Mock<IDeviceRepository> _deviceRepositoryMock;

    public GetDeviceHandlerTest()
    {
        _deviceRepositoryMock = new Mock<IDeviceRepository>();

        _handler = new GetDeviceHandler(_deviceRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShallReturnsSingleDevice()
    {
        // Arrange
        var request = _fixture.Create<GetDeviceUseCase>();

        _deviceRepositoryMock.Setup(x => x.GetDeviceByIdAsync(request.Id))
                             .ReturnsAsync(_fixture.Create<Device>());

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.IsType<Device>(result);

        _deviceRepositoryMock.Verify(x => x.GetDeviceByIdAsync(It.IsAny<int>()), Times.Once);
    }
}