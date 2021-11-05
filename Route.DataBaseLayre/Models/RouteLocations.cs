using InterfacesLib;

namespace Route.DataBaseLayre.Models
{
    /// <summary>
    /// Route Location List
    /// </summary>
    public class RouteLocations : IEntity<int>
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public int RouteId { get; set; }
        
        public CustomerLocation CustomerLocation { get; set; }
        public int CustomerLocationId { get; set; }
    }
}