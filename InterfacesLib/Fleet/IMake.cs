namespace InterfacesLib.Fleet
{
    public interface IMake : IEntity<int>
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
