using ChangoMasApp.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using ChangoMasApp.Utils;
using System.Text;
using System.Net.Http.Json;
using ChangoMasApp.ViewModels;
using Microsoft.Maui.Graphics.Text;

namespace ChangoMasApp.Services
{
    public partial class CarritoService: ICarritoService
    {
        HttpClient client;

        private readonly IProductosService _productosService;

        public CarritoService(IProductosService productosService)
        {
            _productosService = productosService;
        }


        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public CarritoService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Constants.ApiDataServer)
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Carrito> GetCarritoDelUsuarioAsync()
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var userId = await SecureStorage.GetAsync("userId");

            var response = await client.GetAsync(Constants.CarritosEndPoint);
            if (response.IsSuccessStatusCode)
            {
                // Obtener todos los carritos desde la respuesta
                var carritos = await response.Content.ReadFromJsonAsync<IEnumerable<Carrito>>(options);

                // Filtrar el carrito que corresponde al userId
                var carritoDelUsuario = carritos?.FirstOrDefault(c => c.IdUsuario == int.Parse(userId));

                return carritoDelUsuario;
            }

            return default;
        }
        public async Task<bool> CrearCarritoProducto(int idProducto) 
        {
            var token = await SecureStorage.GetAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Guarda el IdProducto en SecureStorage
            await SecureStorage.SetAsync("productoId", idProducto.ToString());
            var userId = await SecureStorage.GetAsync("userId");

            CarritoProductoRequest request = new CarritoProductoRequest
            {
                idUsuario = int.Parse(userId),
                idProducto = idProducto,
                cantidad = 1
            };
            var jsonContent2 = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8, "application/json");

            var response2 = await client.PostAsync(Constants.AgregarProductoACarritoEndPoint, jsonContent2);

            return true;
        }

        public async Task<List<Productos>> GetListaProductoDeCarritoAsync()
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync(Constants.ProductosDeCarritoEndPoint);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Productos>>(options);
            }
            return default;
            
        }

        public async Task<CarritoProducto> GetCarritoProductoDelProductoAsync(int id)
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"{Constants.ApiDataServer}{Constants.CarritoProductoDeProductoEndPoint}?id={id}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Leemos el contenido de la respuesta y lo convertimos en un objeto de tipo Productos
                return await response.Content.ReadFromJsonAsync<CarritoProducto>(options);
            }

            return default;
        }

        public async Task<bool> EliminarProductoDelCarrito(int id)
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"{Constants.ApiDataServer}{Constants.EliminarProductoDeCarritoEndPoint}?id={id}";

            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Leemos el contenido de la respuesta y lo convertimos en un objeto de tipo Productos
                return true;
            }

            return default;
        }

    }
    
}
