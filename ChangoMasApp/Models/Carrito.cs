using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.Models
{
    public class Carrito
    {
        public int IdCarrito { get; set; }

        public int? IdUsuario { get; set; }

        public string Estado { get; set; }

        public decimal? Total { get; set; }
    }
}
