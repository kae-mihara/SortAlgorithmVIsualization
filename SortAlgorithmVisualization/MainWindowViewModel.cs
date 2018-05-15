using SortAlgorithmVisualization.Algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace SortAlgorithmVisualization
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            timer = new Timer();
            timer.Interval = TimeSpan.FromSeconds(0.01).Milliseconds;
            timer.Elapsed += Timer_Tick;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (!ticked || datas == null) return;
            var geos = new Geometry[sorts.Length];
            for (int i = 0; i < sorts.Length; ++i)
            {
                var data = datas[i];
                var geo = new StreamGeometry();
                var changed = changed_indexes[i];
                using (var context = geo.Open())
                {
                    for (int j = 0; j < data.Length; ++j)
                    {
                        if (changed != null && (j == changed[0] || j == changed[1]))
                        {
                            var x1 = j * 4 - 1;
                            var x2 = j * 4 + 1;
                            var y = data[j];
                            context.BeginFigure(new Point(x1, 0), true, true);
                            context.LineTo(new Point(x1, y), false, false);
                            context.LineTo(new Point(x2, y), false, false);
                            context.LineTo(new Point(x2, 0), false, false);
                        }
                        else
                        {
                            context.BeginFigure(new Point(j * 4, 0), false, false);
                            context.LineTo(new Point(j * 4, data[j]), true, false);
                        }
                    }
                }
                geo.Freeze();
                geos[i] = geo;
            }
            ticked = false;
            Geometries = geos;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (datas)
            {
                changed_indexes = new int[sorts.Length][];
                bool all_completed = true;
                for (int i = 0; i < sorts.Length; ++i)
                {
                    if (!(all_completed &= sorts[i].Step(out changed_indexes[i])))
                        datas[i] = sorts[i].Data;
                }
                if (all_completed) Stop();
                ticked = true;
            }
        }

        internal void Stop()
        {
            timer.Stop();
        }

        internal void Start()
        {
            if (!initialized)
                Initialize();
            timer.Start();
            initialized = false;
        }
        internal void Initialize()
        {
            var data = new int[100];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = 2 * (i + 1);
            }
            for (int i = 0; i < data.Length; ++i)
            {
                var shuffle = random.Next(0, 100);
                var temp = data[i];
                data[i] = data[shuffle];
                data[shuffle] = temp;
            }
            changed_indexes = new int[sorts.Length][];
            datas = new int[sorts.Length][];
            for (int i = 0; i < datas.Length; ++i)
            {
                datas[i] = data;
            }
            foreach (var sort in sorts)
            {
                sort.Initialize(data);
            }
            initialized = true;
            ticked = true;
        }
        private int[][] datas;
        private int[][] changed_indexes;
        private ISortAlgorithm<int>[] sorts = new ISortAlgorithm<int>[]
        {
            new BubbleSort<int>(),
            new QuickSort<int>(),
            new InsertSort<int>(),
            new ShellSort<int>(),
            new SelectSort<int>()
        };
        private bool ticked = false;
        private bool initialized = false;
        private Random random = new Random();
        private Timer timer;

        private Geometry[] _geos;
        public Geometry[] Geometries
        {
            get => _geos; set { _geos = value; OnPropertyChanged("Geometries"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
