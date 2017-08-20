using Pymes4.Classes;
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
    public partial class ShoppingCar : ContentPage
    {
        public ShoppingCar(string telefono)
        {
            InitializeComponent();
            BindingContext = new ShoppingCarViewModel("71382211", "1"); //nueva instancia de itempage para SUBBINDING EN PAGES
        }
        #region Poner en el view model (OJO ESTO ES TEMPORAL)
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as ItemShoppingCar;
            if (item == null)
                return;

            //llama nueva pagina 
            Navigation.PushAsync(new ItemsPageDetailDelete(item));

        }
        #endregion
    }
}