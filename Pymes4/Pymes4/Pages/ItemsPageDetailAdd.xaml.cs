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
    public partial class ItemsPageDetailAdd : ContentPage
    {
        public ItemsPageDetailAdd(Item item)
        {
            InitializeComponent();
            BindingContext = new ItemsPageDetailAddViewModel(item); //nueva instancia de itempage para SUBBINDING EN PAGES
        }
    }
}