namespace InterfacesLib.Route
{
    public interface IRouteLocations <TCheckpoint, TRoute> 
        where TCheckpoint : ICheckpoint 
        where TRoute : IRoute
    {
        Checkpoint Checkpoint { get; set; }
        int CheckpointId { get; set; }
        int Id { get; set; }
        Route Route { get; set; }
        int RouteId { get; set; }
    }
}