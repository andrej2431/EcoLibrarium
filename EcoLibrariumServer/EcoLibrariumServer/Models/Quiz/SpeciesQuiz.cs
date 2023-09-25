namespace EcolibrariumServer.Models.Quiz
{
    public class SpeciesQuiz : Quiz
    {
        public SpeciesQuiz(SpeciesQuizInfo speciesQuizInfo) : base(speciesQuizInfo)
        {
            QuizEntries = speciesQuizInfo.quizEntries;
        }

        private SpeciesQuiz() : base(new QuizInfo()) { }

        public List<SpeciesQuizEntry> QuizEntries { get; set; } = new();
    }
}