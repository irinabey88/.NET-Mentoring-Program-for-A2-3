/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code            
            Task.Factory.StartNew(() =>
               {
                   int[] array = ArrayGenerator.GenerateArray(15, 13, 25);
                   array.Print();
                   return array;
               }).ContinueWith((taskResult) =>
               {
                   int[] array = taskResult.Result.MultiplyWithNumber(8);
                   array.Print();
                   return array;
               }).ContinueWith((taskResult) =>
               {
                   int[] array = taskResult.Result.Sort();
                   array.Print();
                   return array;
               }).ContinueWith((taskResult) =>
               {
                   Console.WriteLine(taskResult.Result.CalculateAverageValue()); 
               });

            Console.ReadLine();
        }
    }
}
