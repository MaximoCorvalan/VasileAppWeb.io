using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Domain.Models
{
    public class Documento
    {
        public int ID_Documento { get; set; }
        public int? ID_Usuario { get; set; }
        public int? Legajo_Empleado { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha_Publicacion { get; set; }
        public string Ruta { get; set; }//aca le saque el ??
        public bool Conformidad { get; set; }

        public IFormFile Archivo { get; set; }
    }
}
