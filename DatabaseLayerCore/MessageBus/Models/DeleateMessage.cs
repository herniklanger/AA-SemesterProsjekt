namespace DatabaseLayerCore.MessageBus.Models
{
    public class DeleateMessage<T> where T: class
    {
        public T Message { get; set; }
    }
}