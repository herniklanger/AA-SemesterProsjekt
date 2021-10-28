using System;
using System.Text.Json.Serialization;
using InterfacesLib;
using InterfacesLib.Fleet;
using ServiceStack.DataAnnotations;

namespace Fleet.DataBaseLayre.Models
{
	public record Vehicle : IVehicle<VehicleType, Make, Model>, IEntity<int>
	{
		[AutoIncrement]
		public int Id { get; set; }
		public string Vinnummer { get; set; }//ID
		public string Licenseplate { get; set; }
		public string ModelType { get; set; }
		public DateTime RegisteringsDate { get; set; }
		public decimal DrivedKm { get; set; }
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
