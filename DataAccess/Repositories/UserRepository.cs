using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Store dependency
        private readonly PollDbContext _context;

        // Consturtor dpendency
        public UserRepository(PollDbContext context)
        {
            _context = context;
        }

        // Not required
        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        // Not required
        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        // Not required
        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        // Not required
        public User? GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        // Not required
        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
