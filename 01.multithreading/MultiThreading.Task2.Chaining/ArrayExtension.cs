using System;
using System.Linq;

namespace MultiThreading.Task2.Chaining
{
    public static class ArrayExtension
    {
        public static int[] MultiplyWithNumber(this int[] array, int multiplier)
        {
            if(array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return array.Select(element => element * multiplier)
                        .ToArray();
        }

        public static int[] Sort(this int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return array.OrderBy(element => element).ToArray();
        }

        public static double CalculateAverageValue(this int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return array.Average();
        }

        public static void Print(this int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            array.ToList().ForEach(element => Console.Write($"{element} "));

            Console.WriteLine();
        }
    }
}
