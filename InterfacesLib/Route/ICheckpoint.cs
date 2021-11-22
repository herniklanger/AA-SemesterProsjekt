using InterfacesLib;
using System.Collections.Generic;

namespace InterfacesLib.Route

{
    public interface ICheckpoint<TCustomer, TVehicle, TRoute, TCheckpoint>
        where TCustomer : ICustomer
        where TVehicle : IVehicle
        where TRoute : IRoute<TCheckpoint, TVehicle, TCustomer, TRoute>
        where TCheckpoint : ICheckpoint<TCustomer, TVehicle, TRoute, TCheckpoint>
    {
    TCustomer Customer { get; set; }
    int CustomerId { get; set; }
    int Id { get; set; }
    Location Location { get; set; }
    List<TRoute> Routes { get; set; }
    }
}