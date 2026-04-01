using Asp.Versioning.ApiExplorer;
using Challenge.Api.Filters;
using Challenge.Application.DependencyInjectionExtension;
using Challenge.Infrastructure.Data.Context;
using Challenge.Infrastructure.Data.DependencyInjectionExtension;
using Challenge.Infrastructure.Extensions;
using Challenge.Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services
       .AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilterAttribute>();
        })
       .AddJsonOptions(options =>
        {
            var enumConverter = new JsonStringEnumConverter();
            options.JsonSerializerOptions.Converters.Add(enumConverter);
        });

builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<EnumSchemaFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddInfraConfiguration(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationMediatR();

builder.Host.UseDefaultServiceProvider(option => option.ValidateScopes = false);

var app = builder.Build();

app.UseSwaggerPage(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

#if DEBUG
app.UseDeveloperExceptionPage();
#endif

app.UseHttpsRedirection();
app.MapControllers();

// Apply migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
    await db.Database.MigrateAsync();
}

await app.RunAsync();