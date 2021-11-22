using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesLib;
using InterfacesLib.Driver;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace Driver.DatabaseLayer.Models
{
    public class Contact : IContact, IEntity<int>
	{
		[AutoIncrement]
		public int Id { get; set; }
		public string PhoneNumber { get; set; }
		public string ContactType { get; set; }
        public int DriverModelId { get; set; }

        [Ignore]
        public DriverModel? drivers { get; set; }
    }


}
