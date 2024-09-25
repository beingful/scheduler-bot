using SlackNet.AspNetCore;

namespace Bot.Api.Endpoints;

public static class SlackEndpoints
{
    public static WebApplication AddSlackEndpoints(this WebApplication webApp)
    {
        return webApp
            .PostEventEndpoint();
    }

    private static WebApplication PostEventEndpoint(this WebApplication webApp)
    {
        webApp
            .MapPost("slack/event", async (
                HttpContext httpContext,
                ISlackRequestHandler requestHandler) =>
            {
                return await requestHandler.HandleEventRequest(httpContext.Request);
            })
            .WithDisplayName("Post an event")
            .WithDescription("The endpoint of the Scheduler Slack Bot. Designed to handle updates.");

        return webApp;
    }
}
