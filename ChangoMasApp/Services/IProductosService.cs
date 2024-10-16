using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangoMasApp.Models;

namespace ChangoMasApp.Services
{
    public interface IProductosService
    {
        Task <IEnumerable<Productos>> GetListaProductosAsync();
        Task<bool> AgregarProductoAsync(Productos producto);
        Task<bool> EditarProductoAsync(Productos producto);
        Task<bool> EliminarProducto(int id);
        Task<Productos> GetProductoAsync(int id);

    }
}
