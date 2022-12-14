using FileManager.Core.Utilities.Profiles;
using FileManagerAPI.Extensions;
using Serilog;
using AutoMapper;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isDevelopment = environment == Environments.Development;

IConfiguration configure = ConfigurationSetUp.GetConfig(isDevelopment);
LogConfig.SetUpSerilog(configure);

try
{
    Log.Information("Application is starting.");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddSingleton(Log.Logger);
    builder.Services.AddRegisterServices(configure);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(typeof(MappingProfiles));


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    Log.Fatal(e.Message, e.StackTrace, "The application has failed to start correctly!!!");
}
finally
{
    Log.CloseAndFlush();
}