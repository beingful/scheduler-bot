using Bot.Api.Events.Messages;
using Bot.Api.Events.Messages.Types.Commands;

namespace Bot.Api.Handlers.Messages.Types.Commands;

public sealed class CommandHandlerProvider : ITypedMessageEventHandlerProvider
{
    private readonly MessageToCommandConverter _messageToCommandConverter;
    private readonly Dictionary<Command, ICommandEventHandler> _commandHandlers;

    public CommandHandlerProvider(
        MessageToCommandConverter messageToCommandConverter,
        Dictionary<Command, ICommandEventHandler> commandHandlers)
    {
        _messageToCommandConverter = messageToCommandConverter;
        _commandHandlers = commandHandlers;
    }

    public ITypedMessageEventHandler Provide(string message)
    {
        Command command = _messageToCommandConverter.Convert(message);

        return _commandHandlers[command];
    }
}
