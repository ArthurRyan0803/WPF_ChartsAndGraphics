using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChartsAndGraphics.Views.Charts
{
    /// <summary>
    /// CurvesChart.xaml 的交互逻辑
    /// </summary>
    public partial class CurvesChart : UserControl, IChartCanvas
    {
        private static readonly double DEFAULT_MIN = -5;
        private static readonly double DEFAULT_MAX = 15;

        public static readonly DependencyProperty MaxXProperty =
            DependencyProperty.Register(nameof(MaxX), typeof(double), typeof(CurvesChart),
                new FrameworkPropertyMetadata(DEFAULT_MAX, FrameworkPropertyMetadataOptions.AffectsRender, OnGraphicPropertyChanged)
            );

        public static readonly DependencyProperty MaxYProperty =
            DependencyProperty.Register(nameof(MaxY), typeof(double), typeof(CurvesChart),
                new FrameworkPropertyMetadata(DEFAULT_MAX, FrameworkPropertyMetadataOptions.AffectsRender, OnGraphicPropertyChanged)
            );

        public static readonly DependencyProperty MinXProperty =
            DependencyProperty.Register(nameof(MinX), typeof(double), typeof(CurvesChart),
                new FrameworkPropertyMetadata(DEFAULT_MIN, FrameworkPropertyMetadataOptions.AffectsRender, OnGraphicPropertyChanged)
            );

        public static readonly DependencyProperty MinYProperty =
            DependencyProperty.Register(nameof(MinY), typeof(double), typeof(CurvesChart),
                new FrameworkPropertyMetadata(DEFAULT_MIN, FrameworkPropertyMetadataOptions.AffectsRender, OnGraphicPropertyChanged)
            );

        public static readonly DependencyProperty CoordinateFrameProperty =
            DependencyProperty.Register(nameof(CoordinateFrame), typeof(ICoordinateFrame), typeof(CurvesChart),
                new FrameworkPropertyMetadata(new EmptyCoordinateFrame(), FrameworkPropertyMetadataOptions.AffectsRender, OnGraphicPropertyChanged)
            );

        private static void OnGraphicPropertyChanged(object obj, DependencyPropertyChangedEventArgs e)
        {
            //var @this = (ChartCanvasBase)obj;
            //foreach(var c in @this.Children)
            //{
            //    if (c is IAwareParentUiPropertyChangeded i)
            //        i.ParentUiPropertyChanged(e.Property.Name);
            //}
        }

        private static void OnCoordinateFrameChanged(object obj, DependencyPropertyChangedEventArgs e)
        {

        }

        public double MaxX
        {
            get => (double)GetValue(MaxXProperty);
            set => SetValue(MaxXProperty, value);
        }

        public double MinX
        {
            get => (double)GetValue(MinXProperty);
            set => SetValue(MinXProperty, value);
        }

        public double MaxY
        {
            get => (double)GetValue(MaxYProperty);
            set => SetValue(MaxYProperty, value);
        }

        public double MinY
        {
            get => (double)GetValue(MinYProperty);
            set => SetValue(MinYProperty, value);
        }

        public ICoordinateFrame CoordinateFrame
        {
            get => (ICoordinateFrame)GetValue(CoordinateFrameProperty);
            set => SetValue(CoordinateFrameProperty, value);
        }

        public double ViewportWidth => ActualWidth;

        public double ViewpotHeight => ActualHeight;

        public CurvesChart()
        {
            InitializeComponent();
            Loaded += (_, __) => CoordinateFrame.Refresh(this);
            SizeChanged += (_, __) => CoordinateFrame.Refresh(this);
        }

        private class EmptyCoordinateFrame : ICoordinateFrame
        {
            public Geometry FrameGeometry => new LineGeometry();

            public void Refresh(IChartCanvas canvas) { }
        }
    }
}
