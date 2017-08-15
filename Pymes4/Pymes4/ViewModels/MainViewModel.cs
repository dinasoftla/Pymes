using Pymes4.Classes;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Pymes4.Helpers;

namespace Pymes4.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes

        private RootObjectUsuarios usuarios;

        private bool isRunning;

        private bool isEnabled;

        private string message;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public ItemsPageViewModel ItemsPage { get; set; }
        public MainMenuPageViewModel MainMenuPage { get; set; }



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
            IsEnabled = false;
            //GetRates();
            LoadApiResult(Settings.Phone);
            Message = "Select the values";
           
            //MainMenuPage = new MainMenuPageViewModel(); //nueva instancia de menupage para SUBBINDING EN PAGES
            //ItemsPage = new ItemsPageViewModel("71382211", "1"); //nueva instancia de itempage para SUBBINDING EN PAGES
        }
        #endregion

        #region Commands
       
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
                    client.BaseAddress = new Uri("http://192.168.0.17");
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
            Settings.Phone = usuarios.Usuarios[0].telefono;
            Settings.Email = usuarios.Usuarios[0].email;
            Settings.ActiveUser = usuarios.Usuarios[0].activo;
            Settings.Appointment = usuarios.Usuarios[0].cita;
            Settings.AppointmentStatus = usuarios.Usuarios[0].estadocita;
            Settings.Offert = usuarios.Usuarios[0].oferta;
        }


        #endregion
    }
}
