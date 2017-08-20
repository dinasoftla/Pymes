using Pymes4.Helpers;
using Pymes4.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        #region Properties
           // public ItemsPageViewModel ItemsPage { get; set; }
        #endregion


        public MainMenuPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new ItemsPage(Settings.Phone, "1"));
            //ItemsPage = new ItemsPageViewModel("71382211", "2"); //nueva instancia de itempage para SUBBINDING EN PAGES
            IsPresented = true;
            MenuStack.Children.Add(new Button() { Text = "Productos", Command = new Command(OnAddControl) });
            MenuStack.Children.Add(new Button() { Text = "Carrito De Compras", Command = new Command(OnAddControl2) });
            MenuStack.Children.Add(new Button() { Text = "Enviados", Command = new Command(OnAddControl3) });
        }
        private void OnAddControl()
        {
            //ItemsPage = new ItemsPageViewModel("71382211", "2"); //nueva instancia de itempage para SUBBINDING EN PAGES
            Detail = new NavigationPage(new ItemsPage(Settings.Phone, "1"));
            IsPresented = true;
        }
        private void OnAddControl2()
        {
            Detail = new NavigationPage(new ShoppingCar("71382211"));
            //ItemsPage = new ItemsPageViewModel("71382211", "2"); //nueva instancia de itempage para SUBBINDING EN PAGES
            IsPresented = false;
        }
        private void OnAddControl3()
        {
            Detail = new NavigationPage(new Delivered("71382211"));
            //ItemsPage = new ItemsPageViewModel("71382211", "2"); //nueva instancia de itempage para SUBBINDING EN PAGES
            IsPresented = false;
        }
    }
   

    class MainMenuPageViewModel : INotifyPropertyChanged
    {

        public MainMenuPageViewModel()
        {
            IncreaseCountCommand = new Command(click);
        }

        private void click()
        {
            throw new NotImplementedException();
        }

        public ICommand IncreaseCountCommand { get; }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}