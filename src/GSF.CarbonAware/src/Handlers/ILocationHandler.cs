using GSF.CarbonAware.Models;

namespace GSF.CarbonAware.Handlers;

public interface ILocationHandler
{
    Task<IDictionary<string, Location>> GetAllLocationsAsync();
}
