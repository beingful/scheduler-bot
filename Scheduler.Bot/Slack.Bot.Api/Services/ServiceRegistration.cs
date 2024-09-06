using Slack.Bot.Api.Configuration.Models;
using Slack.Bot.Api.Handlers;
using SlackNet.AspNetCore;
using SlackNet.Events;
using SlackNet.Extensions.DependencyInjection;

namespace Slack.Bot.Api.Services;

internal static class ServiceRegistration
{
    public static IServiceCollection AddSlackServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSlackNet(slackConfiguration =>
            {
                Configuration.ConfigurationProvider configurationProvider = new(configuration);

                SlackApi slackApiConfiguration = configurationProvider.GetSection<SlackApi>();

                slackConfiguration
                    .UseApiToken(slackApiConfiguration.AccessToken)
                    .UseSigningSecret(slackApiConfiguration.SigningSecret)
                    .RegisterEventHandler<BotMessage, BotMessageHandler>()
                    .RegisterEventHandler<MessageEvent, MessageHandler>();
            });
    }
}
