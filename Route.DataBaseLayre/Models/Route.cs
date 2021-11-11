using System.Collections.Generic;
using System.Runtime.Serialization;
using InterfacesLib;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    public class Route : IEntity<int>

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location StartCheckpoint { get; set; }
        public Location EndCheckpoint { get; set; }
        [Reference]
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        [Reference]
        public List<RouteLocations> RouteLocationsList { get; set; }
        
        public string HistoryLocation { get; set; }

    }
}