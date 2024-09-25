using Bot.Api.Configuration.Models;
using SlackNet;
using SlackNet.Events;
using SlackNet.WebApi;
using System;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Bot.Api.Handlers.Messages;

public sealed class MessageEventHandler : SlackNet.IEventHandler<MessageEvent>
{
    private readonly SlackBot _bot;
    private readonly IMessageEventHandler _userMessageHandler;
    private readonly ISlackApiClient _slackApiClient;
    private readonly ILogger _logger;

    public MessageEventHandler(
        SlackBot bot,
        IMessageEventHandler userMessageHandler,
        ISlackApiClient slackApiClient,
        ILogger<MessageEventHandler> logger)
    {
        _bot = bot;
        _userMessageHandler = userMessageHandler;
        _slackApiClient = slackApiClient;
        _logger = logger;
    }

    public async Task Handle(MessageEvent messageEvent)
    {
        _logger.LogInformation(
            "Message \'{0}\' was posted in the channel \'{1}\' by the user \'{2}\'",
            messageEvent.Text,
            messageEvent.Channel,
            messageEvent.User);

        if (IsNotBot(messageEvent.User))
        {
            await _userMessageHandler.HandleAsync(messageEvent);
        }

        if (messageEvent.Text == "hi")
        {
            await _slackApiClient.Chat.PostMessage(new Message
            {
                Text = string.Format(
                $"Hello, {messageEvent.User}!"),
                Channel = messageEvent.Channel
            });
        }
    }

    private bool IsNotBot(string userId)
    {
        return userId.ToLower() != _bot.Id.ToLower();
    }
}
