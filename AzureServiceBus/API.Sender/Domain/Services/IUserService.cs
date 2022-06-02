using API.Sender.CQRS.Commands;
using API.Sender.Domain.Models;

namespace API.Sender.Domain.Services
{
    public interface IUserService
    {
        Task<User?> AddUser(AddUser.Command request);
        Task<User?> UpdateUser(UpdateUser.Command request);
        Task<List<User>> GetUsers();
        Task ActivateUser(int id);
    }
}
