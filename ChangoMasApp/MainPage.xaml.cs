using ChangoMasApp.Services;
using ChangoMasApp.ViewModels;

namespace ChangoMasApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UsuariosService usService = new UsuariosService();
            MainViewModel viewModel = new MainViewModel(usService);
            this.BindingContext = viewModel;
        }
    }
}
