using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    interface IThreadExecutor
    {
        void ExecuteThread(ParameterizedThreadStart executionFunction);

        int ThreadsCount { get; }
    }
}
