using Pymes4.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pymes4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : TabbedPage
    {
        
        public OrdersPage()
        {

            Page categorias = new Categories();
            Children.Add(categorias);


            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ShoppingCar = new ShoppingCarViewModel();
            mainViewModel.Delivered = new DeliveredViewModel();


            Page shoppingCar = new ShoppingCar();
            Children.Add(shoppingCar);

            Page delivered = new Delivered();
            Children.Add(delivered);

        }

    }
}