using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChartsAndGraphics.Views.Charts
{
    public interface ICurvesCanvas
    {
        public double MinX { get; }
        public double MaxX { get; }
        public double MinY { get; }
        public double MaxY { get; }

        public double ViewPortWidth { get; }
        public double ViewPortHeight { get; }

        //FrameworkElement FrameworkElement { get; }
        public event RoutedEventHandler RangeRelatedPropertyChanged;
        public event RoutedEventHandler ViewPortSizeChanged;
    }
}
