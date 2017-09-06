using Pymes4.Classes;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Pymes4.Helpers;
using Pymes4.Services;
using System.Linq;



namespace Pymes4.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes

        public ObservableCollection<ItemShoppingCar> ItemShoppingCar { get; set; }

        public ObservableCollection<Grouping<int, ItemShoppingCar>> itemsGrouped { get; set; }

        public int ItemCount => ItemShoppingCar.Count;

        private RootObjectProductosCarrito productoscarrito;

        private decimal totalcarrito;

        private bool isVisible;

        private bool isRefreshing;

        private NavigationService navigationService;

        public ItemsPageViewModel ItemsPage { get; set; }

        public ShoppingCarViewModel ShoppingCar { get; set; }
        //public MainMenuPageViewModel MainMenuPage { get; set; }

        private RootObjectUsuarios usuarios;

        private bool isRunning;

        private bool isEnabled;

        private string message;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public ObservableCollection<Grouping<int, ItemShoppingCar>> ItemsGrouped
        {
            set
            {
                if (itemsGrouped != value)
                {
                    itemsGrouped = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ItemsGrouped"));
                }
            }
            get
            {
                return itemsGrouped;
            }
        }
        public bool IsVisible
        {
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisible"));
                }
            }
            get
            {
                return isVisible;
            }
        }
        public decimal TotalCarrito
        {
            set
            {
                if (totalcarrito != value)
                {
                    totalcarrito = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalCarrito"));
                }
            }
            get
            {
                return totalcarrito;
            }
        }

        public bool IsRefreshing
        {
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
            get
            {
                return isRefreshing;
            }
        }
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }
        public string Message
        {
            set
            {
                if (message != value)
                {
                    message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
                }
            }
            get
            {
                return message;
            }
        }
        #endregion

        #region Constructors

        public MainViewModel()
        {
            //Singleton
            instance = this;

            //Navigation
            navigationService = new NavigationService();

            IsEnabled = false;
            //GetRates();
            LoadApiResult(Settings.Phone);
            Settings.ApiAddress = "http://192.168.0.10";
            Message = "Select the values";
           
            //MainMenuPage = new MainMenuPageViewModel(); //nueva instancia de menupage para SUBBINDING EN PAGES
            //ItemsPage = new ItemsPageViewModel("71382211", "1"); //nueva instancia de itempage para SUBBINDING EN PAGES
        }
        #endregion

        #region Commands
        public ICommand LoadProductsCommand { get { return new RelayCommand(LoadShoppingCar); } }

        public async void LoadShoppingCar()
        {
            //Categoria = pageapp + "Pagina bindada";
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
            try
            {
                IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress);
                string url = string.Format("/apirest/index.php/vercarrito/{0}/{1}", Settings.Phone, "1");
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                    IsRunning = false;
                    IsEnabled = false;
                    return;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    //Crear clase interface para notificaciones:
                    productoscarrito = JsonConvert.DeserializeObject<RootObjectProductosCarrito>(result);
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error De Conexión", ex.Message, "Aceptar");
                IsRunning = false;
                IsEnabled = false;
                return;
            }
            //

            SuccessfulLoadShoppingCar();
            IsRunning = false;
            IsEnabled = true;
            //}

        }

        private void SuccessfulLoadShoppingCar()
        {
            ItemShoppingCar = new ObservableCollection<ItemShoppingCar>();
            decimal total;

            for (int i = 0; i < productoscarrito.ProductosCarrito.Count; i++)
            {
                ItemShoppingCar.Add(new ItemShoppingCar
                {
                    Order = productoscarrito.ProductosCarrito[i].codpedidoactual,
                    UserId = productoscarrito.ProductosCarrito[i].codusuario,
                    Code = productoscarrito.ProductosCarrito[i].codarticulo,
                    Description = productoscarrito.ProductosCarrito[i].descripcion,
                    Image1 = Settings.ApiAddress + "/sistema/upload/" + productoscarrito.ProductosCarrito[i].foto,
                    Image2 = Settings.ApiAddress + "/sistema/upload/" + productoscarrito.ProductosCarrito[i].foto2,
                    Image3 = Settings.ApiAddress + "/sistema/upload/" + productoscarrito.ProductosCarrito[i].foto3,
                    Category = productoscarrito.ProductosCarrito[i].linea,
                    Qualification = productoscarrito.ProductosCarrito[i].calificacion,
                    Guarantee = productoscarrito.ProductosCarrito[i].garantia,
                    Quantity = productoscarrito.ProductosCarrito[i].cantidad,
                    Date = productoscarrito.ProductosCarrito[i].fecha.Substring(0, 10),
                    Price = productoscarrito.ProductosCarrito[i].precio,
                    Total = productoscarrito.ProductosCarrito[i].total,
                    Status = productoscarrito.ProductosCarrito[i].estado
                });

                try
                { total = Convert.ToDecimal(productoscarrito.ProductosCarrito[i].total); }
                catch (Exception e)
                { total = 0; }

                TotalCarrito = totalcarrito + total;
            }

            var sorted = from ItemShoppingCar in ItemShoppingCar
                         orderby ItemShoppingCar.Description
                         group ItemShoppingCar by ItemShoppingCar.DescriptionSort into monkeyGroup
                         select new Grouping<int, ItemShoppingCar>(monkeyGroup.Key, monkeyGroup);

            ItemsGrouped = new ObservableCollection<Grouping<int, ItemShoppingCar>>(sorted);

            if (TotalCarrito == 0)
            {
                IsVisible = false;
                App.Current.MainPage.DisplayAlert("Mensaje", "El Carrito no contiene articulos!", "Aceptar");
                return;
            }
            else
            {
                IsVisible = true;
            }

        }
        //public async void LoadShoppingCar()
        //{
        //    //IsRefreshing = true;
        //    //ShoppingCar.LoadProducts();
        //    //IsRefreshing = false;
        //}
        #endregion

        #region Methods
        public async void LoadApiResult(string phone)
        {

           
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
                try
                {
                    IsRunning = true;
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress);
                    string url = string.Format("/apirest/index.php/consultarcliente/{0}", phone);
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                        IsRunning = false;
                        IsEnabled = false;
                        return;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //Crear clase interface para notificaciones:
                        usuarios = JsonConvert.DeserializeObject<RootObjectUsuarios>(result);

                    }

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error De Conexión", ex.Message, "Aceptar");
                    IsRunning = false;
                    IsEnabled = false;
                    return;
                }
            //

                Successful();
                IsRunning = false;
                IsEnabled = true;
            //}

        }


        private void Successful()
        {
            if (usuarios.Usuarios[0].activo == "1")
            {
                App.Current.MainPage = new Pages.MainMenuPage();
            }
            else if (usuarios.Usuarios[0].activo == "0")
            {
                App.Current.MainPage = new Pages.InactiveUsr();
            }
            Settings.Name = usuarios.Usuarios[0].nombre;
            Settings.Phone = usuarios.Usuarios[0].telefonos;
            Settings.Email = usuarios.Usuarios[0].email;
            Settings.ActiveUser = usuarios.Usuarios[0].activo;
            Settings.Appointment = usuarios.Usuarios[0].cita;
            Settings.AppointmentStatus = usuarios.Usuarios[0].estadocita;
            Settings.Offert = usuarios.Usuarios[0].oferta;
        }


        #endregion

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion

    }
}
