using Pymes4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pymes4.Infrastructure
{
    public class InstanceLocator //Clase Creada a pie para relacionar el mainviewmodel
    {
        public MainViewModel Main { get; set; }
        public ShoppingCarViewModel ShoppingCar { get; set; }
        public DeliveredViewModel Delivered { get; set; }
        public AppointmentPageViewModel AppointmentPage { get; set; }
        public CallNowPageViewModel CallNowPage { get; set; }
        public LocationPageViewModel LocationPage { get; set; }
        public OffersPageViewModel OffersPage { get; set; }
        public QualityPageViewModel QualityPage { get; set; }
        public SocialNetworksPageViewModel SocialNetworksPage { get; set; }


        public InstanceLocator()
        {
            Main = new MainViewModel();
            ShoppingCar = new ShoppingCarViewModel();
            Delivered = new DeliveredViewModel();
            AppointmentPage = new AppointmentPageViewModel();
            CallNowPage = new CallNowPageViewModel();
            LocationPage = new LocationPageViewModel();
            OffersPage = new OffersPageViewModel();
            QualityPage = new QualityPageViewModel();
            SocialNetworksPage = new SocialNetworksPageViewModel();
        }
    }

}
