using GalaSoft.MvvmLight.Command;
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
    public class SocialNetworksPageViewModel
    {
             

        #region Constructor
        public SocialNetworksPageViewModel()
    {
    }
    #endregion

    #region Commands

    public ICommand CreateAppointmentCommand { get { return new RelayCommand(MET); } }

    #endregion

    #region Methods

    private async void MET()
    {



    }
    #endregion
}
}
