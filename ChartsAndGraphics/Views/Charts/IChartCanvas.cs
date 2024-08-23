using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartsAndGraphics.Views.Charts
{
    public interface IChartCanvas
    {
        public double MinX { get; }
        public double MaxX { get; }
        public double MinY { get; }
        public double MaxY { get; }

        public double ViewportWidth { get; }
        public double ViewpotHeight { get; }
    }
}
