using API.Sender.Domain.Models;
using API.Sender.Domain.Services;
using MediatR;

namespace API.Sender.CQRS.Queries
{
    public class GetUsers
    {
        public record Query : IRequest<QueryResult>
        {

        }

        public record QueryResult
        {
            public List<User> Users { get; init; }

            public QueryResult(List<User> users)
            {
                Users = users;
            }
        }

        public class QueryHandler : IRequestHandler<Query, QueryResult>
        {
            private readonly IUserService _userService;

            public QueryHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
            {
                List<User> users = await _userService.GetUsers();

                return new QueryResult(users);
            }
        }
    }
}
