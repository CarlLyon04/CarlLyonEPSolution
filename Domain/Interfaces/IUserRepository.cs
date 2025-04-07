using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        // Get all the users
        IEnumerable<User> GetAllUsers();

        // Get a user by ID
        User? GetUserById(int id);

        // Create a new user
        void CreateUser(User user);

        // Update an existing user
        void UpdateUser(User user);


        // Delete a user
        void DeleteUser(User user);
    }
}
