using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using SoftwareManagement.ApiClient.Entities.Common.Enums;
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient;
public abstract class HttpEngine : IDisposable
{
    private static HttpClient httpClient;
    private static readonly TimeSpan httpTimeout = new TimeSpan(0, 0, 30);
    protected internal static HttpClient HttpClient
    {
        get
        {
            if (HttpEngine.httpClient == null)
            {
                var httpClientHandler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    CookieContainer = new CookieContainer()
                };

                HttpEngine.httpClient = new HttpClient(httpClientHandler)
                {
                    Timeout = HttpEngine.httpTimeout
                };

            }

            return HttpEngine.httpClient;
        }
        set => HttpEngine.httpClient = value;
    }
    public virtual void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        HttpEngine.HttpClient?.Dispose();
        HttpEngine.httpClient = null;
    }

}
public sealed class HttpEngine<TRequest, TResponse> : HttpEngine
        where TRequest : IRequest, new()
        where TResponse : IResponse, new()
{
    internal static readonly HttpEngine<TRequest, TResponse> instance = new HttpEngine<TRequest, TResponse>();

    private static readonly JsonSerializerOptions jOptions = new JsonSerializerOptions
    {
        Converters = { new JsonStringEnumConverter(), new JsonDateOnlyConverter() },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<TResponse> QueryAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpMessage;
        
        try
        {
            httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new TResponse { Status = Status.HttpError, ErrorMessage = ex.GetBaseException().Message };
        }
         

        var response = await ProcessResponseAsync(httpMessage).ConfigureAwait(false);

        return response;

    }
    internal async Task<HttpResponseMessage> ProcessRequestAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var uri = request.GetUri();

        if (request is IRequestGet)
        {
            return await HttpEngine.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
        }

        HttpMethod method = request switch
        {
            IRequestPost => HttpMethod.Post,
            IRequestCreate => HttpMethod.Post,
            IRequestUpdate => HttpMethod.Put,
            IRequestDelete => HttpMethod.Delete,
            _ => throw new NotImplementedException()
        };

        using StringContent content = new StringContent(JsonSerializer.Serialize(request, jOptions), System.Text.Encoding.UTF8, "application/json");
        using HttpRequestMessage requestMessage = new HttpRequestMessage(method, uri) { Content = content };

        return await HttpEngine.HttpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
    }
    internal async Task<TResponse> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        if (httpResponse == null)
            throw new ArgumentNullException(nameof(httpResponse));

        using (httpResponse)
        {
            var response = new TResponse();

            

            if (httpResponse.IsSuccessStatusCode)
            {

                response = await JsonSerializer.DeserializeAsync<TResponse>(await httpResponse.Content.ReadAsStreamAsync(), jOptions).ConfigureAwait(false);
                if (response == null)
                    throw new NullReferenceException(nameof(response));              

            }
            else
            {
                response.Status = Status.HttpError;
                response.ErrorMessage = httpResponse.ReasonPhrase;
            }

            return response;
        }
    }


}
