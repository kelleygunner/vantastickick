using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VantasticKick.Utils
{
    public class Circle<T>
    {
        private Queue<T> _queue;
        private int _size;

        public Circle(int size)
        {
            _size = size;
            _queue = new Queue<T>();
        }

        public void Add(T item)
        {
            _queue.Enqueue(item);
            if (_queue.Count > _size)
            {
                _queue.Dequeue();
            }
        }

        public IEnumerable<T> ExcludeCircleFrom(IEnumerable<T> items)
        {
            return items.Except(_queue);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("items: ");
            foreach (var item in _queue)
            {
                sb.Append(item);
                sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}
