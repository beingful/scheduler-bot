using Slack.Bot.Api.Events.Messages.Types;
using System.Text.RegularExpressions;

namespace Slack.Bot.Api.Events.Messages;

public abstract partial class MessageConverter
{
    protected readonly ILogger Logger;

    public MessageConverter(ILogger logger)
    {
        Logger = logger;
    }

    public virtual MessageType MessageType => MessageType.None;

    public virtual bool IsConvertable(string message) => Pattern().IsMatch(message);

    [GeneratedRegex("")]
    protected virtual partial Regex Pattern();
}

public abstract partial class MessageConverter<TResult> : MessageConverter where TResult : Enum
{
    public MessageConverter(ILogger logger) : base(logger)
    {
    }

    public abstract TResult Convert(string message);
}
