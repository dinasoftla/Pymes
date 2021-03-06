﻿using GalaSoft.MvvmLight.Command;
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
    
    public class ShoppingCarViewModel : INotifyPropertyChanged
    {
        #region Attributes

        INavigation Navigation;// Se declara la variable de navegacion 1) parte

        public ObservableCollection<ItemShoppingCar> ItemShoppingCar { get; set; }

        public ObservableCollection<Grouping<int, ItemShoppingCar>> itemsGrouped { get; set; }

        public int ItemCount => ItemShoppingCar.Count;

        private RootObjectProductosCarrito productoscarrito;

        private bool isRunning;

        private bool isEnabled;

        private bool isRefreshing;

        private string totalcarrito;

        private string nota;

        private string message;

        private string categoria;

        private bool isVisible;

        private bool emptyShoppingCarVisible;

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
        public string TotalCarrito
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
        
        public bool IsRefreshing
        {
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
            get
            {
                return isRefreshing;
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
        public bool EmptyShoppingCarVisible
        {
            set
            {
                if (emptyShoppingCarVisible != value)
                {
                    emptyShoppingCarVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmptyShoppingCarVisible"));
                }
            }
            get
            {
                return emptyShoppingCarVisible;
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
        #endregion

        #region Constructors

        public ShoppingCarViewModel()
        {

        }
        #endregion

        #region Commands

        public ICommand CreateOrderCommand { get { return new RelayCommand(CreateOrder); } }
        public ICommand LoadProductsCommand { get { return new RelayCommand(LoadProducts); } }



        #endregion

        #region Methods
        public async void LoadProducts()
        {
            IsRefreshing = true;
            EmptyShoppingCarVisible = true;
            LoadApiResult(Settings.Phone, "1");
            IsRefreshing = false;
        }

        private async void CreateOrder()
        {

            string nota = string.Empty;
            if (String.IsNullOrEmpty(Nota))
            {
                nota = "Ninguna";
            }
            else
            {
                nota = Nota;
            }

            try
            {
                IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress);
                string url = string.Format("/apirest/index.php/crearpedido/{0}/{1}", Settings.Phone, nota);
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
                    //var result = await response.Content.ReadAsStringAsync();
                    ////Crear clase interface para notificaciones:
                    //usuarios = JsonConvert.DeserializeObject<RootObjectUsuarios>(result);
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
        public async void LoadApiResult(string phone, string pageapp)
        {
            //Categoria = pageapp + "Pagina bindada";
            //if (!String.IsNullOrEmpty(Settings.Phone))
            //{
            try
            {
                IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress);
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
            decimal total;
            TotalCarrito = "₡ 0";
            decimal totalcarrito = 0;

            for (int i = 0; i < productoscarrito.ProductosCarrito.Count; i++)
            {
                ItemShoppingCar.Add(new ItemShoppingCar
                {
                    Order = productoscarrito.ProductosCarrito[i].codpedidoactual,
                    UserId = productoscarrito.ProductosCarrito[i].codusuario,
                    Code = productoscarrito.ProductosCarrito[i].codarticulo,
                    Description = productoscarrito.ProductosCarrito[i].descripcion,
                    Image1 = Settings.ApiAddress + "/sistema/upload/" + productoscarrito.ProductosCarrito[i].foto,
                    Image2 = Settings.ApiAddress + "/sistema/upload/" + productoscarrito.ProductosCarrito[i].foto2,
                    Image3 = Settings.ApiAddress + "/sistema/upload/" + productoscarrito.ProductosCarrito[i].foto3,
                    Category = productoscarrito.ProductosCarrito[i].linea,
                    Qualification = productoscarrito.ProductosCarrito[i].calificacion,
                    Guarantee = productoscarrito.ProductosCarrito[i].garantia,
                    Quantity = productoscarrito.ProductosCarrito[i].cantidad,
                    Date = productoscarrito.ProductosCarrito[i].fecha.Substring(0, 10),
                    Price = "₡ " + productoscarrito.ProductosCarrito[i].precio,
                    Total = "₡ " + productoscarrito.ProductosCarrito[i].total,
                    Status = productoscarrito.ProductosCarrito[i].estado
                });

                try
                {
                    total = Convert.ToDecimal(productoscarrito.ProductosCarrito[i].total.Replace(".",",")); }
                catch (Exception e)
                { total = 0; }

                totalcarrito = totalcarrito + total;
            }

            var sorted = from ItemShoppingCar in ItemShoppingCar
                         orderby ItemShoppingCar.Description
                         group ItemShoppingCar by ItemShoppingCar.DescriptionSort into monkeyGroup
                         select new Grouping<int, ItemShoppingCar>(monkeyGroup.Key, monkeyGroup);

            ItemsGrouped = new ObservableCollection<Grouping<int, ItemShoppingCar>>(sorted);

            if (totalcarrito == 0)
            {
                IsVisible = false;
                EmptyShoppingCarVisible = true;
                return;
            }
            else
            {
                EmptyShoppingCarVisible = false;
                IsVisible = true;
            }

            TotalCarrito = "₡ " + totalcarrito;
            TotalCarrito = TotalCarrito.Replace(",", ".");
        }
        public async Task OrderSuccessful()
        {
            //Remueve la pagina superior de la navegacion (Es como dar click en "atras")
            //La variable Navigation fue recibida de la ventana anterior, fue necesario declararla en la clase actual para poder usarla;

            //await Navigation.PopAsync();
            //await Navigation.PushAsync(new Delivered(Settings.Phone));

            LoadApiResult(Settings.Phone, "1");

            //App.Current.MainPage = new MainMenuPage();
            await App.Current.MainPage.DisplayAlert("Orden Generada", "Puede revisar los pedidos en la opción Enviados 'Enviados'", "Aceptar");
            //Recibe la pagina de navegacion para ser utilizada 2) parte
            //Navigation = PageNav;
        }


        #endregion
    }
}
