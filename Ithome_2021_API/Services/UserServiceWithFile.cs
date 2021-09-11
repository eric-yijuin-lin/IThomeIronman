using Ithome_2021_API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ithome_2021_API.Services
{
    public class UserServiceOptions
    {
        public bool EnableLog { get; set; }
        public string FileName { get; set; }
    }

    public class UserServiceWithFile : IUserCRUD
    {
        private readonly string _fileName;
        private readonly List<User> _users;
        private readonly IOptions<UserServiceOptions> _option;

        private List<User> ReadUsersFromFile(string fileName)
        {
            if (!File.Exists(_fileName))
            {
                var file = File.Create(_fileName);
                file.Close();
            }

            var users = new List<User>();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var values = reader.ReadLine().Split(",");
                    users.Add(new User()
                    {
                        UserId = Convert.ToInt32(values[0]),
                        UserName = values[1],
                        Email = values[2]
                    });
                }
            }
            return users;
        }

        private void SaveUsersToFile(string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                foreach (var user in _users)
                {
                    writer.WriteLine($"{user.UserId},{user.UserName},{user.Email}");
                }
            }
        }

        public UserServiceWithFile(IOptions<UserServiceOptions> option)
        {
            _option = option;
            _fileName = _option.Value.FileName;
            _users = ReadUsersFromFile(_fileName);
        }

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
            if (_users.Count == 0)
            {
                model.UserId = 1;
            }
            else
            {
                model.UserId = _users.Max(x => x.UserId) + 1;
            }
            _users.Add(model);
            SaveUsersToFile(_fileName);
        }

        public void UpdateUser(int id, User model)
        {
            var existingUser = _users.FirstOrDefault(x => x.UserId == id);
            if (existingUser != null)
            {
                existingUser.UserName = model.UserName;
                existingUser.Email = model.Email;
            }
            SaveUsersToFile(_fileName);
        }

        public void DeleteUser(int id)
        {
            var existingUser = _users.FirstOrDefault(x => x.UserId == id);
            if (existingUser != null)
            {
                _users.Remove(existingUser);
            }
            SaveUsersToFile(_fileName);
        }
    }
}
