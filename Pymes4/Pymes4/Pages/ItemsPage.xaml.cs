﻿using Pymes4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pymes4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();
            
        }

        #region Poner en el view model (OJO ESTO ES TEMPORAL)
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

            void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
            {
                var item = ((ListView)sender).SelectedItem as Item;
                if (item == null)
                    return;
            }
        #endregion

    }
    
}