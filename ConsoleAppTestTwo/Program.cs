using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppTestTwo
{
    class Program
    {
        /// <summary>
        /// Write a method that finds, efficiently with respect to time used, all numbers that occur exactly once in the input collection.
        /// For example, FindUniqueNumbers(new int[] { 1, 2, 1, 3 }) should return {2, 3}
        /// </summary>        
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 1, 2, 1, 3, 3, 4, 5, 6, 6, 2, 100, 99, 99 };
            foreach (var number in FindUniqueNumbers(numbers))
                Console.WriteLine(number);

        }

        public static IEnumerable<int> FindUniqueNumbers(IEnumerable<int> numbers)
        {
            List<int> result = new();
            QuickSort(0, numbers.Count() - 1, numbers as int[]);            
            int n = numbers.Count();

            if (numbers.ElementAt(0) != numbers.ElementAt(1))            
                result.Add(numbers.ElementAt(0));                            

            for (int i = 1; i < n - 1; i++)
            {
                if (numbers.ElementAt(i) != numbers.ElementAt(i + 1) && numbers.ElementAt(i) != numbers.ElementAt(i - 1))                
                    result.Add(numbers.ElementAt(i));                                    
            }
            
            if (numbers.ElementAt(n - 2) != numbers.ElementAt(n - 1))
            {
                result.Add(numbers.ElementAt(n - 1));                
            }

            return result;
        }

        // Algoritmo QuickSort
        static void QuickSort(int inicio, int fin, int[] list)
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

        static int Particion(int inicio, int fin, int[] list)
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

        static void Swap(int index1, int index2, int[] list)
        {
            int temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
