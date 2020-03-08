using System.Collections.ObjectModel;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    public class ObservableSynchronizedCollection
    {
        private ReaderWriterLockSlim collectionLock = new ReaderWriterLockSlim();
        private ObservableCollection<int> _collection = new ObservableCollection<int>();

        public ObservableCollection<int> Collection
        {
            get
            {
                collectionLock.EnterReadLock();
                try
                {
                    return _collection;
                }
                finally
                {
                    collectionLock.ExitReadLock();
                }
            }
        }

        public void Add(int value)
        {
            collectionLock.EnterWriteLock();
            try
            {
                _collection.Add(value);
            }
            finally
            {
                collectionLock.ExitWriteLock();
            }
        }
    }
}
