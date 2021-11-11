namespace DatabaseLayerCore.MessageBus.Models
{
    public class DeleateMessage<T>
    {
        public T Message { get; set; }
    }
}