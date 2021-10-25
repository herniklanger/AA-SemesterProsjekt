using InterfacesLib;
using InterfacesLib.Fleet;

namespace Fleet.DataBaseLayre.Models
{
    public class Make : IMake, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
