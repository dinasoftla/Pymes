using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pymes4.Classes
{
    public class Producto
    {
        public string codarticulo { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }
        public string precio { get; set; }
        public string caracteristicas { get; set; }
        public string linea { get; set; }
        public string calificacion { get; set; }
        public string totalpaginas { get; set; }
    }

    public class RootObjectProductos
    {
        [JsonProperty(PropertyName = "productos")]
        public List<Producto> Productos { get; set; }
    }
}
