using ChangoMasApp.Services;
using ChangoMasApp.ViewModels;

namespace ChangoMasApp.Views;

public partial class UsuariosPage : ContentPage
{
	public UsuariosPage()
	{
        UsuariosService service = new UsuariosService();
        UsuariosViewModel vm = new UsuariosViewModel(service);
		InitializeComponent();
        this.BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as UsuariosViewModel;

        if (vm != null)
        {
            await vm.GetUsuariosCommand.ExecuteAsync(null);
        }
    }
}