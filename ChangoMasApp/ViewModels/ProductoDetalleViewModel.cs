using ChangoMasApp.Models;
using ChangoMasApp.Views;
using ChangoMasApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChangoMasApp.ViewModels
{
    public partial class ProductoDetalleViewModel : BaseViewModel 
    {
        [ObservableProperty]
        Productos producto;

        private readonly IEliminarService _eliminarService;
        private readonly IProductosService _productosService;
        private readonly IUsuariosService _usuarioService;

        public int RolUsuario { get; private set; }

        public bool EsAdmin => RolUsuario == 1; // Propiedad para mostrar opciones de admin
        public bool EsCliente => RolUsuario == 2; // Opciones para cliente

        public ProductoDetalleViewModel(IEliminarService eliminarService, IProductosService productosService, IUsuariosService usuarioService)
        {
            Title = "Detalle de Producto";
            _eliminarService = eliminarService;
            _productosService = productosService;
            _usuarioService = usuarioService;
            CargarUsuario();
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
        private async Task GoToEditar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductoEditarPage(producto), true);
        }

        [RelayCommand]
        private async Task GoToEliminar()
        {
            bool confirmacion = await _eliminarService.MostrarMensajeConfirmacion("Confirmación", "¿Deseas eliminar el usuario?");

            if (!confirmacion)
            {
                // Si el usuario cancela, no se realiza la edición
                return;
            }

            bool exito = await _productosService.EliminarProducto(producto.IdProducto);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Se elimino el susuario exitosamente", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new ProductosPage(), true);

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al eliminar el usuario", "OK");
            }

        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
