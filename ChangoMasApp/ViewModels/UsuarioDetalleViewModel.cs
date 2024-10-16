using ChangoMasApp.Models;
using ChangoMasApp.Views;
using ChangoMasApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace ChangoMasApp.ViewModels
{
    public partial class UsuarioDetalleViewModel: BaseViewModel
    {
        [ObservableProperty]
        Usuarios usuario;

        private readonly IEliminarService _eliminarService;
        private readonly IUsuariosService _usuariosService;

        public int RolUsuario { get; private set; }

        public bool EsAdmin => RolUsuario == 1; // Propiedad para mostrar opciones de admin
        public bool EsCliente => RolUsuario == 2; // Opciones para cliente

        public UsuarioDetalleViewModel(IEliminarService eliminarService, IUsuariosService usuariosService)
        {
            Title = "Detalle de Usuarios";
            _eliminarService = eliminarService;
            _usuariosService = usuariosService;
            CargarUsuario();
        }

        private async void CargarUsuario()
        {
            var userId = await SecureStorage.GetAsync("userId");

            var usuario = await _usuariosService.GetUsuarioAsync(int.Parse(userId));
            RolUsuario = usuario.IdRol;
            OnPropertyChanged(nameof(EsAdmin));
            OnPropertyChanged(nameof(EsCliente));
        }

        [RelayCommand]
        private async Task GoToEditar() 
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioEditarPage(usuario), true);
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

            bool exito = await _usuariosService.EliminarUsuario(usuario.IdUsuario);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Se elimino el usuario exitosamente", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new UsuariosPage(), true);
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
