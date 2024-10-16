using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.Models
{
    public class ProductoCarrito
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        // Puedes calcular el subtotal aquí
        public decimal Subtotal => Cantidad * Precio;
    }
}
