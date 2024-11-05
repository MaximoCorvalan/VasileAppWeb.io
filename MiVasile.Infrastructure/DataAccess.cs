using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace MiVasile.Infrastructure
{
    public static class DataAccess
    {
        public static string ConnectionString { get; set; }
        public static SqlCommand Command { get; set; }
        public static SqlConnection Connection { get; set; }
        static DataAccess()
        {
            ConnectionString = "Server=tcp:vasileweb.database.windows.net,1433;Initial Catalog=VasileWebBD;Persist Security Info=False;User ID=Vasile;Password=Vas1234_;" +
                                "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Connection = new SqlConnection(ConnectionString);
        }

        public static void AbrirConexion()
        {
            Connection.Open();
        }

     

        public static void CerrarConexion()
        {
            Connection.Close();
        }

        public static async Task<SqlDataReader> GetReaderAsync(string sql)
        {
            try
            {
                if (Connection.State != System.Data.ConnectionState.Open)
                {
                    await Connection.OpenAsync();
                }

                using (Command = new SqlCommand(sql, Connection))
                {
                    return  Command.ExecuteReader();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }





        public static async Task<bool> ExecCommandAsync(string query)
        {
            try
            {
                using (Command = new SqlCommand(query, Connection))
                {
                    int filasAfectadas = await Command.ExecuteNonQueryAsync();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<int> obtenerCantFilas(string sql)
        {
            
              
                if (Connection.State == ConnectionState.Closed) // Verifica si la conexión está cerrada
                {
                    await Connection.OpenAsync();
                }

                using (SqlCommand command = new SqlCommand(sql, Connection))
                {
                    object result = await command.ExecuteScalarAsync();
                   
                    return result != null ? Convert.ToInt32(result) : 0;

                }
            
        }


        public static async Task<bool> ExecStoredProcedureAsync(string storedProcedureName, List<SqlParameter> parameters)
        {
            try
            {
               
                
                // Asegúrate de que la conexión esté abierta antes de crear el comando
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection = new SqlConnection(ConnectionString);
                    await Connection.OpenAsync();
                }

                using (var command = new SqlCommand(storedProcedureName, Connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Agregar los parámetros al comando
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    // Ejecutar el procedimiento almacenado
                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción (opcional: registra el error)
                Console.WriteLine($"Error en ExecStoredProcedureAsync: {ex.Message}");
                return false;
            }
        }
    }
}
