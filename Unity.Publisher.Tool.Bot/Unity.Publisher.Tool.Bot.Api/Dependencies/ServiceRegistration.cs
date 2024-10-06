using Bot.Api.Configuration.Models;
using Bot.Api.Handlers.Messages;
using Bot.Api.Handlers.Messages.Types;
using SlackNet;
using SlackNet.AspNetCore;
using SlackNet.Events;
using Bot.Api.Events.Messages;
using Bot.Api.Events.Messages.Types;
using Bot.Api.Handlers.Messages.Types.Commands;
using Bot.Api.Events.Messages.Types.Commands;
using Bot.Api.APIs.Notifications;
using ConfigurationProvider = Bot.Api.Configuration.ConfigurationProvider;

namespace Bot.Api.Dependencies;

public static class ServiceRegistration
{
    public static IServiceCollection AddConfigurationModels(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationProvider configurationProvider = new(configuration);

        return services
            .AddSingleton<SlackBot>(_ =>
            {
                return configurationProvider.GetSection<SlackBot>();
            })
            .AddSingleton<NotificationSettings>(_ =>
            {
                return configurationProvider.GetSection<NotificationSettings>();
            });
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services.AddScoped<EventNotificationApi>();
    }

    public static IServiceCollection AddSlackServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSlackNet(slackConfiguration =>
            {
                SlackApi slackApiConfiguration =
                    new ConfigurationProvider(configuration).GetSection<SlackApi>();

                slackConfiguration
                    .UseApiToken(slackApiConfiguration.AccessToken)
                    .UseSigningSecret(slackApiConfiguration.SigningSecret)
                    .RegisterEventHandler<MessageEvent>(serviceProvider =>
                    {
                        MessageToCommandConverter messageToCommandConverter = new(
                            logger: serviceProvider.GetRequiredService<ILogger<MessageToCommandConverter>>()
                        );

                        return new MessageEventHandler(
                            slackApiClient: serviceProvider.GetRequiredService<ISlackApiClient>(),
                            bot: serviceProvider.GetRequiredService<SlackBot>(),
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
                                                        logger: serviceProvider.GetRequiredService<ILogger<UnknownCommandHandler>>())
                                                },
                                                {
                                                    Command.Greet, new GreetCommandHandler(
                                                        slackApiClient: serviceProvider.GetRequiredService<ISlackApiClient>(),
                                                        logger: serviceProvider.GetRequiredService<ILogger<GreetCommandHandler>>())
                                                },
                                                {
                                                    Command.Notification, new NotificationCommandHandler(
                                                        notificationSettings: serviceProvider.GetRequiredService<NotificationSettings>(),
                                                        eventNotificationApi: serviceProvider.GetRequiredService<EventNotificationApi>(),
                                                        logger: serviceProvider.GetRequiredService<ILogger<NotificationCommandHandler>>())
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
