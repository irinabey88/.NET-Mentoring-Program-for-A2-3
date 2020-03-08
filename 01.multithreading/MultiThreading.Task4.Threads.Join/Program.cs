/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            Console.WriteLine("Thread");

            IThreadExecutor threadExecutor = new ThreadExecutor(10);
            threadExecutor.ExecuteThread(TreadExecutionFunction);

            Console.WriteLine("ThreadPool");

            IThreadPoolExecutor threadPoolExecutor = new ThreadPoolExecutor(10);
            threadPoolExecutor.ExecuteThread(TreadExecutionFunction);

            Console.ReadLine();
        }

        static void TreadExecutionFunction(object state)
        {
            try
            {
                int counter = (int)state;

                Console.WriteLine($"Counter = {counter}. New tread is started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception arrise : {ex.Message}");
            }
        }
    }
}
