namespace API.Sender.Domain.Services
{
    public interface IAzureServiceBusService
    {
        Task SendMessage(int id, string email);
        Task ReceiveMessage();
    }
}
