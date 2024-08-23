using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ChartsAndGraphics.Extensions;

namespace ChartsAndGraphics.Views.Charts
{
    //using Range = Tuple<double, double>;
    using PointD = System.Windows.Point;

    public class SimpleCoordinateFrame : ICoordinateFrame // Shape, IAwareParentUiPropertyChangeded
    {
        //private static readonly Range DefaultAxisRange = new(-5, 20);
        //private static readonly double DefaultMajorTickSpacing = 10;
        //private static readonly PointD DefaultOrigin = new (0,0);

        //public static readonly DependencyProperty XRangeProperty =
        //    DependencyProperty.Register(
        //        nameof(XRange), typeof(Range), typeof(CoordinateAxes),
        //        new FrameworkPropertyMetadata(DefaultAxisRange, FrameworkPropertyMetadataOptions.AffectsRender, UiDependencyPropertyChanged)
        //    );

        //public static readonly DependencyProperty YRangeProperty =
        //    DependencyProperty.Register(
        //        nameof(YRange), typeof(Range), typeof(CoordinateAxes),
        //        new FrameworkPropertyMetadata(DefaultAxisRange, FrameworkPropertyMetadataOptions.AffectsRender, UiDependencyPropertyChanged)
        //    );

        //public static readonly DependencyProperty OriginProperty =
        //    DependencyProperty.Register(
        //        nameof(Origin), typeof(PointD), typeof(CoordinateAxes),
        //        new FrameworkPropertyMetadata(DefaultOrigin, FrameworkPropertyMetadataOptions.AffectsRender, UiDependencyPropertyChanged)
        //    );

        //private static void UiDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((SimpleCoordinateFrame)d).Refresh();
        //}

        //public static readonly DependencyProperty MajorTickSpacingProperty =
        //    DependencyProperty.Register(
        //        nameof(MajorTickSpacing), typeof(double), typeof(CoordinateAxes),
        //        new FrameworkPropertyMetadata(DefaultMajorTickSpacing, FrameworkPropertyMetadataOptions.AffectsRender)
        //    );

        //public Range XRange { get => (Range)GetValue(XRangeProperty); set => SetValue(XRangeProperty, value); }

        //public Range YRange { get => (Range)GetValue(YRangeProperty); set => SetValue(YRangeProperty, value); }

        //public PointD Origin { get => (PointD)GetValue(OriginProperty); set => SetValue(OriginProperty, value); }

        //public double MajorTickSpacing { get => (double)GetValue(MajorTickSpacingProperty); set => SetValue(MajorTickSpacingProperty, value); }

        //protected override Geometry DefiningGeometry => _geometries;

        public Geometry FrameGeometry => _geometries;

        private GeometryGroup _geometries;
        private ArrowLine _xAxis, _yAxis;

        public SimpleCoordinateFrame()
        {
            //Loaded += CoordinateAxes_Loaded;
            //Unloaded += CoordinateAxes_Unloaded;
            _geometries = new GeometryGroup();

            _xAxis = new ArrowLine();
            _yAxis = new ArrowLine();
        }

        //private void CoordinateAxes_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var canvas = this.GetParent<ChartCanvasBase>();
        //    canvas.SizeChanged += Canvas_SizeChanged;
        //    Refresh();
        //}

        //private void CoordinateAxes_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    var canvas = this.GetParent<ChartCanvasBase>();
        //    canvas.SizeChanged -= Canvas_SizeChanged;
        //}

        //private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    Refresh();
        //}

        //void IAwareParentUiPropertyChangeded.ParentUiPropertyChanged(string _)
        //{
        //    Refresh();
        //}

        public void Refresh(IChartCanvas canvas)
        {
            var helper = new CoordinateNormalizeHelper(canvas);

            var _xAxisBegin = helper.NormalizePoint(new(canvas.MinX, 0));
            var _xAxisEnd = helper.NormalizePoint(new(canvas.MaxX, 0));
            var _yAxisBegin = helper.NormalizePoint(new(0, canvas.MinY));
            var _yAxisEnd = helper.NormalizePoint(new(0, canvas.MaxY));

            _xAxis.Update(_xAxisBegin, _xAxisEnd, 20, 20);
            _yAxis.Update(_yAxisBegin, _yAxisEnd, 20, 20);

            _geometries.Children.Clear();
            _xAxis.AddToGroup(_geometries);
            _yAxis.AddToGroup(_geometries);
        }

        class ArrowLine
        {
            public LineGeometry Line { get; }

            private PathFigure _arrowPathFigure;
            public PathGeometry ArrowPath { get; }

            public ArrowLine()
            {
                Line = new(new(0, 0), new(0, 0));
                _arrowPathFigure = new PathFigure();
                ArrowPath = new();
                ArrowPath.Figures = new();
            }

            public void Update(PointD start, PointD end, double arrowWidth, double arrowHeight)
            {
                Line.StartPoint = start;
                Line.EndPoint = end;

                var vec = end - start;
                var length = vec.Length;

                var vecUnit = vec;
                vecUnit.Normalize();
                var interPoint = start + vecUnit * (length - arrowHeight);

                Vector orthVec = new(vecUnit.Y, -vecUnit.X);
                orthVec.Normalize();

                var arrowPt1 = interPoint + orthVec * (arrowWidth / 2);
                var arrowPt3 = interPoint - orthVec * (arrowHeight / 2);

                _arrowPathFigure.StartPoint = arrowPt1;
                _arrowPathFigure.Segments.Clear();
                _arrowPathFigure.Segments.Add(new LineSegment(end, true));
                _arrowPathFigure.Segments.Add(new LineSegment(arrowPt3, true));

                ArrowPath.Figures.Clear();
                ArrowPath.Figures.Add(_arrowPathFigure);
            }

            public void AddToGroup(GeometryGroup group)
            {
                group.Children.Add(Line);
                group.Children.Add(ArrowPath);
            }
        }
    }
}
