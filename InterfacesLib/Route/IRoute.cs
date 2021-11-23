using InterfacesLib;
using System.Collections.Generic;

namespace InterfacesLib.Route
{
    public interface IRoute <TCheckpoint, TVehicle, TCustomer,TRoute> : IEntity<int>
        where TCustomer : ICustomer
        where TVehicle : IVehicle
        where TCheckpoint : ICheckpoint<TCustomer, TVehicle, TRoute, TCheckpoint>
        where TRoute : IRoute<TCheckpoint, TVehicle, TCustomer,TRoute>
    {
        List<TCheckpoint> Checkpoint { get; set; }
        string HistoryLocation { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        Location StartCheckpoint { get; set; }
        TVehicle Vehicle { get; set; }
        int VehicleId { get; set; }
    }
}