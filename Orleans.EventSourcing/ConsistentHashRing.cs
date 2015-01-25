using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{

    /// <summary>
    /// Consistent hash ring with generic key and value types.
    /// </summary>
    public class ConsistentHashRing<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private class Item
        {
            public readonly TKey Key;
            public readonly TValue Value;
            public Item Left;
            public Item Right;

            public Item(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
        private Item _root = null;
        private long _count = 0;
        private Func<TKey, TKey, int> _comparer;
        private bool _tiebreaker = true; // used to balance Remove

        /// <summary>
        /// Creates an empty ring using default comparer for TKey.
        /// </summary>
        public ConsistentHashRing()
        {
            _comparer = Comparer<TKey>.Default.Compare;
        }
        /// <summary>
        /// Creates an empty ring using given comparer for TKey.
        /// </summary>
        public ConsistentHashRing(IComparer<TKey> comparer)
        {
            _comparer = comparer.Compare;
        }
        /// <summary>
        /// Creates an empty ring using given compare function for TKey.
        /// </summary>
        public ConsistentHashRing(Func<TKey, TKey, int> comparer)
        {
            _comparer = comparer;
        }

        /// <summary>
        /// Adds node with given key to ring.
        /// </summary>
        public void Add(TKey nodeKey, TValue node)
        {
            Insert(ref _root, nodeKey, node);
        }

        /// <summary>
        /// Removes node with given key from ring.
        /// </summary>
        public void Remove(TKey nodeKey)
        {
            Remove(ref _root, nodeKey);
        }

        /// <summary>
        /// Gets entry with key following x (or x if it is a key).
        /// </summary>
        public KeyValuePair<TKey, TValue> GetNearest(TKey x)
        {
            if (_root == null) throw new Exception("No nodes specified.");
            var item = GetNearest(x, _root);
            return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
        }

        /// <summary>
        /// Gets entry with key preceding x (or x if it is a key).
        /// </summary>
        public KeyValuePair<TKey, TValue> GetNearestBackwards(TKey x)
        {
            if (_root == null) throw new Exception("No nodes specified.");
            var item = GetNearestBackwards(x, _root);
            return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
        }

        /// <summary>
        /// Gets entry succeeding given key.
        /// The key has to exist, if not use GetNearest or GetNearestBackwards.
        /// </summary>
        public KeyValuePair<TKey, TValue> GetSuccessor(TKey key)
        {
            if (_root == null) throw new Exception("No nodes specified.");
            Item prev = null;
            int comparison = 0;
            var item = _root;
            while ((comparison = _comparer(key, item.Key)) != 0)
            {
                if (comparison < 0) prev = item; ;
                item = (comparison < 0) ? item.Left : item.Right;
                if (item == null) throw new ArgumentOutOfRangeException();
            }

            if (item.Right != null) item = GetMin(item.Right);
            else if (prev != null) item = prev;
            else item = GetMin(_root);

            return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
        }

        /// <summary>
        /// Gets entry preceding given key.
        /// The key has to exist, if not use GetNearest or GetNearestBackwards.
        /// </summary>
        public KeyValuePair<TKey, TValue> GetPredecessor(TKey key)
        {
            if (_root == null) throw new Exception("No nodes specified.");
            Item prev = null;
            int comparison = 0;
            var item = _root;
            while ((comparison = _comparer(key, item.Key)) != 0)
            {
                if (comparison > 0) prev = item; ;
                item = (comparison < 0) ? item.Left : item.Right;
                if (item == null) throw new ArgumentOutOfRangeException();
            }

            if (item.Left != null) item = GetMax(item.Left);
            else if (prev != null) item = prev;
            else item = GetMax(_root);

            return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
        }

        /// <summary>
        /// Enumerates ring starting at key following x.
        /// </summary>
        public IEnumerable<KeyValuePair<TKey, TValue>> EnumerateFromNearest(TKey x)
        {
            var nearest = GetNearest(x, _root);
            var enumerator = new EnumeratorIncreasing(_root, nearest, _comparer);
            while (enumerator.MoveNext()) yield return enumerator.Current;

            enumerator = new EnumeratorIncreasing(_root);
            while (enumerator.MoveNext())
            {
                if (_comparer(enumerator.Current.Key, nearest.Key) == 0) yield break;
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Enumerates ring starting at key following x.
        /// Skips duplicate values.
        /// </summary>
        public IEnumerable<KeyValuePair<TKey, TValue>> EnumerateFromNearestDistinct(TKey key)
        {
            var items = new HashSet<TValue>();
            var nearest = GetNearest(key, _root);
            var enumerator = new EnumeratorIncreasing(_root, nearest, _comparer);
            while (enumerator.MoveNext()) if (items.Add(enumerator.Current.Value)) yield return enumerator.Current;

            enumerator = new EnumeratorIncreasing(_root);
            while (enumerator.MoveNext())
            {
                if (_comparer(enumerator.Current.Key, nearest.Key) == 0) yield break;
                if (items.Add(enumerator.Current.Value)) yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Enumerates ring backwards starting at key preceding x.
        /// </summary>
        public IEnumerable<KeyValuePair<TKey, TValue>> EnumerateFromNearestBackwards(TKey key)
        {
            var nearest = GetNearestBackwards(key, _root);
            var enumerator = new EnumeratorDecreasing(_root, nearest, _comparer);
            while (enumerator.MoveNext()) yield return enumerator.Current;

            enumerator = new EnumeratorDecreasing(_root);
            while (enumerator.MoveNext())
            {
                if (_comparer(enumerator.Current.Key, nearest.Key) == 0) yield break;
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Enumerates ring backwards starting at key preceding x.
        /// Skips duplicate values.
        /// </summary>
        public IEnumerable<KeyValuePair<TKey, TValue>> EnumerateFromNearestBackwardsDistinct(TKey key)
        {
            var items = new HashSet<TValue>();
            var nearest = GetNearestBackwards(key, _root);
            var enumerator = new EnumeratorDecreasing(_root, nearest, _comparer);
            while (enumerator.MoveNext()) if (items.Add(enumerator.Current.Value)) yield return enumerator.Current;

            enumerator = new EnumeratorDecreasing(_root);
            while (enumerator.MoveNext())
            {
                if (_comparer(enumerator.Current.Key, nearest.Key) == 0) yield break;
                if (items.Add(enumerator.Current.Value)) yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Gets number of entries in ring.
        /// </summary>
        public long Count { get { return _count; } }



        private Item Get(TKey key)
        {
            return Get(key, _root);
        }
        private Item Get(TKey key, Item item)
        {
            if (item == null) return null;

            var comparison = _comparer(key, item.Key);
            if (comparison == 0) return item;
            return Get(key, comparison < 0 ? item.Left : item.Right);
        }
        private Item GetNearest(TKey key, Item item)
        {
            var comparison = _comparer(item.Key, key);

            // perfect match
            if (comparison == 0)
            {
                // -> done
                return item;
            }

            // item is larger
            if (comparison > 0)
            {
                // ... and there is no smaller one
                if (item.Left == null)
                {
                    // -> done
                    return item;
                }
                // ... and there is a smaller one
                else
                {
                    // ... and the largest of the smaller items is smaller then the current item
                    var largestOfTheSmaller = GetMax(item.Left);
                    if (_comparer(largestOfTheSmaller.Key, key) < 0)
                    {
                        // -> then we already have the best item
                        return item;
                    }
                    // ... and the largest of the smaller items is still larger then the current item
                    else
                    {
                        // -> then search left for a smaller item that is still larger than key
                        return GetNearest(key, item.Left);
                    }
                }
            }
            // item is smaller
            else
            {
                // ... and there is no larger one
                if (item.Right == null)
                {
                    // -> wrap around and return globally smallest
                    return GetMin(_root);
                }
                // ... and there is a larger one
                else
                {
                    // -> search right for an item larger than key
                    return GetNearest(key, item.Right);
                }
            }
        }
        private Item GetNearestBackwards(TKey key, Item item)
        {
            var comparison = _comparer(item.Key, key);

            // perfect match
            if (comparison == 0)
            {
                // -> done
                return item;
            }

            // item is smaller
            if (comparison < 0)
            {
                // ... and there is no larger one
                if (item.Right == null)
                {
                    // -> done
                    return item;
                }
                // ... and there is a larger one
                else
                {
                    // ... and the smallest of the larger items is larger then the current item
                    var smallestOfTheLarger = GetMin(item.Right);
                    if (_comparer(smallestOfTheLarger.Key, key) > 0)
                    {
                        // -> then we already have the best item
                        return item;
                    }
                    // ... and the smallest of the larger items is still smaller then the current item
                    else
                    {
                        // -> then search right for a larger item that is still smaller than key
                        return GetNearestBackwards(key, item.Right);
                    }
                }
            }
            // item is larger
            else
            {
                // ... and there is no smaller one
                if (item.Left == null)
                {
                    // -> wrap around and return globally largest
                    return GetMax(_root);
                }
                // ... and there is a smaller one
                else
                {
                    // -> search left for an item smaller than key
                    return GetNearestBackwards(key, item.Left);
                }
            }
        }
        private Item GetMin(Item item)
        {
            if (item.Left != null) return GetMin(item.Left);
            return item;
        }
        private Item GetMax(Item item)
        {
            if (item.Right != null) return GetMax(item.Right);
            return item;
        }
        private int GetCount(Item item)
        {
            if (item == null) return 0;
            return 1 + GetCount(item.Left) + GetCount(item.Right);
        }
        private void Insert(ref Item item, TKey key, TValue value)
        {
            if (item == null)
            {
                item = new Item(key, value) { Left = null, Right = null };
                _count++;
            }
            else
            {
                var comparison = _comparer(key, item.Key);
                if (comparison < 0)
                {
                    Insert(ref item.Left, key, value);
                }
                else if (comparison > 0)
                {
                    Insert(ref item.Right, key, value);
                }
                else
                {
                    item = new Item(item.Key, value) { Left = item.Left, Right = item.Right };
                    //item.Value = value;
                }
            }
        }
        private void Remove(ref Item item, TKey key)
        {
            if (item == null) return;
            var comparison = _comparer(key, item.Key);
            if (comparison == 0)
            {
                if (item.Left == null && item.Right == null)
                {
                    item = null;
                    _count--;
                    return;
                }

                int a = DepthRight(item.Left);
                int b = DepthLeft(item.Right);
                if (a == b)
                {
                    if (_tiebreaker) a++; else b++;
                    _tiebreaker = !_tiebreaker;
                }

                if (a > b)
                {
                    var replacement = RemoveLargest(ref item.Left);
                    replacement.Left = item.Left;
                    replacement.Right = item.Right;
                    item = replacement;
                    _count--;
                }
                else
                {
                    var replacement = RemoveSmallest(ref item.Right);
                    replacement.Left = item.Left;
                    replacement.Right = item.Right;
                    item = replacement;
                    _count--;
                }
            }
            else
            {
                if (comparison < 0)
                {
                    Remove(ref item.Left, key);
                }
                else
                {
                    Remove(ref item.Right, key);
                }
            }
        }
        private Item RemoveSmallest(ref Item item)
        {
            if (item == null) return null;

            if (item.Left != null) return RemoveSmallest(ref item.Left);

            var result = item;
            item = item.Right;
            return result;
        }
        private Item RemoveLargest(ref Item item)
        {
            if (item == null) return null;

            if (item.Right != null) return RemoveLargest(ref item.Right);

            var result = item;
            item = item.Left;
            return result;
        }
        private int DepthLeft(Item item)
        {
            if (item == null) return 0;
            return 1 + DepthLeft(item.Left);
        }
        private int DepthRight(Item item)
        {
            if (item == null) return 0;
            return 1 + DepthRight(item.Right);
        }

        #region ToString

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return ToString(_root);
        }

        private string ToString(Item item)
        {
            if (item == null) return "[]";
            if (item.Left == null && item.Right == null) return "[" + item.Key.ToString() + "]";
            return string.Format("[{0} {1},{2}]", item.Key, ToString(item.Left), ToString(item.Right));
        }

        #endregion

        #region IEnumerable

        private class EnumeratorIncreasing : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private Item _root;
            private Item _current;
            private Stack<Item> _stack;
            private int _state = -1;

            public EnumeratorIncreasing(Item root)
            {
                _root = root;
            }

            public EnumeratorIncreasing(Item root, Item startAt, Func<TKey, TKey, int> compare)
            {
                _root = root;
                _stack = new Stack<Item>();

                var x = _root;
                while (x != startAt)
                {
                    var comparison = compare(startAt.Key, x.Key);
                    if (comparison == 0) throw new InvalidOperationException();

                    if (comparison < 0) _stack.Push(x);

                    x = comparison < 0 ? x.Left : x.Right;
                }
                _current = x;
                _state = 10;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    if (_current == null) throw new InvalidOperationException();
                    //Console.WriteLine("[DEBUG] Current -> {0}", _current.Key);
                    return new KeyValuePair<TKey, TValue>(_current.Key, _current.Value);
                }
            }

            public void Dispose()
            {
                _root = null;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_state == -1)
                {
                    if (_root == null) return false;

                    _stack = new Stack<Item>();
                    var x = _root;
                    while (x.Left != null) { _stack.Push(x); x = x.Left; }
                    _current = x;
                    _state = 1;
                    return true;
                }

                if (_state == 0)
                {
                    if (_stack.Count == 0) return false;
                    _current = _stack.Pop();
                    _state = 1;
                    return true;
                }

                if (_state == 1)
                {
                    if (_current.Right == null)
                    {
                        _state = 0;
                        return MoveNext();
                    }

                    var x = _current.Right;
                    while (x.Left != null) { _stack.Push(x); x = x.Left; }
                    _current = x;
                    _state = 1;
                    return true;
                }

                if (_state == 10)
                {
                    _state = 1;
                    return true;
                }

                throw new InvalidOperationException();
            }

            public void Reset()
            {
                _current = null;
                _stack = null;
                _state = -1;
            }
        }

        private class EnumeratorDecreasing : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private Item _root;
            private Item _current;
            private Stack<Item> _stack;
            private int _state = -1;

            public EnumeratorDecreasing(Item root)
            {
                _root = root;
            }

            public EnumeratorDecreasing(Item root, Item startAt, Func<TKey, TKey, int> compare)
            {
                _root = root;
                _stack = new Stack<Item>();

                var x = _root;
                while (x != startAt)
                {
                    var comparison = compare(startAt.Key, x.Key);
                    if (comparison == 0) throw new InvalidOperationException();

                    if (comparison > 0) _stack.Push(x);

                    x = comparison < 0 ? x.Left : x.Right;
                }
                _current = x;
                _state = 10;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    if (_current == null) throw new InvalidOperationException();
                    //Console.WriteLine("[DEBUG] Current -> {0}", _current.Key);
                    return new KeyValuePair<TKey, TValue>(_current.Key, _current.Value);
                }
            }

            public void Dispose()
            {
                _root = null;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_state == -1)
                {
                    if (_root == null) return false;

                    _stack = new Stack<Item>();
                    var x = _root;
                    while (x.Right != null) { _stack.Push(x); x = x.Right; }
                    _current = x;
                    _state = 1;
                    return true;
                }

                if (_state == 0)
                {
                    if (_stack.Count == 0) return false;
                    _current = _stack.Pop();
                    _state = 1;
                    return true;
                }

                if (_state == 1)
                {
                    if (_current.Left == null)
                    {
                        _state = 0;
                        return MoveNext();
                    }

                    var x = _current.Left;
                    while (x.Right != null) { _stack.Push(x); x = x.Right; }
                    _current = x;
                    _state = 1;
                    return true;
                }

                if (_state == 10)
                {
                    _state = 1;
                    return true;
                }

                throw new InvalidOperationException();
            }

            public void Reset()
            {
                _current = null;
                _stack = null;
                _state = -1;
            }
        }

        /// <summary>
        /// Supports a simple iteration over a generic collection.
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new EnumeratorIncreasing(_root);

        }

        /// <summary>
        /// Supports a simple iteration over a collection.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
