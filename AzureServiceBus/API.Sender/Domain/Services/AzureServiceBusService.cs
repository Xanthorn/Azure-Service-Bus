using API.Sender.Domain.Models;
using Azure.Messaging.ServiceBus;

namespace API.Sender.Domain.Services
{
    public class AzureServiceBusService : IAzureServiceBusService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public AzureServiceBusService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("AzureServiceBus");
            string queueName = configuration.GetSection("AzureServiceBus:QueueName").Value;
            _client = new(connectionString);
            _sender = _client.CreateSender(queueName);
        }

        public async Task ReceiveMessage()
        {
            throw new NotImplementedException();
        }

        public async Task SendMessage(User user)
        {
            ServiceBusMessage message = new($"{user.Id},{user.Email},{user.FirstName},{user.LastName},{user.Age}");

            await _sender.SendMessageAsync(message);
        }
    }
}
