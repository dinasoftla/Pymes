using Pymes4.Pages;
using Pymes4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pymes4.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "ShoppingCar":
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.ShoppingCar = new ShoppingCarViewModel();

                    await App.Current.MainPage.Navigation.PushAsync(new ShoppingCar());
                    break;
                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }

}
