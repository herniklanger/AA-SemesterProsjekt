using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesLib.Fleet
{
    public interface IVehicleType : IEntity<int>
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
