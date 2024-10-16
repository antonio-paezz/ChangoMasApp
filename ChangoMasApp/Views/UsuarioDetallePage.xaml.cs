using ChangoMasApp.Models;
using ChangoMasApp.ViewModels;
using ChangoMasApp.Services;

namespace ChangoMasApp.Views;

public partial class UsuarioDetallePage : ContentPage
{
	public UsuarioDetallePage(Usuarios param)
	{
		InitializeComponent();
        EliminarService service = new EliminarService();
        UsuariosService serviceU = new UsuariosService();
        UsuarioDetalleViewModel vm = new UsuarioDetalleViewModel(service, serviceU);
        this.BindingContext = vm;
        vm.Usuario = param;
    }
}