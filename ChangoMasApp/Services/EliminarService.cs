using System.Threading.Tasks;


namespace ChangoMasApp.Services
{
    public partial class EliminarService: IEliminarService
    {
        public async Task<bool> MostrarMensajeConfirmacion(string title, string message) 
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "Aceptar", "Cancelar");
        }
        public async Task MostrarAlerta(string title, string message) 
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}
