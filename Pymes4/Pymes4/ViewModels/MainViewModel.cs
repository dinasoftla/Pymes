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

        private RootObjectUsuario usuarios;

        private decimal amount;

        private double sourceRate;

        private double targetRate;

        private bool isRunning;

        private bool isEnabled;

        private string message;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public ObservableCollection<Rate> Rates { get; set; }

        public decimal Amount
        {
            set
            {
                if (amount != value)
                {
                    amount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Amount"));
                }
            }
            get
            {
                return amount;
            }
        }

        public double SourceRate
        {
            set
            {
                if (sourceRate != value)
                {
                    sourceRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceRate"));
                }
            }
            get
            {
                return sourceRate;
            }
        }

        public double TargetRate
        {
            set
            {
                if (targetRate != value)
                {
                    targetRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TargetRate"));
                }
            }
            get
            {
                return targetRate;
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
           
            Rates = new ObservableCollection<Rate>();
            IsEnabled = false;
            //GetRates();
            LoadApiResult(Settings.Phone);
            Message = "Select the values";

        }
        #endregion

        #region Commands

        public ICommand ConvertCommand { get { return new RelayCommand(ConvertMoney); } }

        private async void  ConvertMoney()
        {
            if (Amount <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an amount", "Acept");
                return;
            }

            if (SourceRate == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must select a source rate", "Acept");
                return;
            }

            if (TargetRate == -1)
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must select a target rate", "Acept");
                return;
            }

            decimal amountConverted = amount / (decimal)sourceRate * (decimal)targetRate;

            Message = string.Format("{0:N2} = {1:N2}", amount, amountConverted);

        }



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
                    client.BaseAddress = new Uri("http://192.168.0.10");
                    string url = string.Format("/apirest/index.php/consultacliente/{0}", phone);
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
                        usuarios = JsonConvert.DeserializeObject<RootObjectUsuario>(result);

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
            if (usuarios.Usuarios[0].usuarioactivo == "1")
            {
                App.Current.MainPage = new Pages.MenuPage();
            }
            else if (usuarios.Usuarios[0].usuarioactivo == "0")
            {
                App.Current.MainPage = new Pages.InactiveUsr();
            }
            Settings.Name = usuarios.Usuarios[0].nombre;
            Settings.Phone = usuarios.Usuarios[0].telefono;
            Settings.Email = usuarios.Usuarios[0].email;
            Settings.ActiveUser = usuarios.Usuarios[0].usuarioactivo;
            Settings.Appointment = usuarios.Usuarios[0].cita;
            Settings.AppointmentStatus = usuarios.Usuarios[0].estadocita;
            Settings.Offert = usuarios.Usuarios[0].oferta;

        }


        #endregion
    }
}
