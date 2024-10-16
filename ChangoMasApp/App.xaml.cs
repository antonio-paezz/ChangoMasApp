using ChangoMasApp.Services;
using ChangoMasApp.ViewModels;
using ChangoMasApp.Views;

namespace ChangoMasApp
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public App()
        {
            InitializeComponent();
            var loginService = new LoginService();
            MainPage = new NavigationPage(new LoginPage
            {
                BindingContext = new LoginViewModel(loginService)
            });

        }
    }
}
