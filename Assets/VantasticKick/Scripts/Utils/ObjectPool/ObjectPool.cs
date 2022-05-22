using System.Collections.Generic;

namespace VantasticKick.Utils.ObjectPool
{
    public class ObjectPool<T>
    {
        private Queue<T> _pool;

        public ObjectPool(T[] items)
        {
            _pool = new Queue<T>(items);
        }

        public T Pull()
        {
            if (_pool.Count > 0)
            {
                return _pool.Dequeue();
            }

            return default;
        }

        public void Push(T item)
        {
            _pool.Enqueue(item);
        }
        
    }
}
