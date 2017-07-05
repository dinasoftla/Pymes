using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pymes4.Classes
{
    public class Item
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        //URL for our monkey image!
        public string Price { get; set; }


        public string NameSort => Name[0].ToString();


    }
}
