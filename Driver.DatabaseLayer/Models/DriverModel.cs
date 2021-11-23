using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesLib.Driver;
using InterfacesLib;
using ServiceStack.DataAnnotations;

namespace Driver.DatabaseLayer.Models
{
    public class DriverModel : IDriverModel<Contact>, IEntity<int>
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        [Reference]
        public List<Contact> Contacts { get; set ; }
    }
}
