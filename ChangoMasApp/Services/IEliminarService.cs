using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.Services
{
    public interface IEliminarService
    {
        Task<bool> MostrarMensajeConfirmacion(string title, string message);
        Task MostrarAlerta(string title, string message);
    }
}
