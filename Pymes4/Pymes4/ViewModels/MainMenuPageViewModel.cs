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
    public class MainMenuPageViewModel : INotifyPropertyChanged
    {
        #region Attributes

        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; set; }

        public int ItemCount => Items.Count;

        private RootObjectProductos productos;

        private bool isRunning;

        private bool isEnabled;

        private string message;


        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Item;
            if (item == null)
                return;
        }

        #endregion

        #region Properties

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

        public MainMenuPageViewModel()
        {
            //IsEnabled = false;
            ////GetRates();
            //LoadApiResult(Settings.Phone);
            //Message = "Select the values";

            //LoadApiResult("71382211", "1");
            

        }
        #endregion

        #region Commands

        public ICommand RetriveProductsCommand { get { return new RelayCommand(LoadProducts); } }

        private async void LoadProducts()
        {
            LoadApiResult("71382211", "2");



        }

        #endregion


        #region Methods
        
        public async void LoadApiResult(string phone, string pageapp)
        {
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
            try
            {
                IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://192.168.0.17");
                string url = string.Format("/apirest/index.php/consultaproductos/{0}/{1}", phone, pageapp);
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
                    productos = JsonConvert.DeserializeObject<RootObjectProductos>(result);
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
            Items = new ObservableCollection<Item>();

            for (int i = 0; i < productos.Productos.Count; i++)
            {
                Items.Add(new Item
                {
                    Code = productos.Productos[i].codarticulo,
                    Name = productos.Productos[i].descripcion,
                    Image = productos.Productos[i].foto,
                    Description = productos.Productos[i].caracteristicas,
                    Price = productos.Productos[i].precio
                });
            }

            var sorted = from item in Items
                         orderby item.Name
                         group item by item.NameSort into monkeyGroup
                         select new Grouping<string, Item>(monkeyGroup.Key, monkeyGroup);

            ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sorted);

        }


        #endregion
    }
   
}

