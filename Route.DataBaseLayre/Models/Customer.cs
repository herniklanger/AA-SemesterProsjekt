using System.Collections.Generic;
using InterfacesLib;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [Reference]
        public List<Checkpoint> Locations  { get; set; } 
    }
}