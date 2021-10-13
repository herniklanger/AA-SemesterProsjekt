using InterfacesLib.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverBusinessLogic
{
    public class Contact : IContact<Driver>
    {
        public int Id { get; set; }
        public Driver DriverId { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactType { get; set; }
    }
}
