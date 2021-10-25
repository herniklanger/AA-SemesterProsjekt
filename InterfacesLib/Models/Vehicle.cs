using System;
using InterfacesLib;
using InterfacesLib.Fleet;

namespace Fleet.DataBaseLayre.Models
{
    public class Vehicle : IVehicle<VehicleType, Make, Model>, IEntity<int>
    {
        public int Id { get; set; }
        public string Vinnummer { get; set; }//ID
        public string Licenseplate { get; set; }
        public string ModelType { get; set; }
        public DateTime RegisteringsDate { get; set; }
        public decimal DrivedKm { get; set; }
        public VehicleType VehicleType { get; set; }
        public Make Make { get; set; }
        public Model Model { get; set; }
    }
}
