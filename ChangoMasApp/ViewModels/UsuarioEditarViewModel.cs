using ChangoMasApp.Models;
using ChangoMasApp.Services;
using ChangoMasApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChangoMasApp.ViewModels
{
    public partial class UsuarioEditarViewModel : BaseViewModel
    {
        [ObservableProperty]
        Usuarios usuario;

        public ObservableCollection<string> Roles { get; set; }
        public string RolSeleccionado { get; set; }

        private readonly IUsuariosService _usuariosService;

        public UsuarioEditarViewModel(IUsuariosService usuariosService, Usuarios usuario)
        {
            _usuariosService = usuariosService;
            Usuario = usuario;
            Roles = new ObservableCollection<string> { "Administrador", "Usuario" };
        }

        [RelayCommand]
        public async Task EditarAsync()
        {
            if (usuario == null || usuario.IdUsuario == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Usuario no cargado correctamente", "OK");
                return;
            }


            if (RolSeleccionado == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe seleccionar Rol", "OK");

            }
            else 
            {
                switch (RolSeleccionado)
                {
                    case "Administrador":
                        usuario.IdRol = 1;
                        break;
                    case "Usuario":
                        usuario.IdRol = 2;
                        break;
                    default:
                        break;
                }
            }

            bool exito = await _usuariosService.EditarUsuarioAsync(usuario);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Se edito el usuario exitosamente", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new UsuarioDetallePage(usuario), true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al editar el usuario", "OK");
            }
        }
    }
}
