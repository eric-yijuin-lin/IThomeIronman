using Ithome_2021_API.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace Ithome_2021_API.Services
{
    public class MySqlOptions
    {
        public string ConnectionString { get; set; }
    }
    public class UserServiceWithMySQL : IUserCRUD
    {
        private readonly IOptions<MySqlOptions> _options;

        public UserServiceWithMySQL(IOptions<MySqlOptions> options)
        {
            _options = options;
        }

        public void CreateUser(User model)
        {
            using (var db = new MySqlConnection(_options.Value.ConnectionString))
            {
                bool emailUsed = db.ExecuteScalar<bool>(
                    @"SELECT EXISTS 
                        (SELECT user_id FROM `user` WHERE email = @Email)",
                    model);

                if (!emailUsed)
                {
                    db.Execute(
                        @"INSERT INTO `user` (user_name, email, verified)
                          VALUES (@UserName, @Email, 0)",
                        model);
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var db = new MySqlConnection(_options.Value.ConnectionString))
            {
                db.Execute(
                    @"DELETE FROM `user`
                      WHERE user_id = @UserId",
                    new { UserId = id});
            }
        }

        public List<User> GetAllUsers()
        {
            using (var db = new MySqlConnection(_options.Value.ConnectionString))
            {
                var result = db.Query<User>(
                    @"SELECT
                        user_id AS UserID,
                        user_name AS UserName,
                        email AS Email,
                        verified AS Verified
                      FROM `user`",
                    new {});
                return result.ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (var db = new MySqlConnection(_options.Value.ConnectionString))
            {
                var result = db.QuerySingleOrDefault<User>(
                    @"SELECT
                        user_id AS UserID,
                        user_name AS UserName,
                        email AS Email,
                        verified AS Verified
                      FROM `user`
                      WHERE user_id = @UserId",
                    new { UserId = id});
                return result;
            }
        }

        public void UpdateUser(int id, User model)
        {
            using (var db = new MySqlConnection(_options.Value.ConnectionString))
            {
                bool emailUsed = db.ExecuteScalar<bool>(
                    @"SELECT EXISTS 
                        (SELECT user_id FROM `user` WHERE email = @UserId)",
                    model);

                if (!emailUsed)
                {
                    db.Execute(
                        @"UPDATE `user` 
                          SET
                            user_name = @UserName,
                            email = @Email,
                            verified = @Verified
                          WHERE
                            user_id = @UserId",
                        model);
                }
            }
        }
    }
}
