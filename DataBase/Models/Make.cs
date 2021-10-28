using InterfacesLib;
using InterfacesLib.Fleet;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fleet.DataBaseLayre.Models
{
	public class Make : IMake, IEntity<int>
	{
		[AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		[JsonIgnore]
		[Reference]
		public List<Vehicle>? Vehicles { get; set; }
	}
}
