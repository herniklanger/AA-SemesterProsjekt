namespace InterfacesLib.Driver
{
    public interface IDriverModel : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
