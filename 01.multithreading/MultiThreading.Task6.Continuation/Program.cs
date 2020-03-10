/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            Console.ReadLine();
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Task taskAParent = Task.Run(() =>
            {
                Console.WriteLine("Parent Task A-> ");
                PrintCurrentThreadInformation();
                Console.WriteLine();
                throw new Exception();
            });

            Task taskA = taskAParent.ContinueWith((result) => {
                Console.Write("Child task A ");
                PrintCurrentThreadInformation();
            });
            taskA.Wait();

            Console.ReadLine();
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Task taskBParent = Task.Run(() =>
            {
                Console.WriteLine("Parent Task B -> ");
                PrintCurrentThreadInformation();
                throw new InvalidOperationException("exception");
            });

            Task taskB = taskBParent.ContinueWith((result) => {
                Console.Write("Child task B");
                PrintCurrentThreadInformation();
            },
            TaskContinuationOptions.NotOnRanToCompletion);
            taskB.Wait();

            Console.ReadLine();
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Task taskCParent = Task.Run(() =>
            {
                Console.WriteLine("Parent Task C -> ");
                PrintCurrentThreadInformation();
                throw new Exception();
            });

            Task taskC = taskCParent.ContinueWith((result) => {
                Console.Write("Child task C ");
                PrintCurrentThreadInformation();
            }
            , TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            Console.ReadLine();
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task taskDParent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent Task D -> ");
                PrintCurrentThreadInformation();
                cancellationTokenSource.Token.WaitHandle.WaitOne();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }, cancellationTokenSource.Token);

            Task taskD = taskDParent.ContinueWith((result) =>
            {

                Console.WriteLine($"From Continuation: {result.Status}");

                Console.WriteLine("Task is canceled");
                Console.Write("Child task");
                PrintCurrentThreadInformation();
            }
            ,TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.HideScheduler);
            cancellationTokenSource.Cancel();
            taskD.Wait();

            Console.ReadLine();
        }

        private static void PrintCurrentThreadInformation() => Console.WriteLine($"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}");
    }
}
