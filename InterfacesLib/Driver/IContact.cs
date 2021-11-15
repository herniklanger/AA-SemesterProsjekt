namespace InterfacesLib.Driver
{
    public interface IContact<TDriver> where TDriver : IDriverModel
    {
        public int Id { get; set; }
        public TDriver DriverId { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactType { get; set; }
    }
}
