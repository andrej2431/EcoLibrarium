using Microsoft.EntityFrameworkCore;
using EcoLibrariumServer.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EcolibrariumServer.Models.Quiz;
using EcolibrariumServer.Models.Species;

namespace EcoLibrariumServer.Data
{
    public class ApiContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Species> Species { get; set; }

        public DbSet<SpeciesQuiz> SpeciesQuizzes { get; set; }

        public DbSet<QuizCollection> QuizCollections { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { 
        }

    }
}
