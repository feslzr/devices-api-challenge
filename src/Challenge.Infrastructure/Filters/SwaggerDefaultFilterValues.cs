using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace Challenge.Infrastructure.Filters;

[ExcludeFromCodeCoverage]
internal class SwaggerDefaultFilterValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ApiDescription apiDescription = context.ApiDescription;

        operation.Deprecated |= apiDescription.IsDeprecated();

        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation?.Responses?[responseKey];

            var toRemove = response?.Content?
                .Where(kvp => !responseType.ApiResponseFormats.Any(x => x.MediaType == kvp.Key))
                .Select(kvp => kvp.Key)
                .ToList();

            if (toRemove != null)
                foreach (var contentType in toRemove)
                    response?.Content?.Remove(contentType);
        }

        if (operation?.Parameters == null)
            return;

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions
                .FirstOrDefault(p => p.Name == parameter.Name);

            if (description is null)
                continue;

            parameter.Description ??= description.ModelMetadata?.Description;
        }
    }
}