/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();


            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            SynchronizedCollection synchronizedCollection = new SynchronizedCollection();

            Task printTask = new Task(() => {

                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Stop execution");
                        break;
                    }

                    synchronizedCollection.PrintCollection();
                }
            });           

            Task modificationTask  = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    synchronizedCollection.AddElement(i);
                }

                cancelTokenSource.Cancel();
            });

            printTask.Start();
            modificationTask.Start();
            Task.WaitAll(modificationTask, printTask);

            Console.ReadLine();
        }
    }
}
