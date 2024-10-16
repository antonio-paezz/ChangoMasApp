using ChangoMasApp.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using ChangoMasApp.Utils;
using System.Text;


namespace ChangoMasApp.Services
{
    public class LoginService: ILoginService
    {
        HttpClient client;

        private static JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public LoginService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Constants.ApiDataServer)
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<string> GetTokenAsync(string email, string contraseña)
        {
            var loginData = new
            {
                email = email,
                contraseña = contraseña
            };
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8, "application/json");

            var response = await client.PostAsync(Constants.LoginEndPoint, jsonContent);


            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<Login>(result, options);
                if (loginResponse?.token != null && loginResponse.login != null)
                {
                    // Guarda el token en SecureStorage
                    await SecureStorage.SetAsync("authToken", loginResponse.token);
                    // Guarda el IdUsuario en SecureStorage
                    await SecureStorage.SetAsync("userId", loginResponse.login.IdUsuario.ToString());

                    return loginResponse.token;
                }
            }

            return null; // O lanza una excepción según tu lógica
        }
    }
}
