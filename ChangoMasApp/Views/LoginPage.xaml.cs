using ChangoMasApp.Services;
using ChangoMasApp.ViewModels;

namespace ChangoMasApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        LoginService serviceU = new LoginService();

        LoginViewModel vm = new LoginViewModel (serviceU);
        this.BindingContext = vm;
    }
}