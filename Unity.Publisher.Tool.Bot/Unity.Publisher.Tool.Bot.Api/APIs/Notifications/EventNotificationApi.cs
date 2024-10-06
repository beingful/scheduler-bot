using Bot.Api.APIs.Notifications.Models;
using Bot.Api.Configuration.Models;

namespace Bot.Api.APIs.Notifications;

public sealed class EventNotificationApi : ApiService<EventNotificationApi>
{
    public EventNotificationApi(
        IConfiguration configuration,
        ILogger<EventNotificationApi> logger) : base(configuration, logger)
    {
    }

    public async Task SendAsync(NotificationSettings notification)
    {
        try
        {
            await PostAsync(
            request: new EventNotification(
                Sender: new Sender(
                    Name: notification.Initiator.Name,
                    Address: notification.Initiator.AccountId,
                    Account: new Account(
                        Id: notification.Initiator.AccountId,
                        Secret: notification.Initiator.AccountSecret)),
                Recipient: new Recipient(
                    Address: notification.Destination)));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occured during API call.");
        }
    }
}
