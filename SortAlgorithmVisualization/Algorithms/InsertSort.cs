using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithmVisualization.Algorithms
{
    public class InsertSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        private int i;
        private int j;
        private T insert;
        private bool flag = false;
        private T[] _data;
        public T[] Data => _data;

        public void Initialize(T[] data)
        {
            _data = new T[data.Length];
            data.CopyTo(_data, 0);
            i = 1;
            j = i - 1;
        }

        public bool Step()
        {
            if (i == _data.Length) return true;
            if (!flag)
            {
                while(i < _data.Length)
                {
                    if (_data[i].CompareTo(_data[i - 1]) == -1) break;
                    i++;
                }
                if (i == _data.Length) return true;
                flag = true;
                insert = _data[i];
                j = i - 1;
            }
            if (j >= 0 && insert.CompareTo(_data[j]) == -1)
            {
                _data[j + 1] = _data[j];
                j--;
            }
            else
            {
                flag = false;
                _data[j + 1] = insert;
            }
            return false;
        }
    }
}
