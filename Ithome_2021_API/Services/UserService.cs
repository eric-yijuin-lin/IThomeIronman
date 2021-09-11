using Ithome_2021_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ithome_2021_API.Services
{
    public interface IUserCRUD
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public void CreateUser(User model);
        public void UpdateUser(int id, User model);
        public void DeleteUser(int id);
    }
    public class UserService : IUserCRUD
    {
        private static List<User> _users = new List<User>()
        {
            new User() {UserId = 0, UserName = "Alice", Email="alice@test.mail"},
            new User() {UserId = 1, UserName = "Bob", Email="bob@test.mail"},
            new User() {UserId = 2, UserName = "Cathy", Email="cathy@test.mail"},
        };

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(x => x.UserId == id);
        }

        public void CreateUser(User model)
        {
            model.UserId = _users.Max(x => x.UserId) + 1;
            _users.Add(model);
        }

        public void UpdateUser(int id, User model)
        {
            var existingUser = _users.FirstOrDefault(x => x.UserId == id);
            if (existingUser != null)
            {
                existingUser.UserName = model.UserName;
                existingUser.Email = model.Email;
            }
        }

        public void DeleteUser(int id)
        {
            var existingUser = _users.FirstOrDefault(x => x.UserId == id);
            if (existingUser != null)
            {
                _users.Remove(existingUser);
            }
        }
    }
}
