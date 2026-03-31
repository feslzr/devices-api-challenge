using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Challenge.Infrastructure.Filters;
using Challenge.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultFilterValues>();
        });
    }

    public static void UseSwaggerPage(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions.Select(d => d.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{description}/swagger.json",
                                        description.ToUpperInvariant());
            }
        });
    }
}