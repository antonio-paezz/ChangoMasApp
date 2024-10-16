using ChangoMasApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.Services
{
    public interface ILoginService
    {
        Task<string> GetTokenAsync(string email, string contraseña);
    }
}
