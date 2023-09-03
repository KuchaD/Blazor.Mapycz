using System.Web;
using Blazor.Mapy.cz.Helper;
using Blazor.Mapy.cz.Helper.Enums;
using Blazor.Mapy.cz.Models;
using Blazor.Mapy.cz.Options;
using Microsoft.Extensions.Options;
using Type = Blazor.Mapy.cz.Helper.Enums.Type;

namespace Blazor.Mapy.cz.Services;

public class GeocodeService : IGeocodeService
{
    private readonly FluentClient _fluentClient;
    private readonly MapyCzOptions _options;

    public GeocodeService(HttpClient client, IOptions<MapyCzOptions> options)
    {
        _options = options.Value;
        _fluentClient = new FluentClient(client);
    }

    private FluentClient BaseClient(string input, uint limit = 15, CancellationToken cancellationToken = default)
    {
        return _fluentClient
            .Get("/v1/geocode")
            .AddQueryParameter(QueryParams.Apikey, _options.ApiKey)
            .AddQueryParameter(GeoCodingQueryParams.Limit, limit.ToString())
            .AddQueryParameter(QueryParams.Lang, _options.Lang)
            .AddQueryParameter(GeoCodingQueryParams.Search, input)
            .WithCancellationToken(cancellationToken);
    }
    
    public async Task<GeoCodeResponse> GetGeoCodeAsync(string input, uint limit = 15, CancellationToken cancellationToken = default)
    {
       return await BaseClient(input, limit, cancellationToken: cancellationToken)
                .AddQueryParameter(GeoCodingQueryParams.Type, Type.RegionalSubType.Municipality)
                .AddQueryParameter(GeoCodingQueryParams.Type, Type.RegionalSubType.MunicipalityPart)
                .AddQueryParameter(GeoCodingQueryParams.Type, Type.RegionalSubType.Street)
                .AddQueryParameter(GeoCodingQueryParams.Type, Type.RegionalSubType.Address)
                .SendAsync<GeoCodeResponse>();
    }
    
    public async Task<GeoCodeResponse> GetGeoCodeAsync(string input, List<Type> types, uint limit = 15, CancellationToken cancellationToken = default)
    {
        var client = BaseClient(input, limit, cancellationToken: cancellationToken);
        foreach (var type in types)
        {
            client.AddQueryParameter(GeoCodingQueryParams.Type, type);
        }

        return await client.SendAsync<GeoCodeResponse>();
    }
}