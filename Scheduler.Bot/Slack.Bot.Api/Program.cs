using Slack.Bot.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Logging.AddAzureWebAppDiagnostics();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var webApp = builder.Build();

webApp.UseSwagger();
webApp.UseSwaggerUI();

webApp.UseHttpsRedirection();

webApp.MapHomeEndpoints();

webApp.Run();
