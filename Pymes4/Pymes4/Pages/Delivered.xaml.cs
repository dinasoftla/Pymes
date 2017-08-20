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
    public partial class Delivered : ContentPage
    {
        public Delivered(string telefono)
        {
            InitializeComponent();
            BindingContext = new DeliveredViewModel(telefono, "1"); //nueva instancia de itempage para SUBBINDING EN PAGES
        }
    }
}