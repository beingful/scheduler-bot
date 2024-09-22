namespace Slack.Bot.Api.Configuration.Models;

public sealed record class SlackApi(string AccessToken, string SigningSecret) : IConfigurationSection;
