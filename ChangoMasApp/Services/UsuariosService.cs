using System.Net.Http.Headers;
using ChangoMasApp.Utils;
using System.Text.Json;
using System.Net.Http.Json;
using ChangoMasApp.Models;
using System.Text;
using Microsoft.Maui.ApplicationModel.Communication;

namespace ChangoMasApp.Services
{
    public partial class UsuariosService: IUsuariosService
    {
        HttpClient client;

        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public UsuariosService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Constants.ApiDataServer)
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IEnumerable<Usuarios>> GetListaUsuariosAsync()
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync(Constants.UsuariosEndPoint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Usuarios>>(options);
            }

            return default;
        }

        public async Task<Usuarios> GetUsuarioAsync(int id)
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"{Constants.ApiDataServer}{Constants.VerUsuarioPorIdEndPoint}?id={id}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Leemos el contenido de la respuesta y lo convertimos en un objeto de tipo Productos
                return await response.Content.ReadFromJsonAsync<Usuarios>(options);
            }


            return default;
        }

        public async Task<bool> AgregarUsuarioAsync(Usuarios usuario)
        {

            try
            {
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(usuario),
                    Encoding.UTF8, "application/json");


                var token = await SecureStorage.GetAsync("authToken");

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await client.PostAsync(Constants.RegistroEndPoint, jsonContent);

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
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditarUsuarioAsync(Usuarios usuario) 
        {
            var editData = new
            {
                idUsuario = usuario.IdUsuario,
                nombreCompleto = usuario.NombreCompleto,
                email = usuario.Email,
                contraseña = usuario.Contraseña,
                provincia = usuario.Provincia,
                ciudad = usuario.Ciudad,
                calle = usuario.Calle,
                codigoPostal = usuario.CodigoPostal,
                numeroCalle = usuario.NumeroCalle,
                departamento = usuario.Departamento,
                idRol = usuario.IdRol
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(editData),
                Encoding.UTF8, "application/json");

            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.PutAsync(Constants.EditarUsuarioEndPoint, jsonContent);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var token = await SecureStorage.GetAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"{Constants.ApiDataServer}{Constants.EliminarUsuarioEndPoint}?id={id}";

            var response = await client.DeleteAsync(url);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
