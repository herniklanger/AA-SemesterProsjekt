using System.Collections.Generic;
using InterfacesLib;

namespace Route.DataBaseLayre.Models
{
    public class CustomerLocation : IEntity<int>
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public List<RouteLocations> KundeRoutes { get; set; }
        public Customer Customers { get; set; }
    }
}