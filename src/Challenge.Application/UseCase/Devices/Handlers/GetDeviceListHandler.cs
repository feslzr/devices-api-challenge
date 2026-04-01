using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Base;
using Challenge.Domain.Entity;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Challenge.Application.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class GetDeviceListUseCase : BaseDeviceUseCase, IRequest<List<Device>>
{
    [JsonPropertyName("offset")]
    public int Offset { get; set; } = 0;

    [JsonPropertyName("limit")]
    public int Limit { get; set; } = 100;
}

public class GetDeviceListHandler(IDeviceRepository deviceRepository) : IRequestHandler<GetDeviceListUseCase, List<Device>>
{
    public async Task<List<Device>> Handle(GetDeviceListUseCase request, CancellationToken cancellationToken)
    {
        var result = new List<Device>();

        var deviceList = await deviceRepository.GetDevicesAsync(request.Name, request.Brand, (int?)request.State, request.Offset, request.Limit);

        result.AddRange(deviceList);

        return result;
    }
}