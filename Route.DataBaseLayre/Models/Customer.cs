using System.Collections.Generic;
using InterfacesLib;

namespace Route.DataBaseLayre.Models
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CustomerLocation> Locations  { get; set; } 
    }
}