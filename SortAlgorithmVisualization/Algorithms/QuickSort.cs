using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithmVisualization.Algorithms
{
    public class QuickSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        private Stack<int> stack;
        private bool flag;
        private int low_bound;
        private int high_bound;
        private int low;
        private int high;
        private T pivot;
        private T[] _data;
        public T[] Data => _data;

        public void Initialize(T[] data)
        {
            _data = new T[data.Length];
            data.CopyTo(_data, 0);
            stack = new Stack<int>();
            flag = false;
            low_bound = 0;
            high_bound = _data.Length - 1;
            stack.Push(low_bound);
            stack.Push(high_bound);
       }

        public bool Step(out int[] changed)
        {
            changed = null;
            if (!flag && stack.Count == 0) return true;
            if (!flag)
            {
                high = stack.Pop();
                low = stack.Pop();
                pivot = _data[low];
                flag = true;
            }
            if (low >= high)
            {
                _data[low] = pivot;
                flag = false;
                var mid = low;
                if (low_bound < mid - 1)
                {
                    stack.Push(low_bound);
                    stack.Push(mid - 1);
                }
                if (mid + 1 < high_bound)
                {
                    stack.Push(mid + 1);
                    stack.Push(high_bound);
                }
            }
            while (low<high&&_data[high].CompareTo(pivot) != -1) high--;
            _data[low] = _data[high];
            while (low < high && _data[low].CompareTo(pivot) != 1) low++;
            _data[high] = _data[low];
            changed = new[] { low, high };
            return false;
        }
    }
}
