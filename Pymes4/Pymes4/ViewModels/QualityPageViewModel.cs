using GalaSoft.MvvmLight.Command;
using Pymes4.Helpers;
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
    public class QualityPageViewModel
    {
            

        #region Constructor
    public QualityPageViewModel()
    {
    }
    #endregion

    #region Commands

    public ICommand QualifyCommand { get { return new RelayCommand(Qualify); } }

        #endregion

        #region Methods

        private async void Qualify()
        {
            string calificacion = "Estrellasbindadas";
            try
            {
                string insertResult = string.Empty;

                HttpClient client = new HttpClient();

                //client.BaseAddress = new Uri("http://openexchangerates.org");
                //string url = string.Format("/api/latest.json?app_id=e511adb5bb0b4d2d9dd6d2a3b8fc21a1");

                client.BaseAddress = new Uri(Settings.ApiAddress);
                string url = string.Format("/apirest/index.php/actualizarcalificacion/{0}/{1}", Settings.Phone, calificacion);

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {

                }

                insertResult = await response.Content.ReadAsStringAsync();


            }
            catch (Exception ex)
            {

            }
        }
        
    #endregion
}
}
