using ChangoMasApp.Models;
using ChangoMasApp.ViewModels;
using ChangoMasApp.Services;

namespace ChangoMasApp.Views;

public partial class ProductoDetallePage : ContentPage
{
	public ProductoDetallePage(Productos param)
	{
		InitializeComponent();
        EliminarService service = new EliminarService();
        ProductosService serviceU = new ProductosService();
        UsuariosService serviceUsu = new UsuariosService();
        ProductoDetalleViewModel vm = new ProductoDetalleViewModel(service, serviceU, serviceUsu);
        this.BindingContext = vm;
        vm.Producto = param;
    }
}