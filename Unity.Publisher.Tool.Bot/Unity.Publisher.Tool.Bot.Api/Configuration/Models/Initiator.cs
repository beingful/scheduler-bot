namespace Bot.Api.Configuration.Models;

public sealed record class Initiator(
    string Name,
    string AccountId,
    string AccountSecret);
