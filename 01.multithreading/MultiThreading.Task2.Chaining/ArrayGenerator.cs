using System;
using System.Linq;

namespace MultiThreading.Task2.Chaining
{
    public static class ArrayGenerator
    {
        public static int[] GenerateArray(int size, int minValue, int maxValue)
        {
            if(size < 0)
            {
                throw new ArgumentException("Array size can not be negative number");
            }
            if(minValue > maxValue)
            {
                throw new ArgumentException("MinValue can not be more than MaxValue");
            }

            Random randNum = new Random();
            int[] resultArray = Enumerable.Repeat(0, size)
                .Select(element => randNum.Next(minValue, maxValue))
                .ToArray();

            return resultArray;
        }
    }
}
