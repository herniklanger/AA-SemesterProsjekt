using InterfacesLib;
using System.Collections.Generic;

namespace Route.DataBaseLayre.Models
{
    public interface IRoute
    {
        List<Checkpoint> Checkpoint { get; set; }
        string HistoryLocation { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        Location StartCheckpoint { get; set; }
        Vehicle Vehicle { get; set; }
        int VehicleId { get; set; }
    }
}