using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Domain.Models
{
    public class DatosPersonalesXUsuario
    {
        public int Legajo { get; set; }
        public int ID_Usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public string Genero { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public int DNI { get; set; }
    }
}
