using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithmVisualization.Algorithms
{
    class SelectSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        private T[] _data;
        private int i;
        private int j;
        private int min;
        public T[] Data => _data;

        public void Initialize(T[] data)
        {
            _data = new T[data.Length];
            data.CopyTo(_data, 0);
            i = 0;
            j = i + 1;
            min = i;
        }

        public bool Step(out int[] changed)
        {
            changed = null;
            if (i == _data.Length) return true;
            if (j == _data.Length)
            {
                if (min != i)
                {
                    var temp = _data[i];
                    _data[i] = _data[min];
                    _data[min] = temp;
                    changed = new[] { i, min };
                }
                i++;
                j = i + 1;
                min = i;
                return false;
            }
            while (j != _data.Length &&_data[j].CompareTo(_data[min]) != -1)
            {
                j++;
            }
            if (j != _data.Length)
            {
                min = j;
                changed = new[] { min, j };
            }
            return false;          
        }
    }
}
