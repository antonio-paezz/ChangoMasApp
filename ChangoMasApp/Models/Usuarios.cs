using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangoMasApp.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }

        public string NombreCompleto { get; set; }

        public string Email { get; set; }

        public string Contraseña { get; set; }

        public string Provincia { get; set; }

        public string Ciudad { get; set; }

        public string Calle { get; set; }

        public string CodigoPostal { get; set; }

        public string NumeroCalle { get; set; }

        public string Departamento { get; set; }
        public int IdRol { get; set; }
    }
}
