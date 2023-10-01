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

# Use only service 
### For route response
```
 [Inject] private IRouteService RouteService { get; set; } = null!;

 var response = await RouteService.GetRoute(
      start,
      end,
      RouteType, 
      AvoidToll,
      wayPoints
      );
```

### For geocoding

```
[Inject] protected IGeocodeService GeocodeService { get; set; }
 var data = await GeocodeService.GetGeoCodeAsync(string input,  List<Type> types, uint limit = 15, CancellationToken cancellationToken = default);

```

#### Licence of Mapy cz.
- When using other functions (geocoding, route planning, etc…), you need to:

- Display the colored Mapy.cz logo with a white border - **Is already include in map.**

If you use only service you need
- The logo can be accompanied by the words “Powered by Mapy.cz”, “Hledají Mapy.cz”, “Plánují Mapy.cz”.
  ![image](https://github.com/KuchaD/Blazor.Mapycz/assets/32700410/b57721c2-5261-47f4-a5af-c69a5bfbb8f0)
- The logo must be placed near or directly within the search/planning dialog.
- The minimum height of the logo is 10px.
- The logo can be clickable, and the link should lead to https://mapy.cz/.


More 
https://developer.mapy.cz/en/rest-api-mapy-cz/atribution/

For API KEY
https://developer.mapy.cz/en/rest-api-mapy-cz/how-to-start/

    
