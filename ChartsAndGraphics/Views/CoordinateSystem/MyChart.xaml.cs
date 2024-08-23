using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ChartsAndGraphics.Views.CoordinateSystem
{
    /// <summary>
    /// MyChart.xaml 的交互逻辑
    /// </summary>
    public partial class MyChart : UserControl
    {
        public static readonly DependencyProperty MaxXProperty =
            DependencyProperty.Register(nameof(MaxX), typeof(double), typeof(MyChart), new PropertyMetadata(10));

        public static readonly DependencyProperty MaxYProperty =
            DependencyProperty.Register(nameof(MaxY), typeof(double), typeof(MyChart), new PropertyMetadata(10));

        public static readonly DependencyProperty MinXProperty =
            DependencyProperty.Register(nameof(MinX), typeof(double), typeof(MyChart), new PropertyMetadata(-10));

        public static readonly DependencyProperty MinYProperty =
            DependencyProperty.Register(nameof(MinY), typeof(double), typeof(MyChart), new PropertyMetadata(-10));

        public static readonly DependencyProperty GraphicsProperty =
            DependencyProperty.Register(
                nameof(MinY), typeof(double), typeof(MyChart), 
                new FrameworkPropertyMetadata(new List<Shape>(), FrameworkPropertyMetadataOptions.AffectsRender, OnGraphicPropertyChanged)
            );

        private static void OnGraphicPropertyChanged(object obj, DependencyPropertyChangedEventArgs e)
        {
            //var @this = (MyChart)obj;
            //@this.Canvas.Children.Clear();
            //var newGraphics = (IReadOnlyCollection<Shape>)e.NewValue;
            ////foreach(var g in newGraphics)
            ////{
            ////    g.GeometryTransform
            ////}
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

        public IReadOnlyCollection<Shape> Graphics
        {
            get => (IReadOnlyCollection<Shape>)GetValue(GraphicsProperty);
            set => SetValue(GraphicsProperty, value);
        }

        private int _zoomIndex;
        public int ZoomIndex
        {
            get => _zoomIndex;
            set
            {
                _zoomIndex = value;
                if (_zoomIndex < 0 || _zoomIndex >= ZoomRatios.Count)
                    throw new IndexOutOfRangeException($"{nameof(ZoomIndex)} out of range");

                CurrentZoomRatio = ZoomRatios[value];
            }
        }

        public static IReadOnlyList<double> ZoomRatios { get; } = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0, 1.25, 1.5, 1.75, 2.0];

        public double CurrentZoomRatio { get; private set; }

        private Vector2 _mouseDownCoordinate;
        private Vector2 _originCoordinate = new (0, 0);

        public MyChart()
        {
            InitializeComponent();
            ZoomIndex = 9;
            MouseWheel += MyChart_MouseWheel;
            MouseDown += MyChart_MouseDown;
        }

        private void MyChart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePos = e.GetPosition(Canvas);
                _mouseDownCoordinate = new((float)mousePos.X, (float)mousePos.Y);
            }
        }

        private void MyChart_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Delta < 0)
                    ZoomIn();
                else if (e.Delta > 0)
                    ZoomOut();
            }
        }

        private double RealXToPaintX(double x)
        {
            return (x - MinX) / (MaxX - MinX) * Canvas.Width;
        }

        private double RealYToPaintY(double y)
        {
            return Canvas.Height - (y - MinY) / (MaxY - MinY) * Canvas.Height;
        }

        private void ZoomIn()
        {
            if (ZoomIndex > 0)
                ZoomIndex--;
        }

        private void ZoomOut()
        {
            if (ZoomIndex < ZoomRatios.Count - 1)
                ZoomIndex++;
        }
    }
}
