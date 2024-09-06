using SlackNet;
using SlackNet.Events;
using SlackNet.WebApi;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Slack.Bot.Api.Handlers;

internal sealed class MessageHandler : IEventHandler<MessageEvent>
{
    private readonly ISlackApiClient _slackApiClient;
    private readonly ILogger _logger;

    public MessageHandler(
        ISlackApiClient slackApiClient,
        ILogger<MessageHandler> logger)
    {
        _slackApiClient = slackApiClient;
        _logger = logger;
    }

    async Task IEventHandler<MessageEvent>.Handle(MessageEvent messageEvent)
    {
        _logger.LogInformation(
            "Message \'{0}\' was posted in the channel \'{1}\' by the user \'{2}\'",
            messageEvent.Text,
            messageEvent.Channel,
            messageEvent.User);

        try
        {
            await _slackApiClient.Chat.PostMessage(new Message
            {
                Text = string.Format(
                $"Hello, {messageEvent.User}!"),
                Channel = messageEvent.Channel
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unexpected error");
        }
    }
}