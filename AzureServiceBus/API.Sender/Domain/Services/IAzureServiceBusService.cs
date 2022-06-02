using API.Sender.Domain.Models;

namespace API.Sender.Domain.Services
{
    public interface IAzureServiceBusService
    {
        Task SendMessage(User user);
    }
}
