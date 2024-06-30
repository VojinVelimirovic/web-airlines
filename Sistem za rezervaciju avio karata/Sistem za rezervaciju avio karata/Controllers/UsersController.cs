using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        [HttpPost]
        [Route("api/users/register")]
        public IHttpActionResult Register(User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            if (Users.FindByUsername(user.Username) != null)
            {
                return BadRequest("Username already exists.");
            }
            Users.AddUser(user);
            HttpContext.Current.Session["CurrentUser"] = user;

            return Ok(user);
        }

        [HttpGet]
        [Route("api/users/login/{username}")]
        public IHttpActionResult Login(string username)
        {
            var user = Users.FindByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            HttpContext.Current.Session["CurrentUser"] = user;

            return Ok(user);
        }

        [HttpPost]
        [Route("api/users/logout")]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.Session.Remove("CurrentUser");

            return Ok();
        }

        [HttpGet]
        [Route("api/users/{username}")]
        public IHttpActionResult UsernameTaken(string username)
        {
            if(Users.FindByUsername(username) == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/users/currentuser")]
        public IHttpActionResult GetCurrentUser()
        {
            var currentUser = HttpContext.Current.Session["CurrentUser"] as User;

            if (currentUser == null)
            {
                return NotFound();
            }

            return Ok(currentUser);
        }

        [HttpPost]
        [Route("api/users/update")]
        public IHttpActionResult UpdateUser(User updatedUser)
        {
            var currentUser = HttpContext.Current.Session["CurrentUser"] as User;
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var existingUser = Users.FindByUsername(updatedUser.Username);
            if (existingUser != null && existingUser.Username != currentUser.Username)
            {
                return BadRequest("Username already exists.");
            }

            currentUser.Username = updatedUser.Username;
            currentUser.Password = updatedUser.Password;
            currentUser.FirstName = updatedUser.FirstName;
            currentUser.LastName = updatedUser.LastName;
            currentUser.Email = updatedUser.Email;
            currentUser.DateOfBirth = updatedUser.DateOfBirth;
            currentUser.Gender = updatedUser.Gender;

            Users.UpdateUser(currentUser);

            Users.SaveUsers();

            HttpContext.Current.Session["CurrentUser"] = currentUser;

            return Ok(currentUser);
        }
    }
}