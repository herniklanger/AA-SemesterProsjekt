using System.Collections.Generic;
namespace InterfacesLib.Driver
{
    public interface IDriverModel <TContact>: IEntity<int> where TContact : IContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public List<TContact> contact { get; set; }
    }
}
