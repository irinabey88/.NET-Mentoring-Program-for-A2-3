using MultiThreading.Task5.Threads.SharedCollection.CollectionListener;
using System;
using System.Collections.Specialized;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    public class ObservableCollectionListener : IObservableCollectionListener
    {
        private readonly ObservableSynchronizedCollection _collection;

        public ObservableCollectionListener(ObservableSynchronizedCollection collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public void StartListening(NotifyCollectionChangedEventHandler eventHandler)
        {
            if(eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            _collection.Collection.CollectionChanged += eventHandler;
        }

        public void StopListening(NotifyCollectionChangedEventHandler eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            _collection.Collection.CollectionChanged -= eventHandler;
        }
    }
}
