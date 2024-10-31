using MiVasile.Domain.Interfaces;
using MiVasile.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiVasile.Infrastructure.Business
{

    public  class LicenciasBusiness : IRepository<Licencia>
    {
       
      public  LicenciasBusiness() { 
        
        
        }


        public async Task<bool> VerificarVigencia()
        {
            bool estado = false;
            DataAccess.AbrirConexion();

          estado=  await DataAccess.ExecStoredProcedureAsync("verificarVigenciaLicencias", null);
            DataAccess.CerrarConexion();
            
            return estado;
        }
        public async Task<bool>  AddAsync(Licencia generic)
        {

            try
            {
                int cant = 0;
                
              bool estado = false;
                string query = "SELECT COUNT(*) FROM LICENCIASXLEGAJO";
                DataAccess.AbrirConexion();
               
                  cant = await DataAccess.obtenerCantFilas(query);



                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@idLicencia", cant + 1));
                sqlParameters.Add(new SqlParameter("@legajo", generic._legajo));
                sqlParameters.Add(new SqlParameter("@tipoLicencia", generic._tipoLicencia));
                sqlParameters.Add(new SqlParameter("@descripcion", generic._descripcion));
                sqlParameters.Add(new SqlParameter("@fechaInicio", generic._fechaIngreso));
                sqlParameters.Add(new SqlParameter("@fechaRegreso", generic._fechaRegreso));

              
                return   estado = await DataAccess.ExecStoredProcedureAsync("agregarLicencia", sqlParameters);

             

        
            }catch (Exception ex) {

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

        public Task<Licencia> Find(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Licencia>> GetAllAsyncLicencias(string LEGAJO,int aux)
        {
            try
            {
                List<Licencia> Listlicencia = new List<Licencia>();
                DataAccess.AbrirConexion();
                string query;

                if (aux == 0)
                {
                 query = "SELECT IDLICENCIA,LEGAJO,TIPOLICENCIA,DESCRIPCION,FECHAINICIO,FECHAREGRESO,BAJA FROM LICENCIASXLEGAJO WHERE BAJA = 0 AND LEGAJO=" + LEGAJO + "  AND NOT UPPER(TIPOLICENCIA) LIKE '%VACACIONES%'";

                }
                else
                {
                     query = "SELECT IDLICENCIA,LEGAJO,TIPOLICENCIA,DESCRIPCION,FECHAINICIO,FECHAREGRESO,BAJA FROM LICENCIASXLEGAJO WHERE BAJA = 0 AND LEGAJO=" + LEGAJO + "  AND  UPPER(TIPOLICENCIA) LIKE '%VACACIONES%'";

                }
                SqlDataReader reader = await DataAccess.GetReaderAsync(query);

                while (reader.Read())
                {
                    Listlicencia.Add(new Licencia
                    {
                        _idLicencia = (int)reader.GetInt32(0),
                        _legajo = (int)reader.GetInt64(1),
                        _tipoLicencia = reader.GetString(2),
                        _descripcion = !reader.IsDBNull(3) ? reader.GetString(3) : null,
                        _fechaIngreso = !reader.IsDBNull(4) ? reader.GetDateTime(4) : new DateTime(2999, 12, 1),
                        _fechaRegreso = !reader.IsDBNull(5) ? reader.GetDateTime(5): new DateTime(2999, 12, 1),

                    });
                }

                return Listlicencia;


            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DataAccess.CerrarConexion();
            }

            return null;

        }

        public Task<List<Licencia>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Licencia generic, string query)
        {
            throw new NotImplementedException();
        }
    }
}
