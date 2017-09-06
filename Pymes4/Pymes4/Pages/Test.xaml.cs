using Pymes4.Helpers;
using Pymes4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pymes4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : TabbedPage
    {


        //private ShoppingCarViewModel shoppingCarViewModel;
        //public ShoppingCarViewModel ShoppingCarViewModel
        //{
        //    get { return this.shoppingCarViewModel; }
        //}


        public Test()
        {
            //var navigationPage = new NavigationPage(new Categories());
            //navigationPage.Icon = "schedule.png";
            //navigationPage.Title = "Schedule";

            //Children.Add(navigationPage);

            Page categorias = new Categories();
            Children.Add(categorias);

            Page shoppingCar = new ShoppingCar();
            Children.Add(shoppingCar);

            ////ShoppingCarViewModel shoppingCarViewModel = new ShoppingCarViewModel(Settings.Phone,"1",null);

            //this.CurrentPageChanged += (object sender, EventArgs e) =>
            //{
            //    var i = this.Children.IndexOf(this.CurrentPage);

            //    if (i == 1)
            //    {
            //        //shoppingCarViewModel.LoadProducts();
            //        //Children.Remove(shoppingCar);
            //        //Children.Add(shoppingCar);
            //    }
            //};



        }
        protected async override void OnAppearing()
        {
           
        }

    }
}