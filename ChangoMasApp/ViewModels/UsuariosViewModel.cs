using ChangoMasApp.Services;
using ChangoMasApp.Models;
using ChangoMasApp.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChangoMasApp.ViewModels
{
    public partial class UsuariosViewModel: BaseViewModel
    {
        public ObservableCollection<Usuarios> Usuarios { get; } = new();
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Usuarios usuarioSeleccionado;

        private readonly IUsuariosService _usuariosService;
        public int RolUsuario { get; private set; }

        public bool EsAdmin => RolUsuario == 1; // Propiedad para mostrar opciones de admin
        public bool EsCliente => RolUsuario == 2; // Opciones para cliente

        public UsuariosViewModel(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
            CargarUsuario();
            //Task.Run(async () => await GetUsuariosAsync());
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
        private async Task GetUsuariosAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    var usuarios = await _usuariosService.GetListaUsuariosAsync();

                    if (usuarios != null)
                    {
                        if (Usuarios.Count != 0)
                            Usuarios.Clear();

                        foreach (var usuario in usuarios)
                            Usuarios.Add(usuario);
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
            if (usuarioSeleccionado == null)
            {
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioDetallePage(usuarioSeleccionado), true);
        }

        [RelayCommand]
        private async Task GoToAgregarUsuario()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioAgregarPage());
        }

        [RelayCommand]
        private async Task GoToBack()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
        }
    }
}
