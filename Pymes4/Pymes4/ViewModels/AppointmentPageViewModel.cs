using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Pymes4.Classes;
using Pymes4.Helpers;
using Pymes4.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pymes4.ViewModels
{
   
    public class AppointmentPageViewModel:INotifyPropertyChanged
    {
        #region Attributes

        DateTime selectedDateIndexPicker = DateTime.Now;
        private string selectedHourIndexPicker;
        private bool isVisible;
        private string mensaje;
        private RootObjectUsuarios usuarios;
        #endregion

        #region Properties

        public DateTime SelectedDateIndexPicker
        {
            set
            {
                selectedDateIndexPicker = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedDateIndexPicker"));
            }
            get
            {
                return selectedDateIndexPicker;
            }
        }

        public string SelectedHourIndexPicker
        {
            set
            {
                if (selectedHourIndexPicker != value)
                {
                    selectedHourIndexPicker = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedHourIndexPicker"));
                }
            }
            get
            {
                return selectedHourIndexPicker;
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
        public string Mensaje
        {
            set
            {
                if (mensaje != value)
                {
                    mensaje = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mensaje"));
                }
            }
            get
            {
                return mensaje;
            }
        }
        #endregion

        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public AppointmentPageViewModel()
        {
            
        }
        #endregion

        #region Commands

        public ICommand CreateAppointmentCommand { get { return new RelayCommand(CreateAppointment); } }

        #endregion

        #region Methods

        public async void LoadApiResult()
        {
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress);
                string url = string.Format("/apirest/index.php/consultarcliente/{0}", Settings.Phone);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                 
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
                return;
            }
            //

            Successful();
            //}

        }


        private void Successful()
        {
            Settings.AppointmentStatus = usuarios.Usuarios[0].estadocita;
            Settings.Appointment = usuarios.Usuarios[0].cita;

            UpdateView();
        }


        public void UpdateView()
        {
            //var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.LoadAppointmentCommand.Execute(this);

          
            if (Settings.AppointmentStatus == "P")
            {
                IsVisible = false;
                Mensaje = "ESTADO: Esperando la confirmación de su cita para el dia: " + Settings.Appointment;
            }
            else if (Settings.AppointmentStatus == "A")
            {
                IsVisible = false;
                Mensaje = "ESTADO: Su cita fue aprobada, esperamos su visita, para el dia: " + Settings.Appointment;
            }
            else
            {
                IsVisible = true;
                Mensaje = "";
            }

        }
        private async void CreateAppointment()
        {
            try
            {
                double horas = Convert.ToDouble(SelectedHourIndexPicker.Substring(0, 2));
                double minutos = Convert.ToDouble(SelectedHourIndexPicker.Substring(3, 2));

                SelectedDateIndexPicker = SelectedDateIndexPicker.AddHours(horas);
                SelectedDateIndexPicker = SelectedDateIndexPicker.AddMinutes(minutos);

                string fecha = SelectedDateIndexPicker.ToString("yyyy/MM/dd HH:mm");
                fecha = fecha.Replace(" ", "x").Replace("/", "-");

                string insertResult = string.Empty;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress);
                string url = string.Format("/apirest/index.php/crearcita/{0}/{1}", Settings.Phone , fecha);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                    return;
                }

                insertResult = await response.Content.ReadAsStringAsync();

                LoadApiResult();

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

    }



}
