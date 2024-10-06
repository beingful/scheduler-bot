using Newtonsoft.Json;
using HttpClient = Bot.Api.Http.HttpClient;
using HttpRequest = Bot.Api.Http.HttpRequest;

namespace Bot.Api.APIs;

public abstract class ApiService;

public abstract class ApiService<TApiService> : ApiService
    where TApiService : ApiService
{
    private readonly HttpClient _httpClient;
    protected readonly ILogger Logger;

    public ApiService(IConfiguration configuration, ILogger<TApiService> logger, JsonSerializerSettings? serializerSettings = default)
    {
        _httpClient = new HttpClient(
            baseUrl: GetApiUrl(configuration),
            serializerSettings: serializerSettings);

        Logger = logger;
    }

    public async Task PostAsync(object request, string? endpoint = null,
        params (string Name, string Value)[] headers)
    {
        HttpRequest httpRequest = _httpClient.CreateRequest(endpoint);

        httpRequest.AddHeaders(headers);

        await httpRequest.PostWithContentAsync(request);
    }

    private static string GetApiUrl(IConfiguration configuration)
    {
        string apiName = typeof(TApiService).Name;

        return configuration.GetConnectionString(apiName)!;
    }
}
