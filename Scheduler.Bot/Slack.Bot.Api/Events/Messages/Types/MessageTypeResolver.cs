namespace Slack.Bot.Api.Events.Messages.Types;

public sealed class MessageTypeResolver
{
    private readonly List<MessageConverter> _messageConverters;

    public MessageTypeResolver(List<MessageConverter> messageConverters)
    {
        _messageConverters = messageConverters;
    }

    public MessageType Resolve(string message)
    {
        MessageType messageType = MessageType.None;

        foreach (MessageConverter converter in _messageConverters)
        {
            if (converter.IsConvertable(message))
            {
                messageType = converter.MessageType;

                break;
            }
        }

        return messageType;
    }
}
