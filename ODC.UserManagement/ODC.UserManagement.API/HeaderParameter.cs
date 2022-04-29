using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODC.UserManagement.API
{
    public class HeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "UserEmail",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "string" },
                Required = true
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Password",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "string" },
                Required = true
            });
        }
    }
}
