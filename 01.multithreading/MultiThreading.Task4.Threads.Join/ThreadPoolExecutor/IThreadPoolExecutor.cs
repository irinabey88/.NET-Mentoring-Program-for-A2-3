using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    interface IThreadPoolExecutor
    {
        void ExecuteThread(WaitCallback executionFunction);

        int ThreadsCount { get; }
    }
}
