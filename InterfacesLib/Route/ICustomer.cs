namespace InterfacesLib.Route
{
    public interface ICustomer : IEntity<int>
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}