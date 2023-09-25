using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Models;
using EcoLibrariumApp.Services;

namespace EcoLibrariumApp.ViewModels
{
    public partial class QuickQuizViewModel : ObservableObject
    {
        int i = 0;

        [RelayCommand]
        public async Task AddQuickQuiz()
        {
            QuickQuizInfo quizInfo = new QuickQuizInfo($"Quick quiz {i}");
            List<EntryAnswer> values = new() { new("YES", true), new("NO", false) };
            QuizEntry entry = new() {Question = "SUS", Answers = values};
            quizInfo.quizEntries.Add(entry);

            i++;
            var result = await QuizService.AddQuickQuiz(quizInfo);
            await MessageService.showMessage(result.Message);
        }

        [RelayCommand]
        public async Task GetAllQuickQuizzes()
        {

        }

        [RelayCommand]
        public async Task GetQuickQuizById()
        {

        }

        [RelayCommand]
        public async Task GetQuickQuizByName()
        {

        }


        [RelayCommand]
        public async Task UploadQuickQuiz()
        {



        }

        [RelayCommand]
        public async Task AddQuiz()
        {

        }

        [RelayCommand]
        public async Task GetAllQuizzes()
        {

        }

        [RelayCommand]
        public async Task GetQuizById()
        {

        }

        [RelayCommand]
        public async Task GetQuizByName()
        {

        }

        [RelayCommand]
        public async Task UploadQuiz()
        {

        }
    }
}
