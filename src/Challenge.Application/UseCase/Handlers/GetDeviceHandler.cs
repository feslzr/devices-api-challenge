using Challenge.Application.Interfaces.Repository;
using Challenge.Domain.Entity;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Application.UseCase.Handlers;

[ExcludeFromCodeCoverage]
public class GetDeviceUseCase : IRequest<Device>
{
    public required int Id { get; set; }
}

public class GetDeviceHandler(IDeviceRepository deviceRepository) : IRequestHandler<GetDeviceUseCase, Device>
{
    public async Task<Device> Handle(GetDeviceUseCase request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetDeviceByIdAsync(request.Id);

        return device;
    }
}