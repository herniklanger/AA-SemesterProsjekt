using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Driver.DatabaseLayer.Models;

namespace Driver.DatabaseLayer.Interfaces
{
    public interface IDriverRepository
    {
        public Task<IEnumerable<DriverModel>> GetByContactType(string contactType, CancellationToken cancellationToken = new());
        public Task<IEnumerable<DriverModel>> GetByName(string contactType, CancellationToken cancellationToken = new());
    }
}
