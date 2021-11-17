using System.Threading.Tasks;
using Fleet.DataBaseLayre.Models;
using MassTransit;

namespace Fleet.MassTransit
{
    public class MessagesConsumer:IConsumer<Vehicle>
    {
        public Task Consume(ConsumeContext<Vehicle> context)
        {
            throw new System.NotImplementedException();
        }
    }
}