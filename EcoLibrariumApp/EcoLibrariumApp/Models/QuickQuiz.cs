using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLibrariumApp.Models
{
    class QuickQuiz
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public string AuthorId {  get; set; }

        public DateTime CreatedAt {  get; set; }

        public List<QuizEntry> QuizEntries { get; set; } = new();
    }
}
