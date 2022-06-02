using API.Sender.Domain.Services;

namespace API.Sender.Receivers
{
    public class AzureServiceBusReceiver
    {
        private readonly IAzureServiceBusService _azureServiceBusService;

        public AzureServiceBusReceiver(IAzureServiceBusService azureServiceBusService)
        {
            _azureServiceBusService = azureServiceBusService;
        }

        public async Task<Action> ReceiveMessage()
        {
            while (true)
            {
                await _azureServiceBusService.ReceiveMessage();
                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
    }
}
