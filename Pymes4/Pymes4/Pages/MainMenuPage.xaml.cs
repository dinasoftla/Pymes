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
        #region Attributes

        private bool isRunning;

        #endregion

        #region Properties
        // public ItemsPageViewModel ItemsPage { get; set; }
        //public bool IsRunning
        //{
        //    set
        //    {
        //        if (isRunning != value)
        //        {
        //            isRunning = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
        //        }
        //    }
        //    get
        //    {
        //        return isRunning;
        //    }
        //}
        #endregion
        #region Methods

        //public async void LoadApiResult(string phone, string pageapp)
        //{
        //    //Categoria = pageapp + "Pagina bindada";
        //    //if (!String.IsNullOrEmpty(Settings.Phone))
        //    //{
        //    try
        //    {
        //        IsRunning = true;
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri("http://192.168.0.17");
        //        string url = string.Format("/apirest/index.php/consultarproductos/{0}/{1}", phone, pageapp);
        //        var response = await client.GetAsync(url);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
        //            IsRunning = false;
        //            IsEnabled = false;
        //            return;
        //        }
        //        else
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            //Crear clase interface para notificaciones:
        //            productos = JsonConvert.DeserializeObject<RootObjectProductos>(result);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Error De Conexión", ex.Message, "Aceptar");
        //        IsRunning = false;
        //        IsEnabled = false;
        //        return;
        //    }
        //    //

        //    Successful();
        //    IsRunning = false;
        //    IsEnabled = true;
        //    //}

        //}

        //private void Successful()
        //{
        //    Items = new ObservableCollection<Item>();

        //    for (int i = 0; i < productos.Productos.Count; i++)
        //    {
        //        Items.Add(new Item
        //        {
        //            Code = productos.Productos[i].codarticulo,
        //            Name = productos.Productos[i].descripcion + " " + productos.Productos[i].codarticulo,
        //            Description = productos.Productos[i].caracteristicas,
        //            Image = "http://192.168.0.17/sistema/upload/" + productos.Productos[i].foto,
        //            Image2 = "http://192.168.0.17/sistema/upload/" + productos.Productos[i].foto2,
        //            Image3 = "http://192.168.0.17/sistema/upload/" + productos.Productos[i].foto3,
        //            Category = productos.Productos[i].linea,
        //            Qualification = productos.Productos[i].calificacion,
        //            Guarantee = productos.Productos[i].garantia,
        //            Price = productos.Productos[i].precio,
        //            ImageShoppingCar = "http://192.168.0.17/sistema/" + "/phpimages/shoppingcar.png"
        //        });
        //    }

        //    var sorted = from item in Items
        //                 orderby item.Name
        //                 group item by item.NameSort into monkeyGroup
        //                 select new Grouping<string, Item>(monkeyGroup.Key, monkeyGroup);

        //    ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sorted);

        //}


        #endregion

        public MainMenuPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new ItemsPage("1"));
            //ItemsPage = new ItemsPageViewModel("71382211", "2"); //nueva instancia de itempage para SUBBINDING EN PAGES
            IsPresented = true;
            MenuStack.Children.Add(new Button() { Text = "Productos", Command = new Command(OnAddControl) });
            MenuStack.Children.Add(new Button() { Text = "Carrito De Compras", Command = new Command(OnAddControl2) });
            MenuStack.Children.Add(new Button() { Text = "Enviados", Command = new Command(OnAddControl3) });
        }
        private void OnAddControl()
        {
            //ItemsPage = new ItemsPageViewModel("71382211", "2"); //nueva instancia de itempage para SUBBINDING EN PAGES
            Detail = new NavigationPage(new ItemsPage("1"));
            IsPresented = false;
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