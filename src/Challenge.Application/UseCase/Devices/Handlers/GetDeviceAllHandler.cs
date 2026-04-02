using Challenge.Application.Interfaces.Repository;
using Challenge.Domain.Entity;
using Challenge.Domain.Models;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Challenge.Application.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class GetDeviceAllUseCase : IRequest<Pagination<Device>>
{
    /// <summary>
    /// Sets the offset for pagination. Default value = 0.
    /// </summary>
    [JsonPropertyName("offset")]
    public int Offset { get; set; } = 0;

    /// <summary>
    /// Sets the limit for pagination. Default value = 100.
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; set; } = 100;
}

public class GetDeviceAllHandler(IDeviceRepository deviceRepository) : IRequestHandler<GetDeviceAllUseCase, Pagination<Device>>
{
    public async Task<Pagination<Device>> Handle(GetDeviceAllUseCase request, CancellationToken cancellationToken)
    {
        var deviceList = await deviceRepository.GetDevicesAsync(null, null, null, request.Offset, request.Limit);

        return new()
        {
            Items = deviceList,
            Total = deviceList.Count,
            Offset = request.Offset,
            Limit = request.Limit
        };
    }
}
