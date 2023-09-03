using Blazor.Mapy.cz.Options;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Blazor.Mapy.cz.JsInterop;

public class SetUpMapJs : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private readonly IOptions<MapyCzOptions> _options;

    public SetUpMapJs(IJSRuntime jsRuntime, IOptions<MapyCzOptions> options)
    {
        _options = options;
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Blazor.Component.Mapycz/MaplibreglJsInterop.js").AsTask());
    }

    public async ValueTask<IJSObjectReference> SetUpMap(double lat, double lng)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<IJSObjectReference>("SetMap", lat, lng, _options.Value.ApiKey);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}