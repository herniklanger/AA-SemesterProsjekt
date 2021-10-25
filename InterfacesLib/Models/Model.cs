﻿using InterfacesLib;
using InterfacesLib.Fleet;

namespace Fleet.DataBaseLayre.Models
{
    public class Model : IModel, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
    }
}
