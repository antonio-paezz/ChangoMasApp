using ChangoMasApp.Services;
using ChangoMasApp.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChangoMasApp.ViewModels
{
    public partial class RegistroViewModel: BaseViewModel
    {
        private readonly IRegistroService _registroService;

        public RegistroViewModel() 
        {
            _registroService = new RegistroService();
        }

        [ObservableProperty]
        private int idUsuario;
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

        

        [RelayCommand]
        public async Task RegistroAsync() 
        {
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
                IdRol = 2
            };
            bool exito = await _registroService.SetRegistrationAsync(usuario);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Registro completado exitosamente", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al registrar el usuario", "OK");
            }

        }


    }
}
