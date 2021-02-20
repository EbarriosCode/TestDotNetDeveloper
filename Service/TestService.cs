using Common;
using System;
using System.Data.SqlClient;

namespace Service
{
    public class TestService
    {
        public static void TestConnection()
        {
            try
            {
                using(var connection = new SqlConnection(Resources.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexión con Exito");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
