using SlackNet.Events;

namespace Bot.Api.Handlers.Messages;

public interface IMessageEventHandler : IEventHandler<MessageEvent>
{
}
