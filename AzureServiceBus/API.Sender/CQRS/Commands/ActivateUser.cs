using API.Sender.Domain.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace API.Sender.CQRS.Commands
{
    public class ActivateUser
    {
        public record Command : IRequest<CommandResult>
        {
            [JsonIgnore]
            public int Id { get; init; }
            public string Email { get; init; }
        }
        
        public record CommandResult
        {
            public bool Success { get; init; }
        }

        public class CommandHandler : IRequestHandler<Command, CommandResult>
        {
            private readonly IUserService _userService;

            public CommandHandler(IUserService userService)
            {
                _userService = userService;
            }

            public Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
