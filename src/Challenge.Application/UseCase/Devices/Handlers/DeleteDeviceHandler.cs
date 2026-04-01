using Challenge.Application.Interfaces.Repository;
using Challenge.Domain.Enum;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Application.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class DeleteDeviceUseCase : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteDeviceHandler(IDeviceRepository deviceRepository) : IRequestHandler<DeleteDeviceUseCase, Unit>
{
    public async Task<Unit> Handle(DeleteDeviceUseCase request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetByIdAsync(request.Id);

        if (device.State == (int)StateEnum.Inuse)
            throw new InvalidOperationException("Device is currently in use and cannot be deleted.");

        deviceRepository.Remove(device);
        await deviceRepository.SaveChangesAsync();

        return Unit.Value;
    }
}