using ChangoMasApp.Models;
using ChangoMasApp.ViewModels;
using ChangoMasApp.Services;

namespace ChangoMasApp.Views;

public partial class ProductoEditarPage : ContentPage
{
	public ProductoEditarPage(Productos param)
	{
		InitializeComponent();
        ProductosService service = new ProductosService();
        ProductoEditarViewModel vm = new ProductoEditarViewModel(service, param);
        this.BindingContext = vm;
        vm.Producto = param;
    }
}