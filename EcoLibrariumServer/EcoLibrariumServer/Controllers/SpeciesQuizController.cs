using EcolibrariumServer.Controllers;
using EcolibrariumServer.Models.Quiz;
using EcoLibrariumServer.Data;
using EcoLibrariumServer.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EcoLibrariumServer.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesQuizController : QuizControllerBase<SpeciesQuizInfo>
    {
        public SpeciesQuizController(ApiContext context, UserManager<ApplicationUser> userManager) : base(context, userManager) { }

        [HttpPut("")]
        public override IActionResult Add([FromBody] SpeciesQuizInfo quizInfo)
        {
            if (quizInfo == null || quizInfo.Name == null)
            {
                return BadRequest("Quiz info not recieved.");
            }

            if (UserHasQuizWithName(quizInfo.Name))
            {
                return BadRequest("You already have a quiz with that name");
            }

            SpeciesQuiz quiz = new SpeciesQuiz(quizInfo);

            quiz.AuthorId = GetUser().Id;
            _context.SpeciesQuizzes.Add(quiz);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public override IActionResult GetAll(bool includePublic = true, bool onlySaved = false)
        {
            List<SpeciesQuiz> quizzes = GetQuizzesByName("", includePublic).ToList();
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id)
        {
            SpeciesQuiz? quiz = GetQuizById(id);

            if (quiz == null)
            {
                return NotFound($"Quiz with the id {id} doesn't exist.");
            }

            return Ok(quiz);
        }


        [HttpGet("name/{name}")]
        public override IActionResult GetByName(string name, bool includePublic = true, bool onlySaved = false)
        {
            List<SpeciesQuiz> quizzes = GetQuizzesByName(name, includePublic).ToList();

            if (quizzes.Count <= 0)
            {
                return NotFound($"No quiz starting with {name} was found.");
            }

            return Ok(quizzes);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("upload/{id}")]
        public override IActionResult Upload(int id)
        {
            SpeciesQuiz? quiz = GetQuizById(id);

            if (quiz == null)
            {
                return NotFound($"Quiz with the id {id} doesn't exist.");
            }

            quiz.IsPublic = true;
            _context.SaveChanges();

            return Ok();
        }



        private bool UserHasQuizWithName(string name)
        {
            var query = from q in _context.SpeciesQuizzes
                        where (q.Name == name && q.AuthorId == GetUser().Id)
                        select q;

            return query.Any();
        }


        private SpeciesQuiz? GetQuizById(int id)
        {
            var query = from q in _context.SpeciesQuizzes
                        where q.Id == id
                        select q;

            return query.Include(quiz=>quiz.QuizEntries).FirstOrDefault();
        }

        private IQueryable<SpeciesQuiz> GetQuizzesByName(string name, bool includeAllPublic)
        {
            var query = from q in _context.SpeciesQuizzes
                        where (q.Name.StartsWith(name))
                        select q;

            if (includeAllPublic)
            {
                query = from q in query
                        where q.IsPublic || q.AuthorId == GetUser().Id
                        select q;
            }
            else
            {
                query = from q in query
                        where q.AuthorId == GetUser().Id
                        select q;
            }

            return query.Include(quiz => quiz.QuizEntries).ThenInclude(entry => entry.Answers);
        }
    }
}
