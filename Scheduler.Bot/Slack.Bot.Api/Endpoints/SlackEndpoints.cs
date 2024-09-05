using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SlackNet.AspNetCore;
using SlackNet.Events;

namespace Slack.Bot.Api.Endpoints;

internal static class SlackEndpoints
{
    public static WebApplication AddSlackEndpoints(this WebApplication webApp)
    {
        return webApp
            .PostUrlVerificationEndpoint()
            .PostEventEndpoint();
    }

    private static WebApplication PostUrlVerificationEndpoint(this WebApplication webApp)
    {
        webApp
            .MapPost("slack/challenge", (
                [FromBody]UrlVerification request,
                HttpContext httpContext,
                ISlackRequestHandler requestHandler) =>
            {
                return Results.Ok(request);
            })
            .WithDisplayName("Url verification endpoint")
            .WithDescription("The endpoint of the Scheduler Slack Bot. Designed to verify the bot API.");

        return webApp;
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
