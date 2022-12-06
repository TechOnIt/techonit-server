using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using iot.Application;
using iot.Application.Commands.Device.CreateDevice;
using iot.Application.Commands.Device.DeleteDevice;
using iot.Application.Commands.Device.UpdateDevice;
using iot.Application.Commands.Sensor.CreatSensor;
using iot.Application.Commands.Sensor.DeleteSensor;
using iot.Application.Commands.Sensor.UpdateSensor;
using iot.Application.Commands.Structures.Authentication.SignInCommands;
using iot.Application.Commands.Users.Authentication.SignInOtpCommands;
using iot.Application.Common.DTOs.Settings;
using iot.Desk.Api.GraphQl.PerformanceReport;
using iot.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

RegisterMediatRCommands(builder.Services);

// Map app setting json to app setting object.
// https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection(nameof(AppSettingDto)));
builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("AppSettingDto"));

ConfigureServices(builder.Services);
var app = builder.Build();

// middlewares
// if you want to catch all exceptions by custom middleware Uncomment the following line
// And if you don't need it, then comment the following line
app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // add altair UI to development only
    app.UseGraphQLAltair();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();
//app.UseAuthorization();
// make sure all our schemas registered to route
app.UseGraphQL<ISchema>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=General}/{action=Index}/{id?}");

    endpoints.MapControllers();
});

await app.RunAsync();


void ConfigureServices(IServiceCollection services)
{
    services.AddInfrastructureServices();
    services.AddApplicationServices();
    services.AddFluentValidationServices();

    // Add services to the container.
    // add notes schema
    builder.Services.AddSingleton<ISchema, ReportSchema>(services => new ReportSchema(new SelfActivatingServiceProvider(services)));
    // register graphQL
    builder.Services.AddGraphQL(options =>
    {
        options.EnableMetrics = true;
    }).AddSystemTextJson();

    services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.WithOrigins("*")
                       .AllowAnyHeader();
            });
    });
}

void RegisterMediatRCommands(IServiceCollection services)
{
    services.AddMediatR(typeof(CreateDeviceCommand).GetTypeInfo().Assembly);
}
