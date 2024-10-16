using ChangoMasApp.Models;
using ChangoMasApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChangoMasApp.Views;
using System.ComponentModel;

namespace ChangoMasApp.ViewModels
{
    public partial class ProductoEditarViewModel : BaseViewModel
    {
        [ObservableProperty]
        Productos producto;

        private readonly IProductosService _productosService;

        public ProductoEditarViewModel(IProductosService productosService, Productos producto)
        {
            _productosService = productosService;
            Producto = producto;
        }

        [RelayCommand]
        public async Task EditarAsync()
        {
            if (producto == null || producto.IdProducto == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Usuario no cargado correctamente", "OK");
                return;
            }

            bool exito = await _productosService.EditarProductoAsync(producto);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Se edito el producto exitosamente", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(producto), true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al editar el producto", "OK");
            }
        }
    }
}
