using ChangoMasApp.Services;
using ChangoMasApp.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ChangoMasApp.Views;

namespace ChangoMasApp.ViewModels
{
    public partial class UsuarioAgregarViewModel : BaseViewModel
    {
        private readonly IUsuariosService _usuariosService;

        public ObservableCollection<string> Roles { get; set; }
        public string RolSeleccionado { get; set; }

        public UsuarioAgregarViewModel()
        {
            _usuariosService = new UsuariosService();
            Roles = new ObservableCollection<string> { "Administrador", "Usuario" };

        }

        [ObservableProperty]
        private string nombreCompleto;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string contraseña;
        [ObservableProperty]
        private string provincia;
        [ObservableProperty]
        private string ciudad;
        [ObservableProperty]
        private string calle;
        [ObservableProperty]
        private string codigoPostal;
        [ObservableProperty]
        private string numeroCalle;
        [ObservableProperty]
        private string departamento;
        [ObservableProperty]
        private int idRol;



        [RelayCommand]
        public async Task AgregarUsuario()
        {
            switch (RolSeleccionado) 
            {
                case "Administrador":
                    idRol = 1; break;
                case "Usuario":
                    idRol = 2; break;
            }
            Usuarios usuario = new Usuarios
            {
                NombreCompleto = NombreCompleto,
                Email = Email,
                Contraseña = Contraseña,
                Provincia = Provincia,
                Ciudad = Ciudad,
                Calle = Calle,
                CodigoPostal = CodigoPostal,
                NumeroCalle = NumeroCalle,
                Departamento = Departamento,
                IdRol = IdRol
            };
            bool exito = await _usuariosService.AgregarUsuarioAsync(usuario);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Usauario agregado exitosamente", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new UsuariosPage(), true);

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al registrar el usuario", "OK");
            }

        }


    }
}
