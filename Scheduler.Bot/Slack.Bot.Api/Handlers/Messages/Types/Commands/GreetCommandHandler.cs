using SlackNet.Events;
using SlackNet;
using SlackNet.WebApi;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Slack.Bot.Api.Handlers.Messages.Types.Commands;

public sealed class GreetCommandHandler : ICommandEventHandler
{
    private readonly ISlackApiClient _slackApiClient;
    private readonly ILogger _logger;

    public GreetCommandHandler(
        ISlackApiClient slackApiClient,
        ILogger<GreetCommandHandler> logger)
    {
        _slackApiClient = slackApiClient;
        _logger = logger;
    }

    public async Task HandleAsync(MessageEvent messageEvent)
    {
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
