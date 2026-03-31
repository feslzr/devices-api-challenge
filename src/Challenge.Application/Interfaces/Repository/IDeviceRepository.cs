using Challenge.Domain.Entity;

namespace Challenge.Application.Interfaces.Repository;

public interface IDeviceRepository : IBaseRepository<Device>
{
    Task<Device> GetDeviceByIdAsync(int id);

    Task<List<Device>> GetDevicesAsync(string? name, string? brand, int? state, int offset, int limit);

    Task<Device?> GetDeviceAsync(string? name, string? brand, int? state);
}