namespace DatabaseLayerCore.MessageBus.Models
{
    public class CreateMessage<T> where T: class
    {
        public T Message { get; set; }
    }
}