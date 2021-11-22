namespace Route.DataBaseLayre.Models
{
    public interface IRouteLocations
    {
        Checkpoint Checkpoint { get; set; }
        int CheckpointId { get; set; }
        int Id { get; set; }
        Route Route { get; set; }
        int RouteId { get; set; }
    }
}