namespace InterfacesLib.Driver
{
    public interface IContact: IEntity<int>
    {
        public int Id { get; set; }
        //public TDriver DriverId { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactType { get; set; }
    }
}
