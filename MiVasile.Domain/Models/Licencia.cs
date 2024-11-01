using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Domain.Models
{

     public  class Licencia 
    {
        public int _idLicencia { get; set; }
        public int _legajo {  get; set; }
        public string _descripcion { get; set; }

        public string _tipoLicencia { get; set; }

        public DateTime _fechaIngreso { get; set; }
        public DateTime _fechaRegreso { get; set; }

        public Licencia() { }

        public Licencia(int idLicencia,string tipoLicencia, int legajo, string descripcion, DateTime fechaIngreso, DateTime fechaRegreso)
        {
            _idLicencia = idLicencia;
            _legajo = legajo;
            _descripcion = descripcion;
            _fechaIngreso = fechaIngreso;
            _fechaRegreso = fechaRegreso;
            _tipoLicencia = tipoLicencia;
        }

    }
}
