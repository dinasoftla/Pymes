using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pymes4.Classes
{
    public class ProductoCarrito
    {
        public string codusuario { get; set; }
        public string codarticulo { get; set; }
        public string foto { get; set; }
        public string descripcion { get; set; }
        public string cantidad { get; set; }
        public string precio { get; set; }
        public string total { get; set; }
        public string estado { get; set; }
        public string fecha { get; set; }
        public string codpedidoactual { get; set; }
    }

    public class RootObjectProductosCarrito
    {
        [JsonProperty(PropertyName = "productoscarrito")]
        public List<ProductoCarrito> ProductosCarrito { get; set; }
    }
}
