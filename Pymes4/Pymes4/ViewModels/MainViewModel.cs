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
using System.Threading.Tasks;
using Pymes4.Pages;

namespace Pymes4.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes

        private NavigationService navigationService;

        public ItemsPageViewModel ItemsPage { get; set; }

        public ShoppingCarViewModel ShoppingCar { get; set; }

        public DeliveredViewModel Delivered { get; set; }

        public AppointmentPageViewModel AppointmentPage { get; set; }

        public CallNowPageViewModel CallNowPage { get; set; }

        public LocationPageViewModel LocationPage { get; set; }

        public OffersPageViewModel OffersPage { get; set; }

        public QualityPageViewModel QualityPage { get; set; }

        public SocialNetworksPageViewModel SocialNetworksPage { get; set; }

        private RootObjectUsuarios usuarios;

        private bool isRunning;

        private bool isEnabled;

        private string message;

        private string nota;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties


        public string Nota
        {
            set
            {
                if (nota != value)
                {
                    nota = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nota"));
                }
            }
            get
            {
                return nota;
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
            //Singleton - Para que pueda ser referenciada la mainview model desde otras view models
            instance = this;

            //Navigation
            navigationService = new NavigationService();

            IsEnabled = false;
            //GetRates();
            Settings.ApiAddress = "http://192.168.0.10";
            LoadApiResult();
            
            Message = "Select the values";

            AppointmentPage = new AppointmentPageViewModel();
            CallNowPage = new CallNowPageViewModel();
            LocationPage = new LocationPageViewModel();
            OffersPage = new OffersPageViewModel();
            QualityPage = new QualityPageViewModel();
            SocialNetworksPage = new SocialNetworksPageViewModel();

            //MainMenuPage = new MainMenuPageViewModel(); //nueva instancia de menupage para SUBBINDING EN PAGES
            //ItemsPage = new ItemsPageViewModel("71382211", "1"); //nueva instancia de itempage para SUBBINDING EN PAGES
        }
        #endregion

        #region Commands

        public ICommand LoadAppointmentCommand { get { return new RelayCommand(LoadAppointment); } }
        public ICommand LoadShoppingCarCommand { get { return new RelayCommand(LoadShoppingCar); } }
        public ICommand LoadDeliveredCommand { get { return new RelayCommand(LoadDelivered); } }
        
        //public ICommand CreateOrderCommand { get { return new RelayCommand(CreateOrder); } }


        #endregion

        #region Methods
        public async void LoadAppointment()
        {
            //Triger que se ejecuta para actualizar la view model, cada vez que se muestra la ventana
            AppointmentPage.LoadApiResult();
        }
        public async void LoadShoppingCar()
        {
            //Triger que se ejecuta para actualizar la view model, cada vez que se muestra la ventana
            ShoppingCar.LoadProducts(); 
        }
        public async void LoadDelivered()
        {
            //Triger que se ejecuta para actualizar la view model, cada vez que se muestra la ventana
            Delivered.LoadProducts();
        }
       
        public async void LoadApiResult()
        {
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
                try
                {
                    IsRunning = true;
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(Settings.ApiAddress);
                    string url = string.Format("/apirest/index.php/consultarcliente/{0}", Settings.Phone);
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
                Settings.Name = usuarios.Usuarios[0].nombre;
                Settings.Phone = usuarios.Usuarios[0].telefonos;
                Settings.Email = usuarios.Usuarios[0].email;
                Settings.ActiveUser = usuarios.Usuarios[0].activo;
                Settings.Appointment = usuarios.Usuarios[0].cita;
                Settings.AppointmentStatus = usuarios.Usuarios[0].estadocita;
                Settings.Offert = usuarios.Usuarios[0].oferta;

                App.Current.MainPage = new Pages.MainMenuPage();
            }
            else if (usuarios.Usuarios[0].activo == "0")
            {
                App.Current.MainPage = new Pages.InactiveUsr();
            }
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
