using InterfacesLib;

namespace Route.DataBaseLayre.Models
{
    public class Vehicle : IEntity<int>
    {
        public int Id {get; set;}
    }
}