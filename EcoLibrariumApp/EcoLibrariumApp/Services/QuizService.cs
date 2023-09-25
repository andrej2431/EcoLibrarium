using EcoLibrariumApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace EcoLibrariumApp.Services
{
    internal static class QuizService
    {
        public struct QuizResult
        {
            public QuizResult(bool success, string message)
            {
                Success = success;
                Message = message;
            }
            public bool Success;
            public string Message;
        }

        public static async Task<QuizResult> AddQuickQuiz(QuickQuizInfo quickQuizInfo)
        {
            if (quickQuizInfo == null || string.IsNullOrEmpty(quickQuizInfo.Name))
            {
                return new QuizResult(false, "Quick quiz info has to contain a name.");
            }

            HttpResponseMessage response = await HttpService.PutRequest(HttpService.ApiUrls.QuickQuizzesAdd, quickQuizInfo);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new QuizResult(false, $"error code {response.StatusCode}, content: {responseContent}");
            }

            return new QuizResult(true, responseContent);
        }
    }
}