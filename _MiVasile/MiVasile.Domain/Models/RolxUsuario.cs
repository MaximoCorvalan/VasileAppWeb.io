using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Domain.Models
{
    public class RolxUsuario
    {
        public int ID_Rol { get; set; }
        public int ID_Usuario { get; set; }
        public RolesDeUsuario Rol { get; set; }
    }
}

public enum RolesDeUsuario
{
    Admin,
    Usuario
}
