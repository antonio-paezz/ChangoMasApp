using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.Models
{
    public class CarritoProductoRequest
    {
        public int idUsuario { get; set; }

        public int idProducto { get; set; }

        public int? cantidad { get; set; }
    }
}
