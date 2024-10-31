using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Domain.Models
{
    public class Sesion
    {
        public int ID_Usuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; }
        public string Leg { get; set; }
        public int dni { get; set; }



        public Claim[] ToClaims()
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, ID_Usuario.ToString()),
                new Claim(ClaimTypes.Name, Nombre),
                new Claim(ClaimTypes.Email, Correo),
                new Claim(ClaimTypes.Role, Rol),
                new Claim("leg", Leg),
                new Claim("dni", dni.ToString())


            };
        }

    }


}
