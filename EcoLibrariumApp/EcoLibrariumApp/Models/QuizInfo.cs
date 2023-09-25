using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLibrariumApp.Models
{
    class QuizInfo
    {
        public QuizInfo(string name) { Name = name; }

        public string Name { get; set; }

        public List<int> quizIds { get; set; } = new();
    }
}
