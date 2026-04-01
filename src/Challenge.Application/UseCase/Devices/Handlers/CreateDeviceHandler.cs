using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Base;
using Challenge.Domain.Entity;
using Challenge.Domain.Enum;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Application.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class CreateDeviceUseCase : BaseDeviceUseCase, IRequest<Device>
{
    [Required(ErrorMessage = "The 'Name' field is required.")]
    public override string? Name { get; set; }

    [Required(ErrorMessage = "The 'Brand' field is required.")]
    public override string? Brand { get; set; }

    [Required(ErrorMessage = "The 'State' field is required.")]
    public override StateEnum? State { get; set; }
}

public class CreateDeviceHandler(IDeviceRepository deviceRepository) : IRequestHandler<CreateDeviceUseCase, Device>
{
    public async Task<Device> Handle(CreateDeviceUseCase request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetDeviceAsync(request.Name, request.Brand, (int?)request.State);

        if (device != null)
            throw new InvalidOperationException("A device with this information has already been registered.");

        device = new Device(request.Name!, request.Brand!, (int)request.State!);

        await deviceRepository.AddAsync(device);
        await deviceRepository.SaveChangesAsync();

        return device;
    }
}