using Newtonsoft.Json;
using Pymes4.Classes;
using Pymes4.Helpers;
using Pymes4.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pymes4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : MasterDetailPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new Categories());
           
            MenuStack.Children.Add(new Button() { Text = "Productos", Command = new Command(OnAddControl) });
            MenuStack.Children.Add(new Button() { Text = "Carrito De Compras", Command = new Command(OnAddControl2) });
            MenuStack.Children.Add(new Button() { Text = "Enviados", Command = new Command(OnAddControl3) });
            MenuStack.Children.Add(new Button() { Text = "Crear Pedidos", Command = new Command(OnAddControl4)});
        }
        private void OnAddControl()
        {
            Detail = new NavigationPage(new Categories());
            IsPresented = false;
        }
        private void OnAddControl2()
        {
            Detail = new NavigationPage(new ShoppingCar());
            IsPresented = false;
        }
        private void OnAddControl3()
        {
            Detail = new NavigationPage(new Delivered("71382211"));
            IsPresented = false;
        }
        private void OnAddControl4()
        {
            Detail = new NavigationPage(new Test());
            IsPresented = false;
        }
    }
   
}