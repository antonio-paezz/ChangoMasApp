using ChangoMasApp.Services;
using ChangoMasApp.Views;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;


namespace ChangoMasApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel 
    {
        private readonly ILoginService _authService;

        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; }

        public LoginViewModel(ILoginService loginService)
        {
            _authService = loginService;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        public async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe de ingresar usuario y contraseña", "OK");
                return;
            }
            var token = await _authService.GetTokenAsync(Email, Password);
            if (!string.IsNullOrEmpty(token))
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Se ha iniciado sesión correctamente", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error al ingresar Usuario o Contraseña, Intente nuevamente", "OK");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        private async Task GoRegistro()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegistroPage());
        }
    }
}