namespace Slack.Bot.Api.Endpoints;

public static class HomeEndpoints
{
    public static void MapHomeEndpoints(this WebApplication webApplication)
    {
        webApplication.GetSwaggerStartPage();
        webApplication.GetGreetingsEndpoint();
    }

    private static void GetSwaggerStartPage(this WebApplication webApplication)
    {
        webApplication.MapGet("/", context =>
        {
            return Task.Run(() =>
            {
                context.Response.Redirect("/swagger");
            });
        })
        .ExcludeFromDescription();
    }

    private static void GetGreetingsEndpoint(this WebApplication webApplication)
    {
        webApplication.MapGet("/greetings", () =>
        {
            return "Hello, user!";
        })
        .WithName("GetGreetings")
        .WithOpenApi(); ;
    }
}
