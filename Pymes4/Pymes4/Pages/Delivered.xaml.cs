using Pymes4.Classes;
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
    public partial class Delivered : ContentPage
    {
        public Delivered()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();

            base.Appearing += (object sender, EventArgs e) => {
                mainViewModel.LoadDeliveredCommand.Execute(this);
            };
        }
        #region Poner en el view model (OJO ESTO ES TEMPORAL)
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Item;
            if (item == null)
                return;

            //llama nueva pagina 
           

        }
        #endregion
    }
}