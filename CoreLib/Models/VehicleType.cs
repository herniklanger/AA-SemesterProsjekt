using InterfacesLib;
using InterfacesLib.Fleet;

namespace CoreLib.Models
{
    public class VehicleType : IVehicleType, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
