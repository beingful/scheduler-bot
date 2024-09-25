namespace Bot.Api.Handlers.Messages.Types;

public interface ITypedMessageEventHandlerProvider
{
    ITypedMessageEventHandler Provide(string message);
}
