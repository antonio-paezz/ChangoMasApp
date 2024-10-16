using ChangoMasApp.Services;
using ChangoMasApp.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;

namespace ChangoMasApp.ViewModels
{
    public partial class CarritoViewModel : BaseViewModel
    {
        public ObservableCollection<ProductoCarrito> Productos { get; set; }
        public decimal Total { get; private set; }

        [ObservableProperty]
        bool isRefreshing;

        private readonly IProductosService _productosService;
        private readonly ICarritoService _carritoService;
        private readonly IEliminarService _eliminarService;


        public CarritoViewModel(IProductosService productosService, ICarritoService carritoService, IEliminarService eliminarService)
        {
            _productosService = productosService;
            _carritoService = carritoService;
            _eliminarService = eliminarService;
            Productos = new ObservableCollection<ProductoCarrito>();
        }

        [RelayCommand]
        public async Task AgregarProductoAlCarrito()
        {
            var productoId = await SecureStorage.GetAsync("productoId");

            // Obtener el producto seleccionado
            var producto = await _productosService.GetProductoAsync(int.Parse(productoId));

            // Verificar si el producto ya está en el carrito
            var productoExistente = Productos.FirstOrDefault(p => p.IdProducto == producto.IdProducto);

            if (productoExistente != null)
            {
                // Si ya existe, simplemente incrementa la cantidad
                productoExistente.Cantidad += 1;
            }
            else
            {
                // Si no existe, crea un nuevo ProductoCarrito y lo agrega
                var nuevoProductoCarrito = new ProductoCarrito
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.NombreProducto,
                    Descripcion = producto.Descripcion,
                    Cantidad = 1, // Agregar por primera vez con cantidad 1
                    Precio = producto.Precio
                };
                Productos.Add(nuevoProductoCarrito);
            }

            // Actualiza el total
            ActualizarTotal();
        }

        [RelayCommand]
        public async Task CargarProductosDelCarrito()
        {
            var productoId = await SecureStorage.GetAsync("productoId");

            // Lógica para obtener los productos del carrito de la API y llenar la ObservableCollection
            var productosEnCarrito = await _carritoService.GetListaProductoDeCarritoAsync();

            Productos.Clear(); // Limpiar la lista actual para evitar duplicados
            if(productosEnCarrito == null)
            {
                return;
            }

            foreach (var producto in productosEnCarrito)
            {
                var productocarrito = await _carritoService.GetCarritoProductoDelProductoAsync(producto.IdProducto);

                var nuevoProductoCarrito = new ProductoCarrito
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.NombreProducto,
                    Descripcion = producto.Descripcion,
                    Cantidad = productocarrito.Cantidad,
                    Precio = producto.Precio
                };

                Productos.Add(nuevoProductoCarrito); // Agregar el producto a la colección
            }

            // Actualizar el total del carrito
            ActualizarTotal();
        }


        public ICommand GoToEliminarProductoDeCarritoCommand => new Command<int>(async (idProducto) =>
        {
            if (idProducto > 0) // Verifica que el id sea válido
            {
                await EliminarProductoDelCarrito(idProducto); // Usa solo el IdProducto
            }
        });
        [RelayCommand]
        public async Task EliminarProductoDelCarrito(int id)
        {
            bool confirmacion = await _eliminarService.MostrarMensajeConfirmacion("Confirmación", "¿Deseas eliminar el usuario?");

            if (!confirmacion)
            {
                // Si el usuario cancela, no se realiza la edición
                return;
            }
            // Lógica para obtener los productos del carrito de la API y llenar la ObservableCollection
            bool exito = await _carritoService.EliminarProductoDelCarrito(id);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Se elimino el susuario exitosamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al eliminar el usuario", "OK");
            }

        }


        private void ActualizarTotal()
        {
            // Sumar los subtotales de todos los productos
            Total = Productos.Sum(p => p.Subtotal);
            OnPropertyChanged(nameof(Total));
        }
    }
}
