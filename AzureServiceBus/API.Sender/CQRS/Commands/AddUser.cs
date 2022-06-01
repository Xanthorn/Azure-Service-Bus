using API.Sender.Domain.Models;
using API.Sender.Domain.Services;
using MediatR;

namespace API.Sender.CQRS.Commands
{
    public class AddUser
    {
        public record Command : IRequest<CommandResult>
        {
            public string Email { get; init; }
            public string FirstName { get; init; }
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

            public CommandHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userService.AddUser(request);

                return new CommandResult { User = user };
            }
        }
    }
}
