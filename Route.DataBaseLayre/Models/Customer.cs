using System.Collections.Generic;
using System.Text.Json.Serialization;
using InterfacesLib;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [Reference]
        [JsonIgnore]
        public List<Checkpoint> Locations  { get; set; } 
    }
}