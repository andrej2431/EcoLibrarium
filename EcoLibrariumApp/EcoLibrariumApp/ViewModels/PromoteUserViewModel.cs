using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoLibrariumApp.Models;
using EcoLibrariumApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLibrariumApp.ViewModels
{
    public partial class PromoteUserViewModel : ObservableObject
    {
        [ObservableProperty]
        string email;

        [RelayCommand]
        public async Task SubmitPromotion()
        {
            UserPromotionInfo userPromotionInfo = new()
            {
                Email = Email
            };

            var result = await AuthenticationService.PromoteUser(userPromotionInfo);

            if (result.Success)
            {
                await MessageService.showMessage("User promoted successfully.");
                await NavigationService.NavigateTo(new AdminMenuPage());
            } else
            {
                await MessageService.showMessage(result.Message);
            }
        }
    }
}
