using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    public class ThreadExecutor : IThreadExecutor
    {
        private int _threadsCounter;

        public ThreadExecutor(int threadsCount)
        {
            if(threadsCount < 1)
            {
                throw new ArgumentException($"ThreadsCount value can not be less that 1.");
            }

            ThreadsCount = threadsCount;
            _threadsCounter = threadsCount;
        }

        public int ThreadsCount { get; }

        public void ExecuteThread(ParameterizedThreadStart executionFunction)
        {
            if(executionFunction == null)
            {
                throw new ArgumentNullException(nameof(executionFunction));
            }
            
            if(_threadsCounter == 0)
            {
                return;
            }

            _threadsCounter--;
            Thread thread = new Thread(executionFunction);
            thread.Start(_threadsCounter);
            thread.Join();
            ExecuteThread(executionFunction);
        }
    }
}
