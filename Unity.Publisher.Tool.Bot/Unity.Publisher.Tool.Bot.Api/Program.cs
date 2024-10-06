using Bot.Api.Endpoints;
using Bot.Api.Dependencies;
using SlackNet.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Logging.AddAzureWebAppDiagnostics();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddConfigurationModels(builder.Configuration)
    .AddApiServices()
    .AddSlackServices(builder.Configuration);

var webApp = builder.Build();

webApp.UseSwagger();
webApp.UseSwaggerUI();

webApp.UseSlackNet();

webApp.UseHttpsRedirection();

webApp
    .AddHomeEndpoints()
    .AddSlackEndpoints();

webApp.Run();
