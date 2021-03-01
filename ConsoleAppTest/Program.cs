using System;
using System.Collections.Generic;

namespace ConsoleAppTest
{
    class Program
    {
        /// <summary>
        /// Implement the FindMaxSum method that, efficiently with respect to time used, returns the largest sum of any two elements in the given list of positive numbers.
        /// For example, the largest sum of the list {5, 9, 7, 11} is the sum of the elements 9 and 11, wich is 20.        
        /// </summary>
        
        static void Main(string[] args)
        {            
            List<int> arr = new List<int> { 52, 12, 34, 10, 6, 40, 12, 40, 100 };
            Console.WriteLine(FindMaxSum(arr));            
        }

        static int FindMaxSum(List<int> list)
        {
            // Ordenamiento por QuickSort
            QuickSort(0, list.Count - 1, list);

            // Buscar los dos últimos índices, es en donde estan los valores mayores
            int result = 0;
            int length = list.Count;

            for (int i = 0; i < length; i++)
            {
                if (i == length-1)
                    result = list[i] + list[i - 1];
            }

            return result;
        }

        // Algoritmo QuickSort
        static void QuickSort(int inicio, int fin, List<int> list)
        {
            int indexPivote;

            // Caso base, un elemento o fragmento inválido
            if (inicio >= fin)
                return;

            // Obtener el indice del pivote para el fragmento actual con el que estamos trabajando
            indexPivote = Particion(inicio, fin, list);

            // Casos Inductivos
            QuickSort(inicio, indexPivote - 1, list); // Fragmento de la izquierda
            QuickSort(indexPivote + 1, fin, list);    // Fragmento de la derecha
        }

        static int Particion(int inicio, int fin, List<int> list)
        {
            int pivote;
            int indexPivote;
            int iteracion;
           
            pivote = list[fin];            
            indexPivote = inicio;
            
            for (iteracion = inicio; iteracion < fin; iteracion++)
            {                
                if (list[iteracion] <= pivote)
                {                    
                    Swap(iteracion, indexPivote, list);                    
                    indexPivote++;
                }
            }
            
            Swap(indexPivote, fin, list);

            return indexPivote;
        }

        static void Swap(int index1, int index2, List<int> list)
        {
            int temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
