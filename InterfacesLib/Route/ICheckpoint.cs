using InterfacesLib;
using System.Collections.Generic;

namespace InterfacesLib.Route

{
    public interface ICheckpoint <TCustomer, TRoute> 
        where TCustomer : ICustomer 
        where TRoute : IRoute
    {
        TCustomer Customer { get; set; }
        int CustomerId { get; set; }
        int Id { get; set; }
        Location Location { get; set; }
        List<TRoute> Routes { get; set; }
    }
}