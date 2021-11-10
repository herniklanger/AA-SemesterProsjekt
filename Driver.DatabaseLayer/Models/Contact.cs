using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesLib;
using InterfacesLib.Driver;
using ServiceStack.DataAnnotations;

namespace Driver.DatabaseLayer.Models
{
    public class Contact : IContact<DriverModel>, IEntity<int>
	{
		[AutoIncrement]
		public int Id { get; set; }

		[Reference]
		public DriverModel DriverId { get; set; }
		public string PhoneNumber { get; set; }
		public string ContactType { get; set; }

	}


}
