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
    public class RolesXUsuario_Business : IRepository<RolxUsuario>
    {
        public Task<bool> AddAsync(RolxUsuario generic)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<RolxUsuario> Find(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RolxUsuario>> GetAllAsync()
        {
            try
            {
                DataAccess.AbrirConexion();
                List<RolxUsuario> rolesXUsuario = new List<RolxUsuario>();
                string query = "SELECT ID_ROL,ID_USUARIO, ROL FROM ROL_X_USUARIO";
                SqlDataReader reader = await DataAccess.GetReaderAsync(query);

                while (reader.Read())
                {
                    rolesXUsuario.Add(new RolxUsuario
                    {
                       ID_Rol = (int)reader.GetInt64(0),
                       ID_Usuario = (int)reader.GetInt64(1),
                       Rol = reader.GetString(2) == RolesDeUsuario.Admin.ToString() ? RolesDeUsuario.Admin : RolesDeUsuario.Usuario
                    });
                }

                return rolesXUsuario;
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

        public Task<bool> UpdateAsync(RolxUsuario generic, string query)
        {
            throw new NotImplementedException();
        }
    }
}
