using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithmVisualization
{
    interface ISortAlgorithm<T> where T : IComparable<T>
    {
        T[] Data { get; }
        void Initialize(T[] data);
        bool Step();
    }
}
