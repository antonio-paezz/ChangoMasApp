using ChangoMasApp.Services;
using ChangoMasApp.Models;
using ChangoMasApp.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using static CommunityToolkit.Mvvm.ComponentModel.__Internals.__TaskExtensions.TaskAwaitableWithoutEndValidation;
using System.Windows.Input;


namespace ChangoMasApp.ViewModels
{
    public partial class ProductosViewModel: BaseViewModel
    {
        public ObservableCollection<Productos> Productos { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Productos productoSeleccionado;

        private readonly IProductosService _productosService;
        private readonly ICarritoService _carritoService;
        private readonly IUsuariosService _usuarioService;
        // private readonly CarritoViewModel _carritoViewModel;

        public int RolUsuario { get; private set; }

        public bool EsAdmin => RolUsuario == 1; // Propiedad para mostrar opciones de admin
        public bool EsCliente => RolUsuario == 2; // Opciones para cliente

        public ProductosViewModel(IProductosService productosService, ICarritoService carritoService, IUsuariosService usuarioService)
        {
            _productosService = productosService;
            _carritoService = carritoService;
            _usuarioService = usuarioService;
            CargarUsuario();
            //_carritoViewModel = carritoViewModel;
        }

        private async void CargarUsuario()
        {
            var userId = await SecureStorage.GetAsync("userId");

            var usuario = await _usuarioService.GetUsuarioAsync(int.Parse(userId));
            RolUsuario = usuario.IdRol;
            OnPropertyChanged(nameof(EsAdmin));
            OnPropertyChanged(nameof(EsCliente));
        }

        [RelayCommand]
        private async Task GetProductosAsync() 
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    var productos = await _productosService.GetListaProductosAsync();

                    if (productos != null)
                    {
                        if (Productos.Count != 0)
                            Productos.Clear();

                        foreach (var producto in productos)
                            Productos.Add(producto);
                    }

                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                }
                finally
                {
                    IsBusy = false;
                }

            }
        }
        [RelayCommand]
        private async Task GoToDetail()
        {
            if (productoSeleccionado == null)
            {
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(productoSeleccionado), true);
        }

        [RelayCommand]
        private async Task GoToAgregar() 
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoAgregarPage());

        }

        [RelayCommand]
        private async Task GoToBack()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
        }

        // Definición del comando que llama al método del servicio
        public ICommand GoToAgregarCarritoCommand => new Command<int>(async (idProducto) =>
        {
            if (idProducto > 0) // Verifica que el id sea válido
            {
                await AgregarProductoAlCarritoAsync(idProducto); // Usa solo el IdProducto
            }
        });

        [RelayCommand]
        private async Task AgregarProductoAlCarritoAsync(int id)
        {
            bool productoCarrito = await _carritoService.CrearCarritoProducto(id);
            //_carritoViewModel.AgregarProductoAlCarrito(id);
            await App.Current.MainPage.DisplayAlert("Éxito", "Se agrego el producto exitosamente", "OK");

        }

    }
}
