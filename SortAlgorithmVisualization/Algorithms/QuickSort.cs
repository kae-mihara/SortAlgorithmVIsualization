using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithmVisualization.Algorithms
{
    public class QuickSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        private Stack<int> low_vals;
        private Stack<int> high_vals;
        private Stack<bool> flags;

        private T[] _data;
        public T[] Data => _data;

        public void Initialize(T[] data)
        {
            _data = new T[data.Length];
            data.CopyTo(_data, 0);
            low_vals = new Stack<int>();
            high_vals = new Stack<int>();
            flags = new Stack<bool>();
            flags.Push(false);
            low_vals.Push(0);
            high_vals.Push(_data.Length);
        }

        public bool Step()
        {
            //
            var flag = flags.Peek();
            if(flag)
            {
                var low = low_vals.Peek();
                var high = high_vals.Peek();
            }
            return true;
        }
    }
}
