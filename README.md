# Blazor.Mapycz

Componets for manipulatin with mapy. cz

# Init Program.cs

```
builder.Services.AddMapyCz(opt =>
{
    opt.ApiKey = "MAPY.CZ API KEY";
});
```

_Host.cshtml
Index.html

```
  @Html.Raw(MapySetup.RenderMaplibre())

or

 <script src='https://unpkg.com/maplibre-gl@latest/dist/maplibre-gl.js'></script>
 <link href='https://unpkg.com/maplibre-gl@latest/dist/maplibre-gl.css' rel='stylesheet' />
      
```

## Example 

```
<Mapy Center=@(new MapyPoint(49.8290669, 18.2534053))>
    <Markers>
        <Marker Position=@(new MapyPoint(49.8290669, 18.2534053) Color="#FFFFFF")></Marker>
        <Marker Position=@(new MapyPoint(49.7813447, 18.2601403))></Marker>
    </Markers>
    
    <Others>
        <Route RouteType="RouteType.FootFast" OnRouteCalculated="RouteRecalculate">
            <RoutePoint Position=@(new MapyPoint(49.8290669, 18.2534053))></RoutePoint>
            <RoutePoint Position="@(new MapyPoint(49.8044000, 18.3012675))" Order=0></RoutePoint>
            <RoutePoint Position=@(new MapyPoint(49.7813447, 18.2601403))></RoutePoint>
        </Route>
    </Others>
</Mapy> 

Distance in m = @distance m
Time @time

@code {

    private double distance;
    TimeSpan time;
    
    void RouteRecalculate(RouteCallbackValue arg)
    {
        distance = arg.Length;
        time = TimeSpan.FromSeconds(arg.Duration);
    }
}
```
