using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithmVisualization.Algorithms
{
    public class BubbleSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        private int _i;
        private int _j;
        private T[] _data;
        public T[] Data => _data;
        public void Initialize(T[] data)
        {
            _data = new T[data.Length];
            data.CopyTo(_data, 0);
            _i = 0;
            _j = _data.Length - 1;
        }

        public bool Step()
        {
            if (_i == _data.Length - 1) return true;
            while (_data[_j].CompareTo(_data[_j - 1]) != -1)
            {
                _j--;
                if (_j == _i)
                {
                    _j = _data.Length - 1;
                    _i++;
                    if (_i == _data.Length - 1) return true;
                }
            }
            var temp = _data[_j - 1];
            _data[_j - 1] = _data[_j];
            _data[_j] = temp;
            return false;
        }
    }
}
