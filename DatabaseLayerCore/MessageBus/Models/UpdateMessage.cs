namespace DatabaseLayerCore.MessageBus.Models
{
    public class UpdateMessage<T>
    {
        public T Message { get; set; }
    }
}