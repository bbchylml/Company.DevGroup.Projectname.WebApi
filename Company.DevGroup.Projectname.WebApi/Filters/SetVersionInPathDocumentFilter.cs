<<<<<<< HEAD
﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Company.DevGroup.Projectname.WebApi.Filters
{
    public class SetVersionInPathDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var updatedPaths = new OpenApiPaths();

            foreach (var entry in swaggerDoc.Paths)
            {
                updatedPaths.Add(
                    entry.Key.Replace("v{version}", swaggerDoc.Info.Version),
                    entry.Value);
            }

            swaggerDoc.Paths = updatedPaths;
        }
    }
}
=======
﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Company.DevGroup.Projectname.WebApi.Filters
{
    public class SetVersionInPathDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var updatedPaths = new OpenApiPaths();

            foreach (var entry in swaggerDoc.Paths)
            {
                updatedPaths.Add(
                    entry.Key.Replace("v{version}", swaggerDoc.Info.Version),
                    entry.Value);
            }

            swaggerDoc.Paths = updatedPaths;
        }
    }
}
>>>>>>> 06de2a93f4a5608de4212ff7aa5b5cc6082aafe6
