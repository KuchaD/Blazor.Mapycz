using Blazor.Mapy.cz.Models;
using Type = Blazor.Mapy.cz.Helper.Enums.Type;

namespace Blazor.Mapy.cz.Services;

public interface IGeocodeService
{
    public Task<GeoCodeResponse> GetGeoCodeAsync(string input, uint limit = 15, CancellationToken cancellationToken = default);
    
    public Task<GeoCodeResponse> GetGeoCodeAsync(string input,  List<Type> types, uint limit = 15, CancellationToken cancellationToken = default);
}