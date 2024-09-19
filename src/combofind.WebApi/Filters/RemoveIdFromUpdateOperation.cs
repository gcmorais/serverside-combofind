using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace combofind.WebApi.Filters
{
    public class RemoveIdFromUpdateOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor.RouteValues["action"] == "Update")
            {
                var idParameter = operation.Parameters.FirstOrDefault(p => p.Name == "id");

                if (idParameter != null)
                {
                    operation.Parameters.Remove(idParameter);
                }
            }
        }
    }
}
