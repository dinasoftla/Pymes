using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pymes4.Classes
{

    public class Categorias
    {
        public string cod_linea { get; set; }
        public string descripcion { get; set; }
    }

    public class RootObjectCategorias
    {
        [JsonProperty(PropertyName = "categorias")]
        public List<Categorias> Categorias { get; set; }
    }
}
