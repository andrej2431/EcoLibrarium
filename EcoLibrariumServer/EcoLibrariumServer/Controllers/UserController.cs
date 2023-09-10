using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoLibrariumServer.Models;
using EcoLibrariumServer.Data;
using EcoLibrariumServer.Validators;
using System.Diagnostics;

namespace EcoLibrariumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiContext _context;

        
        public UserController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost("/create")]
        public JsonResult CreateUser([FromBody] User user) {
            UserValidator userValidator = new UserValidator();
            var validationResult = userValidator.Validate(user);

            if(!validationResult.IsValid) { 
                return new JsonResult(BadRequest("Invalid user info"));
            }

            var userWithSameUsername = from u in _context.Users
                                       where u.Name == user.Name
                                       select u;
            if(userWithSameUsername.Count() > 0)
            {
                return new JsonResult(BadRequest("There already exists a user with that username."));
            }

            var userWithSameEmail = from u in _context.Users
                                    where u.Email == user.Email
                                    select u;
            if(userWithSameEmail.Count() > 0)
            {
                return new JsonResult(BadRequest("There already exists a user with that email."));
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return new JsonResult(Ok());
        }

        [HttpPost("/login")]
        public JsonResult LoginUser([FromBody] User user)
        {
            if(user == null)
            {

            }

            if(user.Name == null || user.PasswordHash == null)
            {
                return new JsonResult(BadRequest("Missing username or password."));
            }

            var userInDatabase = _context.Users.FirstOrDefault(u => u.Name == user.Name);

            if (userInDatabase == null)
            {
                return new JsonResult(BadRequest("User with that username doesn't exist."));
            }
            
            if(userInDatabase.PasswordHash != user.PasswordHash)
            {
                return new JsonResult(BadRequest("Wrong password."));
            }

            Guid sessionId  = Guid.NewGuid();
            user.SessionId = sessionId.ToString();
            _context.SaveChanges();

            return new JsonResult(Ok(sessionId.ToString()));
        }
        
    }
}
