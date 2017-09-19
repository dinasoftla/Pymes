using GalaSoft.MvvmLight.Command;
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
    public class LocationPageViewModel
    {
             
        #region Constructor
        public LocationPageViewModel()
    {
    }
    #endregion

    #region Commands

    public ICommand OpenMapsCommand { get { return new RelayCommand(OpenMaps); } }
    public ICommand OpenWazeCommand { get { return new RelayCommand(OpenWaze); } }

    #endregion

    #region Methods

        private async void OpenMaps()
        {
                Device.OpenUri(new Uri("https://www.google.com.mx/maps/place/HARDY+Maquinas+Para+Ejercicios/@9.902288,-84.0682447,17z/data=!3m1!4b1!4m5!3m4!1s0x8fa0e3044c224a7f:0x57861ddcd21f7587!8m2!3d9.902288!4d-84.066056"));
        }
        private async void OpenWaze()
        {
            Device.OpenUri(new Uri("https://waze.to/lr/hw2838qqcx"));
        }
        #endregion
    }
}
