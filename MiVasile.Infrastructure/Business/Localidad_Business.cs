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
    public class Localidad_Business : IRepository<Localidad>
    {
        public Task<bool> AddAsync(Localidad generic)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Localidad> Find(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Localidad>> GetAllAsync()
        {
            try
            {
                List<Localidad> localidades = new List<Localidad>();
                string query = "SELECT ID_LOCALIDAD, NOMBRE FROM LOCALIDADES";
                SqlDataReader reader = await DataAccess.GetReaderAsync(query);

                while (reader.Read())
                {
                    localidades.Add(new Localidad
                    {
                       ID_Localidad = (int)reader.GetInt64(0),
                       Nombre = reader.GetString(1),
                    });
                }

                return localidades;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> UpdateAsync(Localidad generic, string query)
        {
            throw new NotImplementedException();
        }
    }
}
