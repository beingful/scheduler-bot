using Slack.Bot.Api.Configuration.Models;
using SlackNet.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Slack.Bot.Api.Handlers.Messages;

public sealed class MessageEventHandler : SlackNet.IEventHandler<MessageEvent>
{
    private readonly SlackBot _bot;
    private readonly IMessageEventHandler _userMessageHandler;
    private readonly ILogger _logger;

    public MessageEventHandler(
        SlackBot bot,
        IMessageEventHandler userMessageHandler,
        ILogger<MessageEventHandler> logger)
    {
        _bot = bot;
        _userMessageHandler = userMessageHandler;
        _logger = logger;
    }

    public async Task Handle(MessageEvent messageEvent)
    {
        _logger.LogInformation(
            "Message \'{0}\' was posted in the channel \'{1}\' by the user \'{2}\'",
            messageEvent.Text,
            messageEvent.Channel,
            messageEvent.User);

        if (IsNotBot(messageEvent.User))
        {
            await _userMessageHandler.HandleAsync(messageEvent);
        }
    }

    private bool IsNotBot(string userId)
    {
        return userId.ToLower() != _bot.Id.ToLower();
    }
}
