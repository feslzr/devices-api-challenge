using Challenge.Application.Exceptions;
using Challenge.Application.Interfaces.Repository;
using Challenge.Domain.Entity;
using Challenge.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Infrastructure.Data.Repository;

[ExcludeFromCodeCoverage]
public class DeviceRepository(SqlDbContext context) : BaseRepository<Device>(context), IDeviceRepository
{
    public async Task<Device> GetDeviceByIdAsync(int id)
        => await _dbSet.Where(c => c.Id == id).FirstOrDefaultAsync()
           ?? throw new NullEntityException($"Device with id {id} not found.");

    public async Task<List<Device>> GetDevicesAsync(string? name, string? brand, int? state, int offset, int limit)
    {
        IQueryable<Device> query = BuildQueryParameters(name, brand, state);

        return await query.Skip(offset)
                          .Take(limit)
                          .ToListAsync();
    }

    public async Task<Device?> GetDeviceAsync(string? name, string? brand, int? state)
    {
        IQueryable<Device> query = BuildQueryParameters(name, brand, state);

        return await query.FirstOrDefaultAsync();
    }

    IQueryable<Device> BuildQueryParameters(string? name, string? brand, int? state)
    {
        IQueryable<Device> query = _dbSet;

        if (!string.IsNullOrEmpty(name))
            query = query.Where(c => c.Name.Contains(name));

        if (!string.IsNullOrEmpty(brand))
            query = query.Where(c => c.Brand.Contains(brand));

        if (state.HasValue)
            query = query.Where(c => c.State == state);

        return query;
    }
}