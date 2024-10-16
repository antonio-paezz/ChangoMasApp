using CommunityToolkit.Mvvm.Input;
using ChangoMasApp.Services;
using ChangoMasApp.Views;

namespace ChangoMasApp.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly IUsuariosService _usuarioService;
        public int RolUsuario { get; private set; }

        public bool EsAdmin => RolUsuario == 1; // Propiedad para mostrar opciones de admin
        public bool EsCliente => RolUsuario == 2; // Opciones para cliente

        public MainViewModel(IUsuariosService usuarioService)
        {
            Title = "Menu Principal";
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
        public async Task GoToProductoLista()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductosPage());
        }

        [RelayCommand]
        public async Task GoToUsuarioLista()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuariosPage());
        }

        [RelayCommand]
        public async Task GoToCarrito()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CarritoPage());
        }

        [RelayCommand]
        public async Task GoToAcerca()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AcercaDePage());
        }

        [RelayCommand]
        public async Task Exit()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Salir", "¿Desea terminar la sesión y salir?", "Aceptar", "Cancelar");

            if (result)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

    }
}
