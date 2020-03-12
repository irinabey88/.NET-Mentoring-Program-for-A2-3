using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    public class SynchronizedCollection
    {
        #region Private fields

        private static Semaphore _pool = new Semaphore(1, 1);
        private List<int> _collection = new List<int>();

        #endregion

        public void PrintCollection()
        {
            _pool.WaitOne();

            Console.WriteLine($"Print collection {PrintCurrentThreadInformation()}");
            _collection.ForEach(element => Console.Write($"{element}, "));
            Console.WriteLine();

            _pool.Release();
        }

        public void AddElement(int element)
        {
            _pool.WaitOne();

            Console.WriteLine($"Added element {element} {PrintCurrentThreadInformation()}");            
            _collection.Add(element);

            _pool.Release();
        }

        #region Private methods

        private static string PrintCurrentThreadInformation() => $"Thread={Thread.CurrentThread.ManagedThreadId}";

        #endregion
    }
}
