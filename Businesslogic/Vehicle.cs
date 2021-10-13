using InterfacesLib;
using System;

namespace Businesslogic
{
    public class Vehicle : IVehicle<VehicleType, Make, Model>
    {
        public string Vinnummer { get; set; }//ID
        public string Nummerplade { get; set; }
        public string ModelType { get; set; }
        public DateTime RegisteringsDate { get; set; }
        public decimal DrivedKm { get; set; }
        public VehicleType VehicleType { get; set; }
        public Make Make { get; set; }
        public Model Model { get; set; }
    }
}
