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
    public class CallNowPageViewModel
    {
        #region Constructor
        public CallNowPageViewModel()
        {
        }
        #endregion

        #region Commands

        public ICommand CallNowCommand { get { return new RelayCommand(CallNow); } }

        #endregion

        #region Methods

        private async void CallNow()
        {
            Device.OpenUri(new Uri("tel:1112223333"));
        }
        #endregion
    }
}
