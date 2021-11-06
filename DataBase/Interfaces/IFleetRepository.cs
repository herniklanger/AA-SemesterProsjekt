using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;

namespace Fleet.DataBaseLayre.Interfaces
{
    public interface IFleetRepository
    {
        public Task<IEnumerable<Vehicle>> GetByMake(string make, CancellationToken cancellationToken = new());
    }
}
