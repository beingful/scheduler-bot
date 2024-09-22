using SlackNet.Events;

namespace Slack.Bot.Api.Handlers.Messages;

public interface IMessageEventHandler : IEventHandler<MessageEvent>
{
}
