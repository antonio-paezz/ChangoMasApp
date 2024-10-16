using ChangoMasApp.Models;
using ChangoMasApp.ViewModels;
using ChangoMasApp.Services;

namespace ChangoMasApp.Views;

public partial class UsuarioEditarPage : ContentPage
{
	public UsuarioEditarPage(Usuarios param)
	{
		InitializeComponent();
        UsuariosService service = new UsuariosService();
        UsuarioEditarViewModel vm = new UsuarioEditarViewModel(service, param);
        this.BindingContext = vm;
        vm.Usuario = param;
    }
}