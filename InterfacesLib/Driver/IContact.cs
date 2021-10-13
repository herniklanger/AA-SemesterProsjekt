using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesLib.Driver
{
    public interface IContact<TDriver> where TDriver : IDriver
    {
        public int Id { get; set; }
        public TDriver DriverId { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactType { get; set; }
    }
}
