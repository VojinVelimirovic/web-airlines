using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sistem_za_rezervaciju_avio_karata.Models;

namespace Sistem_za_rezervaciju_avio_karata.Controllers
{
    public class UsersController : ApiController
    {
        public List<User> Get()
        {
            return Users.UsersList;
        }

        public IHttpActionResult Get(string username)
        {
            var user = Users.FindByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public IHttpActionResult Post(User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("Username is null or empty.");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Password is null or empty.");
            }
            if (string.IsNullOrEmpty(user.FirstName))
            {
                return BadRequest("FirstName is null or empty.");
            }
            if (string.IsNullOrEmpty(user.LastName))
            {
                return BadRequest("LastName is null or empty.");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Email is null or empty.");
            }
            if (string.IsNullOrEmpty(user.Gender))
            {
                return BadRequest("Gender is null or empty.");
            }
            if (string.IsNullOrEmpty(user.DateOfBirth))
            {
                return BadRequest("DateOfBirth is null or empty.");
            }
            if (Users.FindByUsername(user.Username) != null)
            {
                return BadRequest("Username already exists.");
            }

            Console.WriteLine($"Received User: {user.Username}, {user.Password}, {user.FirstName}, {user.LastName}, {user.Email}, {user.DateOfBirth}, {user.Gender}, {user.UserType}");

            return Ok(Users.AddUser(user));
        }

        public IHttpActionResult Delete(string username)
        {
            User user = Users.FindByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            Users.RemoveUser(user);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
