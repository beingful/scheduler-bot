using Slack.Bot.Api.Endpoints;
using Slack.Bot.Api.Services;
using SlackNet.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Logging.AddAzureWebAppDiagnostics();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
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
