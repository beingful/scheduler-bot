namespace Bot.Api.Configuration.Models;

public sealed record class NotificationSettings(
    string Destination, Initiator Initiator) : IConfigurationSection;
