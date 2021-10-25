using InterfacesLib;
using InterfacesLib.Fleet;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoreLib.Models
{
	public class Model : IModel, IEntity<int>
	{
		[AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Variant { get; set; }
		[JsonIgnore]
		[Reference]
		public List<Vehicle> Vehicles { get; set; }
	}
}
