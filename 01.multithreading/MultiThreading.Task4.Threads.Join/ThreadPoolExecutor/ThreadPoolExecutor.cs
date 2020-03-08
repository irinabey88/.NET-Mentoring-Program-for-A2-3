using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    public class ThreadPoolExecutor : IThreadPoolExecutor
    {
        private int _threadsCounter;
        private static Semaphore _pool;

        public ThreadPoolExecutor(int threadsCount)
        {
            if (threadsCount < 1)
            {
                throw new ArgumentException($"ThreadsCount value can not be less that 1.");
            }

            ThreadsCount = threadsCount;
            _threadsCounter = threadsCount;
            _pool = new Semaphore(4, 4);
        }

        public int ThreadsCount { get; }

        public void ExecuteThread(WaitCallback executionFunction)
        {
            if (executionFunction == null)
            {
                throw new ArgumentNullException(nameof(executionFunction));
            }

            if (_threadsCounter == 0)
            {
                return;
            }

            _threadsCounter--;
            ExecuteThread(executionFunction);
            ThreadPool.QueueUserWorkItem(executionFunction, _threadsCounter);
            _pool.WaitOne();
            _pool.Release();       
        }
    }
}
