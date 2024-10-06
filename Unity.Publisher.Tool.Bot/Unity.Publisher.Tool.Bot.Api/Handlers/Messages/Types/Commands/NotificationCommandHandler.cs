using Bot.Api.APIs.Notifications;
using Bot.Api.Configuration.Models;
using SlackNet;
using SlackNet.Events;
using SlackNet.WebApi;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Bot.Api.Handlers.Messages.Types.Commands;

public sealed class NotificationCommandHandler : ICommandEventHandler
{
    private readonly NotificationSettings _notificationSettings;
    private readonly EventNotificationApi _eventNotificationApi;
    private readonly ISlackApiClient _slackApiClient;
    private readonly ILogger _logger;

    public NotificationCommandHandler(
        NotificationSettings notificationSettings,
        EventNotificationApi eventNotificationApi,
        ISlackApiClient slackApiClient,
        ILogger<NotificationCommandHandler> logger)
    {
        _notificationSettings = notificationSettings;
        _eventNotificationApi = eventNotificationApi;
        _slackApiClient = slackApiClient;
        _logger = logger;
    }

    public async Task HandleAsync(MessageEvent messageEvent)
    {
        try
        {
            await _eventNotificationApi.SendAsync(_notificationSettings);

            await _slackApiClient.Chat.PostMessage(new Message
            {
                Text = "The message is sent!",
                Channel = messageEvent.Channel
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured while notification was being sent.");
        }
    }
}
