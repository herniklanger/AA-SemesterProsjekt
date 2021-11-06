using InterfacesLib;
using InterfacesLib.Fleet;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fleet.DataBaseLayre.Models
{
	public class Model : IModel, IEntity<int>
	{
		[AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Variant { get; set; }
		[JsonIgnore]
		[Reference]
		public List<Vehicle>? Vehicles { get; set; }
	}
}
