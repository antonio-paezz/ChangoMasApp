using ChangoMasApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.Services
{
    public interface IUsuariosService
    {
        Task<IEnumerable<Usuarios>> GetListaUsuariosAsync();
        Task<bool> AgregarUsuarioAsync(Usuarios usuario);
        Task<bool> EditarUsuarioAsync(Usuarios usuario);
        Task<bool> EliminarUsuario(int id);
        Task<Usuarios> GetUsuarioAsync(int id);
    }
}
