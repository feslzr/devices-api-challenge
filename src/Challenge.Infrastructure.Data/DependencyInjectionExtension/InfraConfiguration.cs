using Challenge.Application.Interfaces.Repository;
using Challenge.Infrastructure.Data.Context;
using Challenge.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Infrastructure.Data.DependencyInjectionExtension;

[ExcludeFromCodeCoverage]
public static class InfraConfiguration
{
    public static IServiceCollection AddInfraConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDeviceRepository, DeviceRepository>();

        services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(configuration["Database:ConnectionString"]), ServiceLifetime.Scoped);

        return services;
    }
}