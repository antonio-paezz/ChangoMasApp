using ChangoMasApp.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using ChangoMasApp.Utils;
using System.Text;

namespace ChangoMasApp.Services
{
    public partial class RegistroService: IRegistroService
    {
        HttpClient client;

        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public RegistroService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Constants.ApiDataServer)
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> SetRegistrationAsync(Usuarios usuario) 
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
    }
}
