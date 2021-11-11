namespace DatabaseLayerCore.MessageBus.Models
{
    public class CreateMessage<T>
    {
        public T Message { get; set; }
    }
}