using EcoLibrariumServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcolibrariumServer.Models.Quiz;
using Microsoft.EntityFrameworkCore;
using EcoLibrariumServer.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EcolibrariumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizCollectionController : QuizControllerBase<QuizCollectionInfo>
    {
        public QuizCollectionController(ApiContext context, UserManager<ApplicationUser> userManager) : base(context, userManager) { }

        [HttpPut("")]
        public override IActionResult Add(QuizCollectionInfo quizInfo)
        {
            if (quizInfo == null || quizInfo.Name == null)
            {
                return BadRequest("Quiz info not recieved.");
            }

            if (UserHasQuizWithName(quizInfo.Name))
            {
                return BadRequest("You already have a quiz with that name");
            }


            List<SpeciesQuiz> speciesQuizzes = GetSpeciesQuizzesByIds(quizInfo.quizIds);

            QuizCollection quiz = new QuizCollection(quizInfo, speciesQuizzes);

            quiz.AuthorId = GetUser().Id;
            _context.QuizCollections.Add(quiz);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(int id)
        {
            QuizCollection? quiz = GetQuizById(id);

            if (quiz == null)
            {
                return NotFound($"Quiz with the id {id} doesn't exist.");
            }

            return Ok(quiz);
        }

        [HttpGet("name/{name}")]
        public override IActionResult GetByName(string name, bool includePublic = true, bool onlySaved = false)
        {
            List<QuizCollection> quizzes = GetQuizzesByName(name, includePublic, onlySaved).ToList();

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
            QuizCollection? quiz = GetQuizById(id);

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
            var query = from q in _context.QuizCollections
                        where (q.Name == name && q.AuthorId == GetUser().Id)
                        select q;

            return query.Any();
        }

        private QuizCollection? GetQuizById(int id)
        {
            return _context.QuizCollections
                .Where(quiz => quiz.Id == id && (quiz.AuthorId == GetUser().Id || quiz.IsPublic))
                .Include(quiz => quiz.Quizzes)
                .ThenInclude(quiz => quiz.QuizEntries)
                .ThenInclude(entry => entry.Answers)
                .FirstOrDefault();
        }

        private IQueryable<QuizCollection> GetQuizzesByName(string name, bool includeAllPublic, bool onlySaved)
        {
            var query = from q in _context.QuizCollections
                        where (q.Name.StartsWith(name))
                        select q;

            if (includeAllPublic)
            {
                query = from q in query
                        where q.IsPublic || q.AuthorId == GetUser().Id
                        select q;
            }
            else if (onlySaved)
            {
                query = from q in query
                        where q.AuthorId == GetUser().Id || q.IsPublic && GetUser().SavedPublicQuizCollections.Contains(q)
                        select q;
            }
            else
            {
                query = from q in query
                        where q.AuthorId == GetUser().Id
                        select q;
            }

            return query
                .Include(quiz => quiz.Quizzes)
                .ThenInclude(quiz => quiz.QuizEntries)
                .ThenInclude(entry => entry.Answers);
        }

        private List<SpeciesQuiz> GetSpeciesQuizzesByIds(IEnumerable<int> speciesIds)
        {
            List<SpeciesQuiz> speciesQuizzes = new List<SpeciesQuiz>();


            foreach (int id in speciesIds)
            {
                var speciesQuiz = _context.SpeciesQuizzes
                    .Where(q => q.Id == id)
                    .Include(q => q.QuizEntries)
                    .ThenInclude(e => e.Answers)
                    .FirstOrDefault();
                if (speciesQuiz != null)
                {
                    speciesQuizzes.Add(speciesQuiz);
                }

            }

            return speciesQuizzes;
        }
    }
}
