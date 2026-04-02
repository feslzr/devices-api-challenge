using Challenge.Application.Interfaces.Repository;
using Challenge.Application.UseCase.Devices.Base;
using Challenge.Domain.Entity;
using Challenge.Domain.Enum;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Application.UseCase.Devices.Handlers;

[ExcludeFromCodeCoverage]
public class UpdateDeviceUseCase : BaseDeviceUseCase, IRequest<Device>
{
    /// <summary>
    /// Sets the device ID to be updated. This field is required and must be provided for the update operation to proceed.
    /// </summary>
    public required int Id { get; set; }

    public override string? Name { get; set; }

    public override string? Brand { get; set; }

    public override StateEnum? State { get; set; }
}

public class UpdateDeviceHandler(IDeviceRepository deviceRepository) : IRequestHandler<UpdateDeviceUseCase, Device>
{
    public async Task<Device> Handle(UpdateDeviceUseCase request, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.GetByIdAsync(request.Id);

        ValidateRequestAndApply(device, request);

        var existingDevice = await deviceRepository.GetDeviceAsync(device.Name, device.Brand, device.State);

        if (existingDevice != null)
            throw new InvalidOperationException("A device with this information has already been registered.");

        deviceRepository.Update(device);
        await deviceRepository.SaveChangesAsync();

        return device;
    }

    #region Private Methods

    static void ValidateRequestAndApply(Device device, UpdateDeviceUseCase request)
    {
        if (device.State == (int)StateEnum.Inuse
            && (!string.IsNullOrWhiteSpace(request.Name)
            || !string.IsNullOrWhiteSpace(request.Brand)))
            throw new InvalidOperationException("Device is currently in use. Fields 'Name' and 'Brand' cannot be updated.");

        ApplyChanges(device, request);
    }

    static void ApplyChanges(Device device, UpdateDeviceUseCase request)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            device.SetName(request.Name);

        if (!string.IsNullOrWhiteSpace(request.Brand))
            device.SetBrand(request.Brand);

        if (request.State.HasValue)
            device.SetState((int)request.State.Value);
    }

    #endregion
}