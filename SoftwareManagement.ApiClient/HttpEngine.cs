﻿using System.Net;
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


        JsonSerializerOptions jOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        HttpMethod method = request switch
        {
            IRequestCreate => HttpMethod.Post,
            IRequestUpdate => HttpMethod.Put,
            IRequestDelete => HttpMethod.Delete,
            _ => throw new NotImplementedException()
        };

        using var stream = new MemoryStream();
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, request);
        using StreamContent streamContent = new StreamContent(stream);
        using HttpRequestMessage requestMessage = new HttpRequestMessage(method, uri) { Content = streamContent };

        return await HttpEngine.HttpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
    }
    internal async Task<TResponse> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        if (httpResponse == null)
            throw new ArgumentNullException(nameof(httpResponse));

        using (httpResponse)
        {
            httpResponse.EnsureSuccessStatusCode();

            var response = new TResponse();

            var rawJson = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            JsonSerializerOptions jOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            response = JsonSerializer.Deserialize<TResponse>(rawJson, jOptions);

            if (response == null)
                throw new NullReferenceException(nameof(response));

            response.Status = httpResponse.IsSuccessStatusCode
                ? response.Status ?? Status.Ok
                : Status.HttpError;

            return response;
        }
    }


}
