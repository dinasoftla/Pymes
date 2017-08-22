using Pymes4.Classes;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Pymes4.Helpers;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace Pymes4.ViewModels
{
    public class ItemsPageDetailAddViewModel : INotifyPropertyChanged
    {
        #region Attributes

        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<Grouping<string, Item>> itemsGrouped { get; set; }

        private string code;

        private string message;
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
  
        public ObservableCollection<Grouping<string, Item>> ItemsGrouped
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
        public string Code
        {
            set
            {
                if (code != value)
                {
                    code = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Code"));
                }
            }
            get
            {
                return code;
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

        public ItemsPageDetailAddViewModel(Item item)
        {
            Code = item.Code;

            //ItemsGrouped = new ObservableCollection<Grouping<string, Item>>();

          
            //CARGAR VENTANA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        }
        #endregion

        #region Commands

        public ICommand RetriveProductsCommand { get { return new RelayCommand(LoadProducts); } }

        private async void LoadProducts()
        {
            //LoadApiResult("71382211", "2");
        }

        #endregion


        #region Methods
        
        public async void LoadApiResult(string phone, string pageapp)
        {


        }

  


        #endregion
    }
   
}

