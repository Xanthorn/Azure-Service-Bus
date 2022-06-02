using API.Sender.Domain.Models;
using API.Sender.Domain.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace API.Sender.CQRS.Commands
{
    public class UpdateUser
    {
        public record Command : IRequest<CommandResult>
        {
            [JsonIgnore]
            public int Id { get; init; }
            public string? Email { get; init; }
            public string? FirstName { get; init; }
            public string? LastName { get; init; }
            public int? Age { get; init; }
        }

        public record CommandResult
        {
            public User User { get; init; }
        }

        public class CommandHandler : IRequestHandler<Command, CommandResult>
        {
            private readonly IUserService _userService;
            private readonly IAzureServiceBusService _azureServiceBusService;

            public CommandHandler(IUserService userService, IAzureServiceBusService azureServiceBusService)
            {
                _userService = userService;
                _azureServiceBusService = azureServiceBusService;
            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userService.UpdateUser(request);

                await _azureServiceBusService.SendMessage(user);

                return new CommandResult { User = user };
            }
        }
    }
}
