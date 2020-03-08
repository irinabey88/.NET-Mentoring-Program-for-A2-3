using System.Collections.Specialized;

namespace MultiThreading.Task5.Threads.SharedCollection.CollectionListener
{
    public interface IObservableCollectionListener
    {
        void StartListening(NotifyCollectionChangedEventHandler eventHandler);

        void StopListening(NotifyCollectionChangedEventHandler eventHandler);
    }
}
