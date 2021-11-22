using InterfacesLib;
using ServiceStack.DataAnnotations;

namespace Route.DataBaseLayre.Models
{
    /// <summary>
    /// Route Location List
    /// </summary>
    public class RouteLocations : IEntity<int>, IRouteLocations
    {
        [AutoIncrement]
        public int Id { get; set; }
        [IgnoreAttribute]
        public Route Route { get; set; }
        public int RouteId
        {
            get { return Route?.Id ?? 0; }
            set
            {
                if (Route == null)
                    Route = new Route() { Id = value };
                else
                    Route.Id = value;
            }
        }
        [IgnoreAttribute]
        public Checkpoint Checkpoint { get; set; }
        public int CheckpointId
        {
            get { return Checkpoint?.Id ?? 0; }
            set
            {
                if (Checkpoint == null)
                    Checkpoint = new Checkpoint { Id = value };
                else
                    Checkpoint.Id = value;
            }
        }
    }
}