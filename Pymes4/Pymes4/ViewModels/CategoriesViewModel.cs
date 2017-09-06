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

    public class CategoriesViewModel : INotifyPropertyChanged
    {
        #region Attributes

        INavigation Navigation;// Se declara la variable de navegacion 1) parte

        private RootObjectCategorias categorias;

        public ObservableCollection<ItemShoppingCar> ItemShoppingCar { get; set; }

        public ObservableCollection<Grouping<int, ItemShoppingCar>> itemsGrouped { get; set; }

        public int ItemCount => ItemShoppingCar.Count;

        private bool isRunning;

        private bool isEnabled;

        private decimal totalcarrito;

        private string nota;

        private string message;

        private string categoria;

        StackLayout stacklayout;

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
        public decimal TotalCarrito
        {
            set
            {
                if (totalcarrito != value)
                {
                    totalcarrito = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalCarrito"));
                }
            }
            get
            {
                return totalcarrito;
            }
        }

        public string Nota
        {
            set
            {
                if (nota != value)
                {
                    nota = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nota"));
                }
            }
            get
            {
                return nota;
            }
        }

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
        public StackLayout Stacklayout
        {
            set
            {
                if (stacklayout != value)
                {
                    stacklayout = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StackLayout"));
                }
            }
            get
            {
                return stacklayout;
            }
        }
        #endregion

        #region Constructors

        public CategoriesViewModel(StackLayout MenuStack, INavigation PageNav)
        {
      
            //Recibe la pagina de navegacion para ser utilizada 2) parte
            Navigation = PageNav;
            Stacklayout = MenuStack;
            CreateOrder();
        }
        #endregion

        #region Commands

        public ICommand CreateOrderCommand { get { return new RelayCommand(CreateOrder); } }

        public async void CreateOrder()
        {
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
            try
            {
                IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress);
                string url = string.Format("/apirest/index.php/consultarcategorias/{0}", Settings.Phone);
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
                    categorias = JsonConvert.DeserializeObject<RootObjectCategorias>(result);
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

            OrderSuccessful();
            IsRunning = false;
            IsEnabled = true;
            //}

        }

        #endregion


        #region Methods

        public async Task OrderSuccessful()
        {
         
            string cod_linea = String.Empty;
            string description = String.Empty;

            for (int i = 0; i < categorias.Categorias.Count; i++)
            {
                cod_linea = categorias.Categorias[i].cod_linea;
                description = categorias.Categorias[i].descripcion;
                Stacklayout.Children.Add(new Button() { Text = description, Command = new Command(OnAddControl), CommandParameter = cod_linea });

            }
        }
        private void OnAddControl(object parameter)
        {

            OrderSuccessful(Convert.ToString(parameter));

        }
        public async Task OrderSuccessful(string parameter)
        {
            //Remueve la pagina superior de la navegacion (Es como dar click en "atras")
            //La variable Navigation fue recibida de la ventana anterior, fue necesario declararla en la clase actual para poder usarla;
            await Navigation.PushAsync(new ItemsPage("1",parameter));

        }

        #endregion
    }
}
