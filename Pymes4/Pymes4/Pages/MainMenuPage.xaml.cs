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

            MenuStack.Children.Add(new Button() { Text = "Ofertas", Command = new Command(OnAddControl) });
            MenuStack.Children.Add(new Button() { Text = "Pedidos", Command = new Command(OnAddControl2) });
            MenuStack.Children.Add(new Button() { Text = "Ubicación", Command = new Command(OnAddControl3) });
            MenuStack.Children.Add(new Button() { Text = "Llamar", Command = new Command(OnAddControl4)});
            MenuStack.Children.Add(new Button() { Text = "Solicitar Cita", Command = new Command(OnAddControl5) });
            MenuStack.Children.Add(new Button() { Text = "Redes Sociales", Command = new Command(OnAddControl6) });
            MenuStack.Children.Add(new Button() { Text = "Control De Calidad", Command = new Command(OnAddControl7) });
        }
        private void OnAddControl()
        {
            Detail = new NavigationPage(new OffersPage());//Ofertas
            IsPresented = false;
        }
        private void OnAddControl2()
        {
            Detail = new NavigationPage(new OrdersPage());//Pedidos
            IsPresented = false;
        }
        private void OnAddControl3()
        {
            Detail = new NavigationPage(new LocationPage());//Ubicacion
            IsPresented = false;
        }
        private void OnAddControl4()
        {
            Detail = new NavigationPage(new CallNowPage());//Llamar
            IsPresented = false;
        }
        private void OnAddControl5()
        {
            Detail = new NavigationPage(new AppointmentPage());//SolicitarCita
            IsPresented = false;
        }
        private void OnAddControl6()
        {
            Detail = new NavigationPage(new SocialNetworksPage());//Redes
            IsPresented = false;
        }
        private void OnAddControl7()
        {
            Detail = new NavigationPage(new QualityPage());//Calidad
            IsPresented = false;
        }
    }
   
}