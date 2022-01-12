using ERSA.Mobile.AdminApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace ERSA.Mobile
{
    internal class ObservableList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>,  INotifyCollectionChanged
    {

        public ObservableList()
        {
            innerList = new List<T>();
        }

        public ObservableList(IEnumerable<T> collection)
        {
            innerList = new List<T>(collection); 
        }

        public ObservableList(int capacity)
        {
            innerList = new List<T>(capacity);
        }

        private List<T> innerList;

        public int Count => innerList.Count;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => null;

        public bool IsReadOnly => false;

        T IList<T>.this[int index] 
        { 
            get => innerList[index];
            set 
            {
                innerList[index] = value;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }   
        

        public T this[int index]
        {
            get => innerList[index];
            set
            {
                innerList[index] = value;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public void Add(T item)
        {
            innerList.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Clear()
        {
            innerList.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item) => innerList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => innerList.CopyTo(array, arrayIndex);

        internal void AddRange(IEnumerable<T> range)
        {
            innerList.AddRange(range);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Remove(T item)
        {
            if (innerList.Remove(item))
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator() => innerList.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => innerList.GetEnumerator();


        public void RemoveAt(int index)
        {
            innerList.RemoveAt(index);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public int IndexOf(T item) => innerList.IndexOf(item);

        public void Insert(int index, T item)
        {
            innerList.Insert(index, item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
