using EcolibrariumServer.Models.Quiz;
using EcoLibrariumServer.Data;
using EcoLibrariumServer.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcolibrariumServer.Controllers
{
    [Authorize]
    abstract public class QuizControllerBase<InfoType> : ControllerBase where InfoType : QuizInfo
    {
        protected readonly ApiContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public QuizControllerBase(ApiContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        protected ApplicationUser GetUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }

        abstract public IActionResult Add(InfoType quizInfo);

        abstract public IActionResult GetById(int id);

        abstract public IActionResult GetByName(string name, bool includePublic, bool onlySaved);

        abstract public IActionResult Upload(int id);
    }
}