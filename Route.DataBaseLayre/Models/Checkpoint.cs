using System.Collections.Generic;
using System.Runtime.Serialization;
using InterfacesLib;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    public class Checkpoint : IEntity<int>
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        [IgnoreDataMember]
        public  List<Route> Routes { get; set; }
        [Reference]
        public List<RouteLocations> RouteLocations { get; set; }
        [Reference]
        public Customer Customers { get; set; }
        public int CustomersId { get; set; }
    }
}