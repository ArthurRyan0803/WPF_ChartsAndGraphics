using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChartsAndGraphics.Views.Charts
{
    /// <summary>
    /// BasicCoordinateFrame.xaml 的交互逻辑
    /// </summary>
    public partial class BasicCoordinateFrame : UserControl/*, ICoordinateFrame*/
    {
        private static readonly double DEFAULT_ARROW_HEIGHT = 10;
        private static readonly double DEFAULT_ARROW_WIDTH = 10;
        private static readonly Type THIS_TYPE = typeof(BasicCoordinateFrame);

        public static readonly DependencyProperty AxisArrowWidthProperty =
            DependencyProperty.Register(nameof(AxisArrowWidth), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(DEFAULT_ARROW_WIDTH, FrameworkPropertyMetadataOptions.AffectsRender, OnAxesGraphicsPropertyChanged)
            );

        public static readonly DependencyProperty AxisArrowHeightProperty =
            DependencyProperty.Register(nameof(AxisArrowHeight), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(DEFAULT_ARROW_HEIGHT, FrameworkPropertyMetadataOptions.AffectsRender, OnAxesGraphicsPropertyChanged)
            );

        public static readonly DependencyProperty XAxisLabelProperty =
            DependencyProperty.Register(nameof(XAxisLabel), typeof(string), THIS_TYPE,
                new FrameworkPropertyMetadata("X", FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty YAxisLabelProperty =
            DependencyProperty.Register(nameof(YAxisLabel), typeof(string), THIS_TYPE,
                new FrameworkPropertyMetadata("Y", FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty OriginLabelProperty =
            DependencyProperty.Register(nameof(OriginLabel), typeof(string), THIS_TYPE,
                new FrameworkPropertyMetadata("O", FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty LabelFontFamilyProperty =
            DependencyProperty.Register(nameof(LabelFontFamily), typeof(FontFamily), THIS_TYPE,
                new FrameworkPropertyMetadata(
                    new FontFamily("Microsoft Yahei UI"), FrameworkPropertyMetadataOptions.AffectsRender
                )
            );

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register(nameof(LabelFontSize), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Brush), THIS_TYPE,
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(nameof(StrokeThickness), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty AxesGeometryProperty =
            DependencyProperty.Register(nameof(AxesGeometry), typeof(Geometry), THIS_TYPE,
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
            );

        private static void OnAxesGraphicsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var @this = (BasicCoordinateFrame)d;
            if (@this.CurvesCanvas != null)
                @this.Refresh();
        }

        public double AxisArrowWidth
        {
            get => (double)GetValue(AxisArrowWidthProperty);
            set => SetValue(AxisArrowWidthProperty, value);
        }

        public double AxisArrowHeight
        {
            get => (double)GetValue(AxisArrowHeightProperty);
            set => SetValue(AxisArrowHeightProperty, value);
        }

        public string XAxisLabel
        {
            get => (string)GetValue(XAxisLabelProperty);
            set => SetValue(XAxisLabelProperty, value);
        }

        public string YAxisLabel
        {
            get => (string)GetValue(YAxisLabelProperty);
            set => SetValue(YAxisLabelProperty, value);
        }

        public string OriginLabel
        {
            get => (string)GetValue(OriginLabelProperty);
            set => SetValue(OriginLabelProperty, value);
        }

        public FontFamily LabelFontFamily
        {
            get => (FontFamily)GetValue(LabelFontFamilyProperty);
            set => SetValue(LabelFontFamilyProperty, value);
        }

        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public Geometry AxesGeometry
        {
            get => (Geometry)GetValue(AxesGeometryProperty);
            set => SetValue(AxesGeometryProperty, value);
        }

        private ICurvesCanvas? CurvesCanvas;
        private readonly ArrowLine _xAxis, _yAxis;
        private readonly GeometryGroup _axesGeometries;
        private readonly GeometryGroup _labelsGeometries;
        private bool AxesGeometryExists => CurvesCanvas != null;
        private readonly double AXIS_LABEL_X_OFFSET = 3;

        public BasicCoordinateFrame()
        {
            InitializeComponent();

            _axesGeometries = new GeometryGroup();
            _labelsGeometries = new GeometryGroup();

            _xAxis = new ArrowLine();
            _yAxis = new ArrowLine();

            xLabel.SizeChanged += (_, __) => UpdateAxisLabelPosition(_xAxis.ActualEndPoint, xLabel);
            yLabel.SizeChanged += (_, __) => UpdateAxisLabelPosition(_yAxis.ActualEndPoint, yLabel);
            originLabel.SizeChanged += (_, __) =>
            {
                if (CurvesCanvas != null)
                    UpdateAxisLabelPosition(new CoordinateNormalizeHelper(CurvesCanvas).NormalizePoint(new(0, 0)), originLabel);
            };

            Loaded += BasicCoordinateFrame_Loaded;
            Unloaded += BasicCoordinateFrame_Unloaded;
        }

        private void BasicCoordinateFrame_Unloaded(object sender, RoutedEventArgs e)
        {
            if (CurvesCanvas == null)
                throw new InvalidOperationException($"Why {nameof(CurvesCanvas)} is null?");

            CurvesCanvas.ViewPortSizeChanged -= OnViewPortSizeChanged;
        }

        private void BasicCoordinateFrame_Loaded(object sender, RoutedEventArgs e)
        {
            DependencyObject obj = this;
            DependencyObject? parent = null;
            while(true)
            {
                parent = VisualTreeHelper.GetParent(obj);
                if (parent == null)
                    throw new InvalidOperationException($"Canot find parent which is derived from {nameof(ICurvesCanvas)}!");

                if (parent is ICurvesCanvas curvesCanvas)
                {
                    CurvesCanvas = curvesCanvas;
                    CurvesCanvas.ViewPortSizeChanged += OnViewPortSizeChanged;
                    Refresh();
                    break;
                }
                else
                    obj = parent;
            }
        }

        private void OnViewPortSizeChanged(object _, RoutedEventArgs e)
        {
            Refresh();
        }

        private void RefreshXAxisLablePosition() => UpdateAxisLabelPosition(_xAxis.ActualEndPoint, xLabel);
        private void RefreshYAxisLabelPosition() => UpdateAxisLabelPosition(_yAxis.ActualEndPoint, yLabel);
        private void RefreshOriginLabelPosition(ICurvesCanvas targetCanvas)
        {
            if (AxesGeometryExists)
                UpdateAxisLabelPosition(new CoordinateNormalizeHelper(targetCanvas).NormalizePoint(new(0, 0)), originLabel);
        }
            
        private void RefreshAxesGeometry(ICurvesCanvas targetCanvas)
        {
            var helper = new CoordinateNormalizeHelper(targetCanvas);

            var _xAxisBegin = helper.NormalizePoint(new(targetCanvas.MinX, 0));
            var _xAxisEnd = helper.NormalizePoint(new(targetCanvas.MaxX, 0));
            var _yAxisBegin = helper.NormalizePoint(new(0, targetCanvas.MinY));
            var _yAxisEnd = helper.NormalizePoint(new(0, targetCanvas.MaxY));

            _xAxis.Update(_xAxisBegin, _xAxisEnd, AxisArrowWidth, AxisArrowHeight);
            _yAxis.Update(_yAxisBegin, _yAxisEnd, AxisArrowWidth, AxisArrowHeight);

            _axesGeometries.Children.Clear();
            _xAxis.AddToGroup(_axesGeometries);
            _yAxis.AddToGroup(_axesGeometries);

            AxesGeometry = _axesGeometries;
        }

        private void UpdateAxisLabelPosition(Point endpoint, TextBlock textblock)
        {
            textblock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            double left = endpoint.X - textblock.DesiredSize.Width - AXIS_LABEL_X_OFFSET;
            double top = endpoint.Y;

            Canvas.SetTop(textblock, top);
            Canvas.SetLeft(textblock, left);
        }

        private void Refresh()
        {
            if (CurvesCanvas == null)
                return;

            RefreshAxesGeometry(CurvesCanvas);

            RefreshXAxisLablePosition();
            RefreshYAxisLabelPosition();
            RefreshOriginLabelPosition(CurvesCanvas);
        }

        class ArrowLine
        {
            private readonly LineGeometry _line;
            private readonly PathFigure _arrowPathFigure;
            private readonly PathGeometry _arrowPath;

            public Point ActualEndPoint { get; private set; }

            public ArrowLine()
            {
                _line = new(new(0, 0), new(0, 0));
                _arrowPathFigure = new();
                _arrowPath = new()
                {
                    Figures = []
                };
            }

            public void Update(Point start, Point end, double arrowWidth, double arrowHeight)
            {
                _line.StartPoint = start;
                _line.EndPoint = end;

                var vec = end - start;
                var length = vec.Length;

                var vecUnit = vec;
                vecUnit.Normalize();
                ActualEndPoint = start + vecUnit * (length - arrowHeight);

                Vector orthVec = new(vecUnit.Y, -vecUnit.X);
                orthVec.Normalize();

                var arrowPt1 = ActualEndPoint + orthVec * (arrowWidth / 2);
                var arrowPt3 = ActualEndPoint - orthVec * (arrowWidth / 2);

                _arrowPathFigure.StartPoint = arrowPt1;
                _arrowPathFigure.Segments.Clear();
                _arrowPathFigure.Segments.Add(new LineSegment(end, true));
                _arrowPathFigure.Segments.Add(new LineSegment(arrowPt3, true));

                _arrowPath.Figures.Clear();
                _arrowPath.Figures.Add(_arrowPathFigure);
            }

            public void AddToGroup(GeometryGroup group)
            {
                group.Children.Add(_line);
                group.Children.Add(_arrowPath);
            }
        }
    }
}
