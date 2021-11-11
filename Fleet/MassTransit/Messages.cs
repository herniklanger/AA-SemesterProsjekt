using System.Threading.Tasks;
using MassTransit;

namespace Fleet.DataBaseLayre.Models.MessageBus
{
    public class MessagesConsumer:IConsumer<Vehicle>
    {
        public Task Consume(ConsumeContext<Vehicle> context)
        {
            throw new System.NotImplementedException();
        }
    }
}