using System.Collections.Generic;
using InterfacesLib;

namespace Route.DataBaseLayre.Models
{
    public class Route : IEntity<int>

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }
        public string HistoryLocation { get; set; }
        public Vehicle Vehicle { get; set; }
        public List<RouteLocations> RouteLocationsList { get; set; }

    }
}