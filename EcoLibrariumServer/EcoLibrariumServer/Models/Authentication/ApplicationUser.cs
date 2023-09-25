using EcolibrariumServer.Models.Quiz;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcoLibrariumServer.Models.Authentication
{

    public class ApplicationUser : IdentityUser<string>
    {
        public List<QuizCollection> SavedPublicQuizCollections { get; set; } = new();
    }

}
