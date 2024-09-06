using SlackNet.Events;
using SlackNet;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Slack.Bot.Api.Handlers;

internal class BotMessageHandler : IEventHandler<BotMessage>
{
    private readonly ILogger _logger;

    public BotMessageHandler(ILogger<BotMessageHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BotMessage botMessage)
    {
        return Task.Run(() =>
        {
            _logger.LogInformation(
                "The message \'{0}\' was sent by a bot with id \'{1}\'",
                botMessage.Text,
                botMessage.BotId);
        });
    }
}
