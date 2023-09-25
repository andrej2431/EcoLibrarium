using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLibrariumApp.Models
{
    class QuickQuizInfo
    {
        public QuickQuizInfo(string name) { Name = name; }

        public string Name { get; set; }

        public List<QuizEntry> quizEntries { get; set; } = new();
    }

    class QuizEntry
    {
        public string Question { get; set; }

        public List<EntryAnswer> Answers { get; set; } = new();
    }

    class EntryAnswer

    {
        public EntryAnswer() { }
        public EntryAnswer(string answer, bool isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }

        public string Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
