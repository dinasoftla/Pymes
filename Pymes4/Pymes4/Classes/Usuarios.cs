using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pymes4.Classes
{

    public class Usuario
    {
        public string idusuario { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string activo { get; set; }
        public string cita { get; set; }
        public string estadocita { get; set; }
        public string oferta { get; set; }
    }

    public class RootObjectUsuarios
    {
        [JsonProperty(PropertyName = "usuarios")]
        public List<Usuario> Usuarios { get; set; }
    }
}
