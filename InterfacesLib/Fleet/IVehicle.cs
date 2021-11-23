using System;

namespace InterfacesLib.Fleet
{
    public interface IVehicle<TVehicleType, TMake, TModel>
	    where TMake : IMake 
	    where TModel : IModel 
	    where TVehicleType : IVehicleType
    {
        string Vinnummer { get; set; }
        string Licenseplate { get; set; }
        string ModelType { get; set; }
        DateTime RegisterDate { get; set; }
        decimal TotalKm { get; set; }
        TVehicleType VehicleType { get; set; }
        TMake Make { get; set; }        
        TModel Model { get; set; }
    }
}
