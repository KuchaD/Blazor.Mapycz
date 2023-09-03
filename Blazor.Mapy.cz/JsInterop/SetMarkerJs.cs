using Blazor.Mapy.cz.Options;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Blazor.Mapy.cz.JsInterop;

public class SetMarkerJs : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public SetMarkerJs(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Blazor.Mapy.cz/MaplibreglJsInterop.js").AsTask());
    }

    public async ValueTask<IJSObjectReference> SetMarker(object map,double lat, double lng, string color)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<IJSObjectReference>("SetMarker", map, lat, lng, color);
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