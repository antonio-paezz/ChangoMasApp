using ChangoMasApp.Services;
using ChangoMasApp.ViewModels;

namespace ChangoMasApp.Views;

public partial class ProductosPage : ContentPage
{
	public ProductosPage()
	{
        InitializeComponent();
        ProductosService service = new ProductosService();
        CarritoService serviceC = new CarritoService();
        UsuariosService serviceU = new UsuariosService();

        ProductosViewModel vm = new ProductosViewModel(service, serviceC, serviceU);
        this.BindingContext = vm;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as ProductosViewModel;

        if (vm != null)
        {
            await vm.GetProductosCommand.ExecuteAsync(null);
        }
    }
}