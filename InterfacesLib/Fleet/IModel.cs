namespace InterfacesLib.Fleet
{
    public interface IModel : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
    }
}
