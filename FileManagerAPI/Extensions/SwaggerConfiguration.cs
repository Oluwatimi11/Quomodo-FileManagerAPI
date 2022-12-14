using System;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FileManagerAPI.Extensions
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("Quomodo-FileManager API", new()
                {
                    Title = "Quomodo-FileManager API",
                    Version = "1",
                    Description = "A payment web API with wallet and transfer features",
                    Contact = new OpenApiContact
                    {
                        Name = "File Manager API",
                        Email = "samueloluwatimi97@gmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "File Manager API"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               // setupAction.IncludeXmlComments(xmlPath);
            });
        }
    }
}

