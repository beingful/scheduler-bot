using SlackNet.Events;

namespace Slack.Bot.Api.Handlers;

public interface IEventHandler<TEvent> where TEvent : Event
{
    Task HandleAsync(TEvent messageEvent);
}
