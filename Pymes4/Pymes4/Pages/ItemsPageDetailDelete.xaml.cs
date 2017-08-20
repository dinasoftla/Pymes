using Pymes4.Classes;
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
    public partial class ItemsPageDetailDelete : ContentPage
    {
        public ItemsPageDetailDelete(ItemShoppingCar ItemShoppingCar)
        {
            InitializeComponent();
        }
    }
}