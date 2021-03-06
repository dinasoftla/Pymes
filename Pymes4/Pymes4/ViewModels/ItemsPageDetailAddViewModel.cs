﻿using Pymes4.Classes;
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
using Pymes4.Pages;

namespace Pymes4.ViewModels
{
    public class ItemsPageDetailAddViewModel : INotifyPropertyChanged
    {
        INavigation Navigation;// Se declara la variable de navegacion 1) parte

        #region Attributes

        private bool isEnabled;
        private bool isRunning;
        private string code;
        private string name;
        private string description;
        private string image1;
        private string image2;
        private string image3;
        private string category;
        private string qualification;
        private string guarantee;
        private string price;
        private int cantidadPedida;

        private string message;
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

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

        public string Name
        {
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
            get
            {
                return name;
            }
        }
        public string Description
        {
            set
            {
                if (description != value)
                {
                    description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
                }
            }
            get
            {
                return description;
            }
        }
        public string Image1
        {
            set
            {
                if (image1 != value)
                {
                    image1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
                }
            }
            get
            {
                return image1;
            }
        }
        public string Image2
        {
            set
            {
                if (image2 != value)
                {
                    image2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image2"));
                }
            }
            get
            {
                return image2;
            }
        }
        public string Image3
        {
            set
            {
                if (image3 != value)
                {
                    image3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image3"));
                }
            }
            get
            {
                return image3;
            }
        }
        public string Category
        {
            set
            {
                if (category != value)
                {
                    category = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category"));
                }
            }
            get
            {
                return category;
            }
        }
        public string Qualification
        {
            set
            {
                if (qualification != value)
                {
                    qualification = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Qualification"));
                }
            }
            get
            {
                return qualification;
            }
        }
        public string Guarantee
        {
            set
            {
                if (guarantee != value)
                {
                    guarantee = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Guarantee"));
                }
            }
            get
            {
                return guarantee;
            }
        }
        public string Price
        {
            set
            {
                if (price != value)
                {
                    price = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
                }
            }
            get
            {
                return price;
            }
        }
        public int CantidadPedida
        {
            set
            {
                if (cantidadPedida != value)
                {
                    cantidadPedida = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CantidadPedida"));
                }
            }
            get
            {
                return cantidadPedida;
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

        public ItemsPageDetailAddViewModel(Item item, INavigation PageNav)
        {
            CantidadPedida = 1;

            Code = item.Code;
            Name = item.Name;
            Description = item.Description;
            Image1 = item.Image;
            Image2 = item.Image2;
            Image3 = item.Image3;
            Category = item.Category;
            Qualification = item.Qualification;
            Guarantee = item.Guarantee;
            Price = item.Price;

            //Recibe la pagina de navegacion para ser utilizada 2) parte
            Navigation = PageNav;
          

        }
        #endregion

        #region Commands

        public ICommand OrderProductCommand { get { return new RelayCommand(OrderProduct); } }

        #endregion


        #region Methods

        private async void OrderProduct()
        {
            if (CantidadPedida > 0)
            {
                AgregarArticulo();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "La cantidad debe ser mayor a Cero", "Aceptar");
            }
            //}
        }
        private async void AgregarArticulo()
        {
            try
            {
                IsRunning = true;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Settings.ApiAddress);
                string url = string.Format("/apirest/index.php/agregaralpedido/{0}/{1}/{2}", Settings.Phone, Code, CantidadPedida);
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

            Successful();
            IsRunning = false;
            IsEnabled = true;
        }

        public async Task Successful()
        {
            await App.Current.MainPage.DisplayAlert("Mensaje", "El Articulo fue agregado al Carrito!", "Aceptar");

            //Remueve la pagina superior de la navegacion (Es como dar click en "atras")
            //La variable Navigation fue recibida de la ventana anterior, fue necesario declararla en la clase actual para poder usarla;
            await Navigation.PopToRootAsync();// 3) parte
        }


        #endregion
    }

}

