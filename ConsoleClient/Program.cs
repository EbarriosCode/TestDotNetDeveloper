using Models;
using Service;
using System;
using System.Collections.Generic;
using System.Data;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            var tableHash = new Dictionary<string, object>();

            //tableHash.Add("@Nombre", "Último");               
            //Create(tableHash);

            //tableHash.Add("@Nombre", "Editado Hoy");
            //tableHash.Add("@ProyectoID", "6");
            //Update(tableHash);

            tableHash.Add("@ProyectoID", "7");
            Delete(tableHash);

            //ListWithDataSet();
            Console.Read();
        }

        static void ListWithDataSet()
        {
            DataSet dataSet = ProyectosService.GetDataSet("SELECT * FROM Proyecto");

            Console.WriteLine($"Número de filas en el DataSet: {dataSet.Tables[0].Rows.Count}");
            Console.WriteLine();

            foreach (DataRow item in dataSet.Tables[0].Rows)
            {
                Console.WriteLine($"ID: {item["ProyectoID"]}, Nombre: {item["Nombre"]}");
            }
        }

        static void Create(Dictionary<string, object> tableHash)
        {            
            int rowAffected = ProyectosService.Actions("INSERT INTO Proyecto (Nombre) VALUES (@Nombre)",
                                                       (int)ActionsDataBase.Create, tableHash);

            Console.WriteLine($"Filas afectadas al Crear: {rowAffected}");
            ListWithDataSet();
        }

        static void Update(Dictionary<string, object> tableHash)
        {
            
            
            int rowAffected = ProyectosService.Actions("UPDATE Proyecto SET Nombre = @Nombre WHERE ProyectoID = @ProyectoID",
                                                       (int)ActionsDataBase.Update, tableHash);

            Console.WriteLine($"Filas afectadas al Actualizar: {rowAffected}");
            ListWithDataSet();
        }

        static void Delete(Dictionary<string, object> tableHash)
        {
            int rowAffected = ProyectosService.Actions("DELETE FROM Proyecto WHERE ProyectoID = @ProyectoID",
                                                        (int)ActionsDataBase.Delete, tableHash);

            Console.WriteLine($"Filas afectadas al Eliminar: {rowAffected}");
            ListWithDataSet();
        }
    }
}
