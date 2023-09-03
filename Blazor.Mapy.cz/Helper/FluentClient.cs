using System.Text;
using System.Text.Json;
using Blazor.Mapy.cz.Models;

namespace Blazor.Mapy.cz.Helper;

public class FluentClient
{
    private readonly HttpClient _httpClient;
    
    private HttpRequestMessage _httpRequestMessage;
    private CancellationToken _cancellationToken = default;
    private Action<Exception>? _onErrorAction = null;
    private JsonSerializerOptions _serializationOptions = new ()
    {
        PropertyNameCaseInsensitive = true
    };
    
    private readonly ICollection<KeyValuePair<string,string>> _queryParameters = new List<KeyValuePair<string, string>>();

    public FluentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpRequestMessage = new HttpRequestMessage();
    }

    public FluentClient Get(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        return this;
    }
    
    public FluentClient Post(string url, object content)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }
        _httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);

        AddJsonContent(content);
        return this;
    }
    
    public FluentClient Put(string url, object content)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }
        _httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, url);

        AddJsonContent(content);
        return this;
    }
    
    public FluentClient Delete(string url, object content)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }
        _httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, url);

        AddJsonContent(content);
        return this;
    }
    
    public FluentClient AddJsonContent(object content)
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }
        
        _httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

        return this;
    }
    
    public FluentClient WithHeader(string name, string value)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }
        
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        
        _httpRequestMessage.Headers.Add(name, value);

        return this;
    }
    
    public FluentClient WithCancellationToken(CancellationToken cancellationToken)
    {
        this._cancellationToken = cancellationToken;

        return this;
    }
    
    public FluentClient OnError(Action<Exception> onErrorAction)
    {
        _onErrorAction = onErrorAction;

        return this;
    }
    
    public async Task<T> SendAsync<T>() where T : new()
    {
        var response = await Execute();
        if (response.IsSuccessStatusCode)
        {
            return await DeserializeResponse<T>(response.Content);
        }
        
        throw new HttpRequestException($"Request failed [{_httpRequestMessage.RequestUri}]", null, response.StatusCode);
    }
    
    public FluentClient AddQueryParameter(string name, string value)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }
        
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        
        _queryParameters.Add(new KeyValuePair<string, string>(name, value));

        return this;
    }

    private string? BuildQueryParams()
    {
        if (!_queryParameters.Any())
        {
            return null;
        }
        var index = 0;
        StringBuilder sb = new StringBuilder();
        foreach (var (key,value) in _queryParameters)
        {
            sb.Append(index == 0 ? "?" : "&");
            sb.Append($"{key}={value}");
            index++;
        }
        
        _httpRequestMessage.RequestUri = new Uri(_httpRequestMessage.RequestUri + sb.ToString(), UriKind.RelativeOrAbsolute);
        return sb.ToString();
    }

    private async Task<HttpResponseMessage> Execute()
    {
        if (_httpRequestMessage is null)
        {
            throw new ArgumentException("You must configure request first");
        }

        try
        {
            BuildQueryParams();
            var response = await _httpClient.SendAsync(_httpRequestMessage, _cancellationToken);
            return response;
        }
        catch (Exception e)
        {
            _onErrorAction?.Invoke(e);
            throw;
        }
        finally
        {
            _queryParameters.Clear();
        }
    }
    
    private async Task<TResponse> DeserializeResponse<TResponse>(HttpContent content) where TResponse : new()
    {
        await using(var responseStream = await content.ReadAsStreamAsync())
        {
            using var jsonDocument = await JsonDocument.ParseAsync(responseStream, cancellationToken: _cancellationToken);
            return jsonDocument.Deserialize<TResponse>(_serializationOptions) ?? new TResponse();
        }
    }
}