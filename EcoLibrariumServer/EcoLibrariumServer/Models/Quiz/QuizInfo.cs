using System.ComponentModel.DataAnnotations;

namespace EcolibrariumServer.Models.Quiz
{
    public class QuizInfo
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
    }

    public class SpeciesQuizInfo : QuizInfo
    {
        [Required]
        public List<SpeciesQuizEntry>? quizEntries { get; set; }
    }

    public class QuizCollectionInfo : QuizInfo
    {
        public List<int> quizIds { get; set; } = new();
    }
}
