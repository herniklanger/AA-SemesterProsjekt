using InterfacesLib;
using InterfacesLib.Fleet;

namespace CoreLib.Models
{
    public class Make : IMake, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
