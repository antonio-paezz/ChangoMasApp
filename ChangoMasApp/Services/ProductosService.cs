using ChangoMasApp.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using ChangoMasApp.Utils;
using System.Text;
using System.Net.Http.Json;
using System.Net.Http;

namespace ChangoMasApp.Services
{
    public partial class ProductosService: IProductosService
    {
        HttpClient client;

        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductosService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Constants.ApiDataServer)
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Productos>> GetListaProductosAsync() 
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync(Constants.ProductsEndpoint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Productos>>(options);
            }

            return default;
        }

        public async Task<Productos> GetProductoAsync(int id)
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"{Constants.ApiDataServer}{Constants.VerProductoPorIdEndPoint}?id={id}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Leemos el contenido de la respuesta y lo convertimos en un objeto de tipo Productos
                return await response.Content.ReadFromJsonAsync<Productos>(options);
            }


            return default;
        }

        public async Task<bool> AgregarProductoAsync(Productos producto)
        {

            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(producto),
                    Encoding.UTF8, "application/json");


                var token = await SecureStorage.GetAsync("authToken");

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await client.PostAsync(Constants.AgregarProductoEndPoint, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return true; // El registro fue exitoso
                }
                else
                {
                    // Podrías loguear la respuesta aquí para más detalles
                    return false; // El registro falló
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías loguear el error o notificarlo al usuario
                Console.WriteLine($"Error al agregar producto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditarProductoAsync(Productos producto)
        {
            var editData = new
            {
                idProducto = producto.IdProducto,
                nombreProducto = producto.NombreProducto,
                descripcion = producto.Descripcion,
                precio = producto.Precio,
                stock = producto.Stock
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(editData),
                Encoding.UTF8, "application/json");

            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PutAsync(Constants.EditarProductoEndPoint, jsonContent);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"{Constants.ApiDataServer}{Constants.EliminarProductoEndPoint}?id={id}";

            var response = await client.DeleteAsync(url);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
