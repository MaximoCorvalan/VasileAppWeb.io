using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Domain.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato de correo no es valido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(dataType:DataType.Password)]
        public string Clave { get; set; }

        public string legajo { get; set; }

        public string Nombre { get; set; }

        public bool? Baja { get; set; }
    }
}
