using Microsoft.AspNetCore.Mvc;
using SlackNet.AspNetCore;
using SlackNet.Events;
using System.Net;

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
                return Results.Text(
                    content: request.Challenge,
                    contentType: "text/plain",
                    statusCode: (int)HttpStatusCode.OK);
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
