using GSF.CarbonAware.Models;
using CarbonAware.Interfaces;


namespace GSF.CarbonAware.Handlers;

internal sealed class LocationHandler : ILocationHandler
{
    private readonly ILocationSource _locationSource;

    public LocationHandler(ILocationSource source)
    {
        _locationSource = source;
    }
 
    public async Task<IDictionary<string, Location>> GetAllLocationsAsync()
    {
       var allLocs = await _locationSource.GetAllGeopositionLocationsAsync();
       var dictionary = new Dictionary<string, Location>();
       foreach (KeyValuePair<string, global::CarbonAware.Model.Location> elem in allLocs)
       {
            dictionary.Add(elem.Key, (Location) elem.Value);
       }
       return dictionary;
    }
}
