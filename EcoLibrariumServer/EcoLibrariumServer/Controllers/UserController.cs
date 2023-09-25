using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcoLibrariumServer.Data;
using EcoLibrariumServer.Validators;
using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using EcoLibrariumServer.Models.Authentication;
using EcolibrariumServer.Models.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace EcoLibrariumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ApiContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCredentials userCredentials)
        {
            var userValidator = new RegisterUserCredentialsValidator();
            var validationResult = userValidator.Validate(userCredentials);


            if (!validationResult.IsValid)
            {
                return BadRequest("Invalid user info");
            }

            ApplicationUser user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userCredentials.UserName,
                Email = userCredentials.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var createResult = await _userManager.CreateAsync(user, userCredentials.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest("Couldn't create user.");
            }

            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                return BadRequest("Couldn't assign role to user.");
            }


            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCredentials userCredentials)
        {
            var userValidator = new LoginUserCredentialsValidator();
            var validationResult = userValidator.Validate(userCredentials);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
            }

            if (string.IsNullOrEmpty(userCredentials.Email) || string.IsNullOrEmpty(userCredentials.Password))
            {
                return BadRequest("Missing credentials.");
            }

            var user = await _userManager.FindByEmailAsync(userCredentials.Email);
            if (user == null)
            {
                return BadRequest("User with that email doesn't exist.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userCredentials.Password, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            {
                return BadRequest("Invalid credentials.");
            }
                
            }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("promote")]
        public async Task<IActionResult> PromoteToAdmin([FromBody] PromoteToAdminInfo setRoleInfo)
        {
            if (setRoleInfo == null)
            {
                return BadRequest("Info on setting role not recieved.");
            }

            if (String.IsNullOrEmpty(setRoleInfo.Email))
            {
                return BadRequest("Email is required!");
            }

            var user = await _userManager.FindByEmailAsync(setRoleInfo.Email);
            if(user == null)
            {
                return BadRequest("User with that email doesn't exist");
            }
            
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if(!result.Succeeded)
            {
                return BadRequest("Unable to promote to admin.");
            }

            return Ok();
        }

        [HttpGet("userinfo")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userProfile = new
            {
                user.UserName,
                user.Email,
                Roles = roles
            };

            return Ok(userProfile);
        }
    }
}
