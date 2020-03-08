/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

            ObservableSynchronizedCollection collection = new ObservableSynchronizedCollection();
            ObservableCollectionListener collectionListener = new ObservableCollectionListener(collection);

            Task listenerTask = Task.Factory.StartNew(() => {
                collectionListener.StartListening(CollectionChanged);

                Task collectionModificationTask = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        collection.Add(i);
                    }
                });
            });

            listenerTask.Wait();
           
            Console.ReadLine();
        }

        private static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            try
            {
                ObservableCollection<int> collection = (ObservableCollection<int>)sender;
                foreach(var item in collection)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
