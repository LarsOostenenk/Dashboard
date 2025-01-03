using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class SwaggerExamplesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody != null && operation.RequestBody.Content.ContainsKey("application/json"))
        {
            operation.RequestBody.Content["application/json"].Example = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["name"] = new Microsoft.OpenApi.Any.OpenApiString("Example Product"),
                ["description"] = new Microsoft.OpenApi.Any.OpenApiString("This is an example description."),
                ["price"] = new Microsoft.OpenApi.Any.OpenApiDouble(29.99),
                ["createdAt"] = new Microsoft.OpenApi.Any.OpenApiString("2025-01-01T00:00:00Z")
            };
        }
    }
}
