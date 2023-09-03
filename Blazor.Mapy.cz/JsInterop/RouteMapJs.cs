using Blazor.Mapy.cz.Models;
using Blazor.Mapy.cz.Options;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Blazor.Mapy.cz.JsInterop;

public class RouteMapJs : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public RouteMapJs(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Blazor.Component.Mapycz/MaplibreglJsInterop.js").AsTask());
    }

    public async ValueTask<IJSObjectReference> RouteMap(IJSObjectReference map, GeometryType geometry)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<IJSObjectReference>("RouteMap", map, geometry);
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