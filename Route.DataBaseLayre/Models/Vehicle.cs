using InterfacesLib;

namespace Route.DataBaseLayre.Models
{
    public class Vehicle : IEntity<int>, IVehicle
    {
        public int Id { get; set; }
    }
}