using Slack.Bot.Api.Events.Messages.Types;
using Slack.Bot.Api.Handlers.Messages.Types;
using SlackNet.Events;

namespace Slack.Bot.Api.Handlers.Messages;

public sealed class UserMessageHandler : IMessageEventHandler
{
    private readonly MessageTypeResolver _messageTypeResolver;
    private readonly Dictionary<MessageType, ITypedMessageEventHandlerProvider> _typedMessageHandlers;

    public UserMessageHandler(
        MessageTypeResolver messageTypeResolver,
        Dictionary<MessageType, ITypedMessageEventHandlerProvider> typedMessageHandlers)
    {
        _messageTypeResolver = messageTypeResolver;
        _typedMessageHandlers = typedMessageHandlers;
    }

    public async Task HandleAsync(MessageEvent messageEvent)
    {
        MessageType messageType = _messageTypeResolver.Resolve(messageEvent.Text);

        if (messageType != MessageType.None)
        {
            ITypedMessageEventHandlerProvider typedMessageHandlerProvider =
                _typedMessageHandlers[messageType];

            ITypedMessageEventHandler messageHandler =
                typedMessageHandlerProvider.Provide(messageEvent.Text);

            await messageHandler.HandleAsync(messageEvent);
        }
    }
}