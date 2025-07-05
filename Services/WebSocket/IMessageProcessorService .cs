namespace WebSocket_API.Services.WebSocket
{
    public interface IMessageProcessorService
    {
        Task ProcessMessageAsync(string json);

    }
}
