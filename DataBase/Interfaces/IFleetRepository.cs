using Fleet.DataBaseLayre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fleet.Interfaces
{
    public interface IFleetRepository
    {
        public Task<IEnumerable<Vehicle>> GetByMake(string make, CancellationToken cancellationToken = new());

    }
}
