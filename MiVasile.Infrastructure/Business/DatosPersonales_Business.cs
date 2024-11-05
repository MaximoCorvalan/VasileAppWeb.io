using MiVasile.Domain.Interfaces;
using MiVasile.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Infrastructure.Business
{
    public class DatosPersonales_Business : IRepository<DatosPersonalesXUsuario>
    {
        public Task<bool> AddAsync(DatosPersonalesXUsuario generic)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<DatosPersonalesXUsuario> Find(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DatosPersonalesXUsuario>> GetAllAsync()
        {
            try
            {
                List<DatosPersonalesXUsuario> datos = new List<DatosPersonalesXUsuario>();
                DataAccess.AbrirConexion();
                string query = "SELECT LEGAJO, ID_USUARIO, NOMBRE_COMPLETO, DOMICILIO_ACTUAL, ID_LOCALIDAD, GENERO, FECHA_NACIMIENTO, DNI FROM DATOS_PERSONAS_X_USUARIO";
                SqlDataReader reader = await DataAccess.GetReaderAsync(query);

                while (reader.Read())
                {
                    datos.Add(new DatosPersonalesXUsuario
                    {
                       Legajo = (int)reader.GetInt64(0),
                       ID_Usuario = (int)reader.GetInt64(1),
                       NombreCompleto = reader.GetString(2),
                       Domicilio = !reader.IsDBNull(3) ? reader.GetString(3) : null,
                       Localidad = !reader.IsDBNull(4) ? this.GetLocalidadXID((int)reader.GetInt64(4)).Result : null,
                       Genero = !reader.IsDBNull(5) ? reader.GetString(5) : null,
                       Fecha_Nacimiento = !reader.IsDBNull(6) ? reader.GetDateTime(6) : new DateTime(2999, 12, 1),
                       DNI = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
                    });
                }

                return datos;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                DataAccess.CerrarConexion();
            }
        }

        public Task<bool> UpdateAsync(DatosPersonalesXUsuario generic, string query)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetLocalidadXID(int id)
        {
            try
            {
                Localidad_Business localidad_Business = new Localidad_Business();
                List<Localidad> localidades = await localidad_Business.GetAllAsync();
                
                if (localidades.Any())
                {
                    Localidad localidad = localidades.FirstOrDefault(x => x.ID_Localidad == id);

                    if (localidad != null)
                        return localidad.Nombre;

                }

                return "Sin Localidad";
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Sin Localidad";
            }
        }
    }
}
