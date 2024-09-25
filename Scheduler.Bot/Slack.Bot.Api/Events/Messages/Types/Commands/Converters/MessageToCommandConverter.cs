using System.Text.RegularExpressions;
using Slack.Bot.Api.Events.Messages.Types;
using Slack.Bot.Api.Events.Messages.Types.Commands;

namespace Slack.Bot.Api.Events.Messages;

public sealed partial class MessageToCommandConverter : MessageConverter<Command>
{
    public MessageToCommandConverter(ILogger<MessageToCommandConverter> logger) : base(logger)
    {
    }

    public override MessageType MessageType => MessageType.Command;

    public override Command Convert(string message)
    {
        Command command = Command.Unknown;

        try
        {
            string commandName = Pattern().Match(message).Value[1..];

            command = (Command)Enum.Parse(typeof(Command), commandName, ignoreCase: true);
        }
        catch (Exception exception)
        {
            Logger.LogError(
                exception,
                "Unexpected error occured while try to convert message \'{0}\' into the command",
                message);
        }

        return command;
    }

    [GeneratedRegex("^-[a-z]+$", options: RegexOptions.IgnoreCase)]
    protected override partial Regex Pattern();
}
