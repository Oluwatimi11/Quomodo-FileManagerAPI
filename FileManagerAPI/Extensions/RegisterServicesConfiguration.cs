using System;
using FileManager.Core.Interfaces;
using FileManager.Core.Services;

namespace FileManagerAPI.Extensions
{
    public static class RegisterServicesConfiguration
    {
        public static void AddRegisterServices(this IServiceCollection services, IConfiguration configure)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });

            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFileService, FileService>();

        }
    }

}

