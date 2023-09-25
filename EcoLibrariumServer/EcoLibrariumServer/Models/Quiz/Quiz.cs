using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcolibrariumServer.Models.Quiz
{
    public class Quiz
    {
        public Quiz(QuizInfo quizInfo)
        {
            Name = quizInfo.Name;
            IsPublic = false;
            AuthorId = null;
            CreatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public bool IsPublic { get; set; }

        public string? AuthorId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
