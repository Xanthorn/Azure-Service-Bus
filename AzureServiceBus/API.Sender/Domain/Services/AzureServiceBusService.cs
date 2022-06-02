using API.Sender.Domain.Models;
using API.Sender.Validators;
using Azure.Messaging.ServiceBus;

namespace API.Sender.Domain.Services
{
    public class AzureServiceBusService : IAzureServiceBusService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;
        private readonly ServiceBusProcessor _processor;
        private readonly IUserService _userService;

        public AzureServiceBusService(IConfiguration configuration, IUserService userService)
        {
            string connectionString = configuration.GetConnectionString("AzureServiceBus");
            string queueName = configuration.GetSection("AzureServiceBus:QueueName").Value;
            _client = new(connectionString);
            _sender = _client.CreateSender(queueName);
            _processor = _client.CreateProcessor(queueName);
            _userService = userService;
        }

        public async Task SendMessage(User user)
        {
            ServiceBusMessage message = new($"{user.Id},{user.Email},{user.FirstName},{user.LastName},{user.Age}");

            await _sender.SendMessageAsync(message);
        }

        public async Task ReceiveMessage()
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            await Task.Delay(TimeSpan.FromMinutes(5));

            await _processor.StopProcessingAsync();
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            string[] userProperties = body.Split(",");

            bool userShouldBeActivated = true;

            for (int i = 1; i < userProperties.Length; i++)
            {
                userShouldBeActivated = string.IsNullOrEmpty(userProperties[i]);
                if (!userShouldBeActivated)
                {
                    return;
                }
            }

            userShouldBeActivated = EmailValidator.Validate(userProperties[1]);

            if (userShouldBeActivated)
            {
                await _userService.ActivateUser(int.Parse(userProperties[0]));
            }

            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
