namespace Slack.Bot.Api.Endpoints;

public static class HomeEndpoints
{
    public static WebApplication AddHomeEndpoints(this WebApplication webApplication)
    {
        return webApplication
            .GetSwaggerEndpoint()
            .GetGreetingsEndpoint();
    }

    private static WebApplication GetSwaggerEndpoint(this WebApplication webApplication)
    {
        webApplication.MapGet("/", context =>
        {
            return Task.Run(() =>
            {
                context.Response.Redirect("/swagger");
            });
        })
        .ExcludeFromDescription();

        return webApplication;
    }

    private static WebApplication GetGreetingsEndpoint(this WebApplication webApplication)
    {
        webApplication.MapGet("/greetings", () =>
        {
            return "Hello, user!";
        })
        .WithName("GetGreetings")
        .WithDescription("Say hello to user")
        .WithOpenApi();

        return webApplication;
    }
}
