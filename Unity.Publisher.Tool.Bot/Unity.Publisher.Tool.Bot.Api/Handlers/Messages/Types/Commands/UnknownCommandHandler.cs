using SlackNet.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Bot.Api.Handlers.Messages.Types.Commands;

public class UnknownCommandHandler : ICommandEventHandler
{
    private readonly ILogger _logger;

    public UnknownCommandHandler(ILogger<UnknownCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(MessageEvent messageEvent)
    {
        await Task.Run(() =>
        {
            _logger.LogInformation(
                $"The command \'{messageEvent.Text}\' is not recognized.",
                messageEvent.Text,
                messageEvent.BotId);
        });
    }
}
