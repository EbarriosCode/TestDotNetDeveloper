using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProyectosService
    {        
        public static DataSet GetDataSet(string query)
        {
            var dataSet = new DataSet();
            
            try
            {
                using (var context = new SqlConnection(Resources.ConnectionString))
                {
                    context.Open();

                    using (var command = new SqlCommand(query, context))
                    {
                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return dataSet;
        }

        public static int Actions(string query, int action, Dictionary<string, object> values)
        {
            int rowsAffected = 0;

            try
            {
                using (var context = new SqlConnection(Resources.ConnectionString))
                {
                    context.Open();

                    switch (action)
                    {
                        case (int)ActionsDataBase.Create:
                            using (var command = new SqlCommand(query, context))
                            {
                                if (values != null && values.ContainsKey("@Nombre"))
                                {
                                    command.Parameters.AddWithValue("@Nombre", values["@Nombre"]);

                                    rowsAffected = command.ExecuteNonQuery();
                                }
                            }
                            break;

                        case (int)ActionsDataBase.Update:
                            using (var command = new SqlCommand(query, context))
                            {
                                if (values != null && values.ContainsKey("@Nombre") && values.ContainsKey("@ProyectoID"))
                                {
                                    command.Parameters.AddWithValue("@Nombre", values["@Nombre"]);
                                    command.Parameters.Add(new SqlParameter()
                                    {
                                        DbType = DbType.Int32,
                                        ParameterName = "@ProyectoID",
                                        Value = values["@ProyectoID"]
                                    });

                                    rowsAffected = command.ExecuteNonQuery();
                                }
                            }
                            break;

                        case (int)ActionsDataBase.Delete:
                            using (var command = new SqlCommand(query, context))
                            {
                                if (values != null && values.ContainsKey("@ProyectoID"))
                                {
                                    command.Parameters.Add(new SqlParameter()
                                    {
                                        DbType = DbType.Int32,
                                        ParameterName = "@ProyectoID",
                                        Value = values["@ProyectoID"]
                                    });

                                    rowsAffected = command.ExecuteNonQuery();
                                }
                            }
                            break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }
    }
}
