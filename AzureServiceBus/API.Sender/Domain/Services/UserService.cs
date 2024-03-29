﻿using API.Sender.CQRS.Commands;
using API.Sender.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Sender.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly SqlDBContext _dbContext;

        public UserService(SqlDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> AddUser(AddUser.Command request)
        {
            User user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user != null)
            {
                return null;
            }

            user = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };

            _dbContext.Users.Add(user);
            int success = await _dbContext.SaveChangesAsync();

            if (success > 0)
            {
                return user;
            }

            else
            {
                return null;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> UpdateUser(UpdateUser.Command request)
        {
            User user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null)
            {
                return null;
            }

            if (request.Email != null)
            {
                user.Email = request.Email;
            }

            if (request.FirstName != null)
            {
                user.FirstName = request.FirstName;
            }

            if (request.LastName != null)
            {
                user.LastName = request.LastName;
            }

            if (request.Age != null)
            {
                user.Age = request.Age;
            }

            int success = await _dbContext.SaveChangesAsync();

            if (success > 0)
            {
                return user;
            }

            else
            {
                return null;
            }
        }

        public async Task ActivateUser(int id)
        {
            User user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return;
            }

            user.IsActive = true;
            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}
