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
    public class Documento_Business : IRepository<Documento>
    {
        public async Task<bool> AddAsync(Documento generic)
        {
            try
            {
                DataAccess.AbrirConexion();
                List<SqlParameter> sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@LEGAJO", generic.Legajo_Empleado),
                    new SqlParameter("@DESCRIPCION", generic.Descripcion),  
                    new SqlParameter("@RUTA_ARCHIVO", generic.Ruta)
                };

                return await DataAccess.ExecStoredProcedureAsync("SP_AGREGARDOCUMENTO", sqlParameters);
            }
            catch (Exception)
            {
                return false; 
            }
            finally
            {
                DataAccess.CerrarConexion();
            }
        }


       
        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Documento> Find(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Documento>> GetAllAsync()
        {
            try
            {
                DataAccess.AbrirConexion();
                List<Documento> list = new List<Documento>();
                string SelectQuery = "SELECT ID_DOCUMENTO, ID_USUARIO, DESCRIPCION, FECHA_PUBLICACION, RUTA, ESTADO_CONFORMIDAD FROM DOCUMENTOS";
                SqlDataReader reader = await DataAccess.GetReaderAsync(SelectQuery);

                while (reader.Read())
                {
                    list.Add(new Documento
                    {
                        ID_Documento = (int)reader.GetInt64(0),
                        ID_Usuario = (int)reader.GetInt64(1),
                        Descripcion = reader.GetString(2),
                        Fecha_Publicacion = reader.GetDateTime(3),
                        Ruta = reader.GetString(4),
                        Conformidad = reader.GetBoolean(5)
                    });
                }

                return list;
            }
            catch (Exception)
            {
                return null; 
            }
            finally
            {
                DataAccess.CerrarConexion();
            }
        }

        public Task<bool> UpdateAsync(Documento generic, string query)
        {
            throw new NotImplementedException();
        }

        public async static Task<bool> UpdateConformidadAsync(int id)
        {
            try
            {
                DataAccess.AbrirConexion();
                string sql = $"UPDATE DOCUMENTOS SET ESTADO_CONFORMIDAD = 1 WHERE ID_DOCUMENTO = {id}";
                return await DataAccess.ExecCommandAsync(sql);
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
    }
}
