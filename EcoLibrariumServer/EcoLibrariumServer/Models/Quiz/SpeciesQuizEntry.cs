using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcolibrariumServer.Models.Quiz
{
    public class SpeciesQuizEntry
    {
        public SpeciesQuizEntry() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Question { get; set; }

        public List<QuestionAnswer> Answers { get; set; } = new();
    }

    public class QuestionAnswer
    {
        public QuestionAnswer() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Answer { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
