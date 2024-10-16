using ChangoMasApp.Models;
using ChangoMasApp.ViewModels;
using ChangoMasApp.Services;

namespace ChangoMasApp.Views;

public partial class CarritoPage : ContentPage
{
    public CarritoPage()
    {
        InitializeComponent();
        ProductosService service = new ProductosService();
        CarritoService serviceC = new CarritoService();
        EliminarService eliminarService = new EliminarService();

        CarritoViewModel vm = new CarritoViewModel(service, serviceC, eliminarService);
        this.BindingContext = vm;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as CarritoViewModel;

        if (vm != null)
        {
            await vm.CargarProductosDelCarritoCommand.ExecuteAsync(null);
        }
    }
}