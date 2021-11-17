using System.Collections.Generic;
using System.Runtime.Serialization;
using InterfacesLib;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    public class Route : IEntity<int>

    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Ignore]
        public Location StartCheckpoint { get; set; }
        [JsonIgnore]
        public decimal StartCheckpointX
        {
            get { return StartCheckpoint.X; }
            set
            {
                var startCheckpoint = StartCheckpoint;
                startCheckpoint.X = value;
                StartCheckpoint = startCheckpoint;
            } 
        }
        [JsonIgnore]
        public decimal StartCheckpointY
        {
            get { return StartCheckpoint.Y; }
            set
            {
                var startCheckpoint = StartCheckpoint;
                startCheckpoint.Y = value;
                StartCheckpoint = startCheckpoint;
            }
        }
        [Ignore]
        public Location EndCheckpoint { get; set; }
        
        [JsonIgnore]
        public decimal EndCheckpointX
        {
            get { return EndCheckpoint.X; }
            set
            {
                var endCheckpoint = EndCheckpoint;
                endCheckpoint.X = value;
                EndCheckpoint = endCheckpoint;
            }
        }
        [JsonIgnore]
        public decimal EndCheckpointY
        {
            get { return EndCheckpoint.Y; }
            set
            {
                var endCheckpoint = EndCheckpoint;
                endCheckpoint.Y = value;
                EndCheckpoint = endCheckpoint;
            }
        }
        [Reference]
        public Vehicle Vehicle { get; set; }
        
        [Ignore]
        public List<Checkpoint> Checkpoint { get; set; }
        [JsonIgnore]
        public int VehicleId { get; set; }
        
        [Reference]
        [JsonIgnore]
        public List<RouteLocations> RouteLocationsList { get; set; }
        
        public string HistoryLocation { get; set; }

    }
}