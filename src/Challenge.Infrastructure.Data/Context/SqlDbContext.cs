using Challenge.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Infrastructure.Data.Context;

[ExcludeFromCodeCoverage]
public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
    public DbSet<Device> Contact { get; set; }
}