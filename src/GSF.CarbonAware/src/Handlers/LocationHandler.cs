using GSF.CarbonAware.Models;
using CarbonAware.Interfaces;


namespace GSF.CarbonAware.Handlers;

internal sealed class LocationHandler : ILocationHandler
{
    private readonly ILocationSource _locationSource;

    private IDictionary<string, Location> _allLocations;

    public LocationHandler(ILocationSource source)
    {
        _locationSource = source;
        _allLocations = new Dictionary<string, Location>();
    }
 
    public async Task<IDictionary<string, Location>> GetAllLocationsAsync()
    {
        if (!_allLocations.Any())
        {
            foreach (KeyValuePair<string, global::CarbonAware.Model.Location> elem in  await _locationSource.GetAllGeopositionLocationsAsync())
            {
                _allLocations.Add(elem.Key, (Location) elem.Value);
            }
        }
       return _allLocations;
    }
}
