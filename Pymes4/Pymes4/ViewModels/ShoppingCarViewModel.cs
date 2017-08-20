using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Pymes4.Classes;
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
    
    public class ShoppingCarViewModel : INotifyPropertyChanged
    {
        #region Attributes

        public ObservableCollection<ItemShoppingCar> ItemShoppingCar { get; set; }

        public ObservableCollection<Grouping<int, ItemShoppingCar>> itemsGrouped { get; set; }

        public int ItemCount => ItemShoppingCar.Count;

        private RootObjectProductosCarrito productoscarrito;

        private bool isRunning;

        private bool isEnabled;

        private string message;

        private string categoria;
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
        public string Categoria
        {
            set
            {
                if (categoria != value)
                {
                    categoria = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Categoria"));
                }
            }
            get
            {
                return categoria;
            }
        }
        public ObservableCollection<Grouping<int, ItemShoppingCar>> ItemsGrouped
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

        public ShoppingCarViewModel(String telefono, string pageapp)
        {
            LoadApiResult(telefono, pageapp);
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
            //Categoria = pageapp + "Pagina bindada";
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
            try
            {
                IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://192.168.0.17");
                string url = string.Format("/apirest/index.php/vercarrito/{0}/{1}", phone, pageapp);
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
                    productoscarrito = JsonConvert.DeserializeObject<RootObjectProductosCarrito>(result);
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
            ItemShoppingCar = new ObservableCollection<ItemShoppingCar>();

            for (int i = 0; i < productoscarrito.ProductosCarrito.Count; i++)
            {
                ItemShoppingCar.Add(new ItemShoppingCar
                {
                    Order = productoscarrito.ProductosCarrito[i].codpedidoactual,
                    UserId = productoscarrito.ProductosCarrito[i].codusuario,
                    Code = productoscarrito.ProductosCarrito[i].codarticulo,
                    Image = "http://192.168.0.17/sistema/upload/" + productoscarrito.ProductosCarrito[i].foto,
                    Description = productoscarrito.ProductosCarrito[i].descripcion,
                    Quantity = productoscarrito.ProductosCarrito[i].cantidad,
                    Date = productoscarrito.ProductosCarrito[i].fecha.Substring(0,10),
                    Price = productoscarrito.ProductosCarrito[i].precio,
                    Total = productoscarrito.ProductosCarrito[i].total,
                    Status = productoscarrito.ProductosCarrito[i].estado 
                });
            }

            var sorted = from ItemShoppingCar in ItemShoppingCar
                         orderby ItemShoppingCar.Description
                         group ItemShoppingCar by ItemShoppingCar.DescriptionSort into monkeyGroup
                         select new Grouping<int, ItemShoppingCar>(monkeyGroup.Key, monkeyGroup);

            ItemsGrouped = new ObservableCollection<Grouping<int, ItemShoppingCar>>(sorted);

        }


        #endregion
    }
}
