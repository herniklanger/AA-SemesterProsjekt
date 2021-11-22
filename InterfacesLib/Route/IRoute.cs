using InterfacesLib;
using System.Collections.Generic;

namespace InterfacesLib.Route
{
    public interface IRoute <TCheckpoint, TVehicle> : IEntity<int>
        where TCheckpoint : ICheckpoint 
        where TVehicle : IVehicle
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