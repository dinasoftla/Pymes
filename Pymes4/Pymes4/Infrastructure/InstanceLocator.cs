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

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }

}
