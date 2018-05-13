using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithmVisualization.Algorithms
{
    public class ShellSort<T> : ISortAlgorithm<T> where T : IComparable<T>
    {
        private T[] _data;
        private int dk;
        private int i;
        private int j;
        private bool flag;
        private T insert;
        public T[] Data => _data;

        public void Initialize(T[] data)
        {
            _data = new T[data.Length];
            data.CopyTo(_data, 0);
            dk = _data.Length / 2;
            i = dk;
            j = i - dk;
            flag = false;
        }

        public bool Step()
        {
            if (dk == 0) return true;
            if (!flag)
            {
                goto loop;
                init:
                {
                    dk = dk / 2;
                    if (dk == 0) return true;
                    i = dk;
                }
                loop:
                {
                    if (!(i < _data.Length)) goto init;
                    if (_data[i].CompareTo(_data[i - dk]) == -1) goto complete;
                    i++;
                    goto loop;
                }
                complete:
                {
                    flag = true;
                    j = i - dk;
                    insert = _data[i];
                }
            }
            if(j>=0 && insert.CompareTo(_data[j]) == -1)
            {
                _data[j + dk] = _data[j];
                j -= dk;
            }
            else
            {
                flag = false;
                _data[j + dk] = insert;
                i++;
            }
            return false;
        }
    }
}
