using ChangoMasApp.Models;

namespace ChangoMasApp.Services
{
    public interface ICarritoService
    {
        Task<Carrito> GetCarritoDelUsuarioAsync();
        Task<bool> CrearCarritoProducto(int idProducto);
        Task<List<Productos>> GetListaProductoDeCarritoAsync();
        Task<CarritoProducto> GetCarritoProductoDelProductoAsync(int id);
        Task<bool> EliminarProductoDelCarrito(int id);
    }
}
