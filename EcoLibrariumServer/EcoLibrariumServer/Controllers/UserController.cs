using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoLibrariumServer.Models;
using EcoLibrariumServer.Data;
using EcoLibrariumServer.Validators;
using System.Diagnostics;
using FluentValidation;

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

        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] RegisterUserCredentials userCredentials)
        {
            var userValidator = new RegisterUserCredentialsValidator();
            var validationResult = userValidator.Validate(userCredentials);

            if (!validationResult.IsValid)
            {
                return BadRequest("Invalid user info");
            }

            var userWithSameUsername = from u in _context.Users
                                       where u.Name == userCredentials.Name
                                       select u;
            if (userWithSameUsername.Count() > 0)
            {
                return BadRequest("There already exists a user with that username.");
            }

            var userWithSameEmail = from u in _context.Users
                                    where u.Email == userCredentials.Email
                                    select u;
            if (userWithSameEmail.Count() > 0)
            {
                return BadRequest("There already exists a user with that email.");
            }

            User user = new User(userCredentials.Name, userCredentials.Email, userCredentials.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserCredentials userCredentials)
        {
            var userValidator = new LoginUserCredentialsValidator();
            var validationResult = userValidator.Validate(userCredentials);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
            }

            var userInDatabase = _context.Users.FirstOrDefault(u => u.Email == userCredentials.Email);

            if (userInDatabase == null)
            {
                return BadRequest("User with that email doesn't exist.");
            }

            if (userInDatabase.PasswordHash != userCredentials.PasswordHash)
            {
                return BadRequest("Wrong password.");
            }

            Guid sessionId = Guid.NewGuid();
            userInDatabase.SessionId = sessionId.ToString();
            _context.SaveChanges();

            var response = new
            {
                username = userInDatabase.Name,
                email = userInDatabase.Email,
                sessionId = userInDatabase.SessionId,
            };

            return Ok(response);
        }

        [HttpPost("logout")]
        public IActionResult LogoutUser([FromBody] SessionInfo sessionInfo)
        {
            if (sessionInfo == null || string.IsNullOrEmpty(sessionInfo.SessionId))
            {
                return BadRequest("No sessionId recieved");
            }

            var userInDatabase = _context.Users.FirstOrDefault(u => u.SessionId == sessionInfo.SessionId);

            if (userInDatabase == null)
            {
                return BadRequest("User with that sessionId doesn't exist.");
            }

            userInDatabase.SessionId = "";
            _context.SaveChanges();

            return Ok();
        }
    }
}
