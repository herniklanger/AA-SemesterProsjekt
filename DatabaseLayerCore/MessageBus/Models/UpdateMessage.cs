namespace DatabaseLayerCore.MessageBus.Models
{
    public class UpdateMessage<T> where T: class
    {
        public T Message { get; set; }
    }
}