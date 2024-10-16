using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangoMasApp.Models;

namespace ChangoMasApp.Services
{
    public interface IRegistroService
    {
        Task<bool>SetRegistrationAsync(Usuarios usuario);
    }
}
