namespace InterfacesLib.Route
{
    public interface IRouteLocations <TCheckpoint, TVehicle, TCustomer, TRoute> 
        where TVehicle : IVehicle
        where TCustomer : ICustomer
        where TCheckpoint : ICheckpoint<TCustomer, TVehicle, TRoute, TCheckpoint>
        where TRoute : IRoute <TCheckpoint, TVehicle, TCustomer,TRoute>
    {
        TCheckpoint Checkpoint { get; set; }
        int CheckpointId { get; set; }
        int Id { get; set; }
        TRoute Route { get; set; }
        int RouteId { get; set; }
    }
}