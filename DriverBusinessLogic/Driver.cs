using InterfacesLib.Driver;
using System;

namespace Driver.BusinessLogic
{
	public class Driver : IDriver
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Role { get; set; }
		public string Email { get; set; }
	}
}
