using System.Security.Cryptography.X509Certificates;
using Blazor.Mapy.cz.Helper.Enums;
using Blazor.Mapy.cz.JsInterop;
using Blazor.Mapy.cz.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Mapy.cz.Components.Mapy;

public partial class Mapy
{
    [Inject] private SetUpMapJs SetUpMapJs { get; set; } = null!;
    
    [Inject] private SetMarkerJs SetMarkerJs { get; set; } = null!;

    [Parameter] 
    public MapyPoint Center { get; set; } = new (50.0870847, 14.4171357);
    
    [Parameter] 
    public double Zoom { get; set; } = 10;
    
    [Parameter]
    public RenderFragment Markers { get; set; }
    
    [Parameter]
    public RenderFragment Others { get; set; }
    
    public List<Marker> markers { get; set; } = new ();
    
    public Route? RouteData { get; set; }
    
    public IJSObjectReference? Map { get; set; }
    

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Load(true);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public void AddMarker(Marker marker)
    {
        markers.Add(marker);
    }
    
    public void SetRouteData(Route route)
    {
        RouteData = route;
    }

    public async Task Load(bool firstRender = false)
    {
       Map = await SetUpMapJs.SetUpMap(Center.Lat, Center.Lng, Zoom, firstRender);
       foreach (var marker in markers)
       { 
           await SetMarkerJs.SetMarker(Map, marker.Position.Lat, marker.Position.Lng, marker.Color);
       }

       if (RouteData is not null)
       {
           await RouteData.LoadRoute(Map);
       }
    }
}