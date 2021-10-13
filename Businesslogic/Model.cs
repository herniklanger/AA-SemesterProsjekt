using InterfacesLib.Fleet;

namespace Fleet.Businesslogic
{
    public class Model : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
    }
}
