using System;
using Newtonsoft.Json;
using InterfacesLib;
using InterfacesLib.Fleet;
using ServiceStack.DataAnnotations;

namespace Fleet.DataBaseLayre.Models
{
	public class Vehicle : IVehicle<VehicleType, Make, Model>, IEntity<int>
	{
		[AutoIncrement]
		public int Id { get; set; }
		public string Vinnummer { get; set; }//ID
		public string Licenseplate { get; set; }
		public string ModelType { get; set; }
		public DateTime RegisterDate { get; set; }
		public decimal TotalKm { get; set; }
		[Reference]
		public VehicleType VehicleType { get; set; }
		[JsonIgnore]
		public int VehicleTypeId { get; set; }

		[Reference]
		public Make Make { get; set; }
		[JsonIgnore]
		public int MakeId { get; set; }

		[Reference]
		public Model Model { get; set; }
		[JsonIgnore]
		public int ModelId { get; set; }
	}
}
