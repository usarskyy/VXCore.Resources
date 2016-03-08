using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace VXCore.Resources
{
    /// <summary>
    /// Type that represents empty readonly dictionary. Used for internal purposes.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    internal class EmptyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly static ReadOnlyCollection<TKey> _keys = new ReadOnlyCollection<TKey>(new TKey[0]);
        private readonly static ReadOnlyCollection<TValue> _values = new ReadOnlyCollection<TValue>(new TValue[0]);

        public static readonly IDictionary<TKey, TValue> Instance = new EmptyDictionary<TKey, TValue>();


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new System.NotSupportedException();
        }

        public void Clear()
        {
            throw new System.NotSupportedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new System.NotSupportedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new System.NotSupportedException();
        }

        public int Count
        {
            get { return 0; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool ContainsKey(TKey key)
        {
            return false;
        }

        public void Add(TKey key, TValue value)
        {
            throw new System.NotSupportedException();
        }

        public bool Remove(TKey key)
        {
            throw new System.NotSupportedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default (TValue);

            return false;
        }

        public TValue this[TKey key]
        {
            get { throw new System.NotSupportedException(); }
            set { throw new System.NotSupportedException(); }
        }

        public ICollection<TKey> Keys
        {
            get { return _keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _values; }
        }
    }
}