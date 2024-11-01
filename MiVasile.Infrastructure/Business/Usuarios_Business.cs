using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiVasile.Domain.Interfaces;
using MiVasile.Domain.Models;

namespace MiVasile.Infrastructure.Business
{
    public class Usuarios_Business : IRepository<Usuario>
    {
        public Task<bool> AddAsync(Usuario generic)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> Find(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            try
            {
                DataAccess.AbrirConexion();
                List<Usuario> usuarios = new List<Usuario>();
                string query = "SELECT ID_USUARIO, CORREO, CLAVE, BAJA FROM USUARIOS";
                SqlDataReader reader = await DataAccess.GetReaderAsync(query);

                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        ID = (int)reader.GetInt64(0),
                        Correo = reader.GetString(1),
                        Clave = reader.GetString(2),
                        Baja = reader.GetBoolean(3)
                    });
                }

                return usuarios;
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

        public Task<bool> UpdateAsync(Usuario generic, string query)
        {
            throw new NotImplementedException();
        }
    }
}
