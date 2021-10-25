using InterfacesLib;
using InterfacesLib.Fleet;

namespace Fleet.DataBaseLayre.Models
{
    public class VehicleType : IVehicleType, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
