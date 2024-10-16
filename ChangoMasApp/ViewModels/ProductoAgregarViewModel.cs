using ChangoMasApp.Services;
using ChangoMasApp.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ChangoMasApp.Views;
using System.Net.Http.Json;
using ChangoMasApp.Utils;
using System.Net.Http.Headers;

namespace ChangoMasApp.ViewModels
{
    public partial class ProductoAgregarViewModel : BaseViewModel
    {
        private readonly IProductosService _productoService;
        private FileResult _imagenSeleccionada;

        public ProductoAgregarViewModel()
        {
            _productoService = new ProductosService();
        }

        [ObservableProperty]
        private int idProducto;
        [ObservableProperty]
        private string nombreProducto;
        [ObservableProperty]
        private string descripcion;
        [ObservableProperty]
        private int precio;
        [ObservableProperty]
        private int stock;
        [ObservableProperty]
        private string imagenUrl;

        [ObservableProperty]
        private ImageSource imagenPreview;

        [RelayCommand]
        public async Task SeleccionarImagen()
        {
            _imagenSeleccionada = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Seleccione una Imagen"
            });

            if (_imagenSeleccionada != null)
            {
                var stream = await _imagenSeleccionada.OpenReadAsync();
                ImagenPreview = ImageSource.FromStream(() => stream); // Actualiza la propiedad enlazada
            }
        }

        public async Task<string> SubirImagenAsync()
        {
            try
            {
                if (_imagenSeleccionada == null) return null;

                using var stream = await _imagenSeleccionada.OpenReadAsync();
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(stream), "archivo", _imagenSeleccionada.FileName);

                var token = await SecureStorage.GetAsync("authToken");

                var client = new HttpClient();
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await client.PostAsync("https://localhost:7165/api/Productos/subirImagen", content);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<UploadResponse>();
                    return resultado?.url;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo subir la imagen", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Excepción", ex.Message, "OK");
                return null;
            }
        }

        [RelayCommand]
        public async Task AgregarAsync()
        {
            var urlImagen = await SubirImagenAsync();

            Productos producto = new Productos
            {
                NombreProducto = NombreProducto,
                Descripcion = Descripcion,
                Precio = Precio,
                Stock = Stock,
                ImagenUrl = urlImagen
            };
            bool exito = await _productoService.AgregarProductoAsync(producto);

            if (exito)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Se agregó el producto", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new ProductosPage(), true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al agregar el producto", "OK");
            }
        }
    }
}
