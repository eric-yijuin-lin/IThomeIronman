using Ithome_2021_API.Models;
using Ithome_2021_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ithome_2021_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCRUD _user;
        public UserController(IUserCRUD user)
        {
            _user = user;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _user.GetAllUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var user = _user.GetUserById(id);
            if (user == null)
            {
                throw new Exception("找不到 user");
            }
            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post(User user)
        {
            _user.CreateUser(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, User newUserData)
        {
            _user.UpdateUser(id, newUserData);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _user.DeleteUser(id);
        }
    }
}
