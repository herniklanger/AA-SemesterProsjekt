using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using InterfacesLib;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    public class Customer : IEntity<int>, ICustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Reference]
        [JsonIgnore]
        public List<Checkpoint> Locations { get; set; }
    }
}