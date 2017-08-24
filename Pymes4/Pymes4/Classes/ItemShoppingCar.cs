using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pymes4.Classes
{
    public class ItemShoppingCar
    {
        public string Order { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Date { get; set; }
        //URL for our monkey image!
        public string Price { get; set; }
        public string Total { get; set; }
        public string Guarantee { get; set; }
        public string Qualification { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }

        public int DescriptionSort => Order[0];


    }
}
