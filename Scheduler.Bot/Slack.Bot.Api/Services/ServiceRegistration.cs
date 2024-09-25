using Slack.Bot.Api.Configuration.Models;
using Slack.Bot.Api.Handlers.Messages;
using Slack.Bot.Api.Handlers.Messages.Types;
using SlackNet;
using SlackNet.AspNetCore;
using SlackNet.Events;
using Slack.Bot.Api.Events.Messages;
using Slack.Bot.Api.Events.Messages.Types;
using Slack.Bot.Api.Handlers.Messages.Types.Commands;
using Slack.Bot.Api.Events.Messages.Types.Commands;

namespace Slack.Bot.Api.Services;

public static class ServiceRegistration
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
                    .RegisterEventHandler<MessageEvent>(serviceProvider =>
                    {
                        SlackBot bot = configurationProvider.GetSection<SlackBot>();
                        MessageToCommandConverter messageToCommandConverter = new(
                            logger: serviceProvider.GetRequiredService<ILogger<MessageToCommandConverter>>()
                        );

                        return new MessageEventHandler(
                            slackApiClient: serviceProvider.GetRequiredService<ISlackApiClient>(),
                            bot: bot,
                            userMessageHandler: new UserMessageHandler(
                                messageTypeResolver: new MessageTypeResolver(
                                    messageConverters: [ messageToCommandConverter ]
                                ),
                                typedMessageHandlers: new Dictionary<MessageType, ITypedMessageEventHandlerProvider>()
                                {
                                    {
                                        MessageType.Command, new CommandHandlerProvider(
                                            messageToCommandConverter: messageToCommandConverter,
                                            commandHandlers: new Dictionary<Command, ICommandEventHandler>()
                                            {
                                                {
                                                    Command.Unknown, new UnknownCommandHandler(
                                                        logger: serviceProvider.GetRequiredService<ILogger<UnknownCommandHandler>>()
                                                    )
                                                },
                                                {
                                                    Command.Greet, new GreetCommandHandler(
                                                        slackApiClient: serviceProvider.GetRequiredService<ISlackApiClient>(),
                                                        logger: serviceProvider.GetRequiredService<ILogger<GreetCommandHandler>>()
                                                    )
                                                }
                                            }
                                        )
                                    }
                                }
                            ),
                            logger: serviceProvider.GetRequiredService<ILogger<MessageEventHandler>>()
                        );
                    });
            });
    }
}
