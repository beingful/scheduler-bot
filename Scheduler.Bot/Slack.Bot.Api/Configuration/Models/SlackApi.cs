namespace Slack.Bot.Api.Configuration.Models;

internal sealed record class SlackApi(string AccessToken, string SigningSecret) : IConfigurationSection;
