using EcoLibrariumApp.Services;
using EcoLibrariumApp.ViewModels;
using EcoLibrariumApp.Models;
using Microsoft.Extensions.Logging;

namespace EcoLibrariumApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HttpService>();
            builder.Services.AddSingleton<AuthenticationService>();
            builder.Services.AddSingleton<MessageService>();
            builder.Services.AddSingleton<NavigationService>();

            // Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<MainMenuPage>();
            builder.Services.AddTransient<SpeciesInfoPage>();
            builder.Services.AddTransient<QuickQuizPage>();
            builder.Services.AddTransient<SavedQuizzesPage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<AdminMenuPage>();
            builder.Services.AddTransient<AddSpeciesPage>();
            builder.Services.AddTransient<PromoteUserPage>();
            // View Models
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<MainMenuViewModel>();
            builder.Services.AddTransient<SpeciesInfoViewModel>();
            builder.Services.AddTransient<QuickQuizViewModel>();
            builder.Services.AddTransient<SavedQuizzesViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<AdminMenuViewModel>();
            builder.Services.AddTransient<AddSpeciesViewModel>();
            builder.Services.AddTransient<PromoteUserViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}