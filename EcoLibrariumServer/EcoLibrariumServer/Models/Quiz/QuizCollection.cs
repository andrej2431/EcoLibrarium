namespace EcolibrariumServer.Models.Quiz
{
    public class QuizCollection : Quiz
    {
        public QuizCollection(QuizCollectionInfo quizCollectionInfo, List<SpeciesQuiz> quizzes) : base(quizCollectionInfo)
        {
            Quizzes = quizzes;
        }

        private QuizCollection() : base(new QuizInfo()) { }

        public List<SpeciesQuiz> Quizzes { get; set; } = new();
    }
}
