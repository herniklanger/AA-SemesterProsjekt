using System.Collections.Generic;
using InterfacesLib;
using InterfacesLib.Route;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    public class Checkpoint : IEntity<int>, ICheckpoint<Customer, Vehicle, Route, Checkpoint>
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Ignore]
        public Location Location { get; set; }
        [JsonIgnore]
        public decimal LocationX
        {
            get
            {
                return Location.X;
            }
            set
            {
                var location = Location;
                location.X = value;
                Location = location;
            }
        }

        [JsonIgnore]
        public decimal LocationY
        {
            get
            {
                return Location.Y;
            }
            set
            {
                var location = Location;
                location.Y = value;
                Location = location;
            }
        }
        [Ignore]
        public List<Route> Routes { get; set; }
        [Reference]
        [JsonIgnore]
        public List<RouteLocations> RouteLocations { get; set; }
        [Reference]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}