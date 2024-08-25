using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChartsAndGraphics.Views.Charts
{
    /// <summary>
    /// CurvesChart.xaml 的交互逻辑
    /// </summary>
    public partial class CurvesChart : UserControl, ICurvesCanvas
    {
        private static readonly double DEFAULT_MIN = -5;
        private static readonly double DEFAULT_MAX = 15;
        private static readonly string DEFAULT_TITLE = "Curve Chart";

        private static readonly Type THIS_TYPE = typeof(CurvesChart);

        public static readonly DependencyProperty MaxXProperty =
            DependencyProperty.Register(nameof(MaxX), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(DEFAULT_MAX, FrameworkPropertyMetadataOptions.AffectsRender, OnRangePropertyChanged)
            );

        public static readonly DependencyProperty MaxYProperty =
            DependencyProperty.Register(nameof(MaxY), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(DEFAULT_MAX, FrameworkPropertyMetadataOptions.AffectsRender, OnRangePropertyChanged)
            );

        public static readonly DependencyProperty MinXProperty =
            DependencyProperty.Register(nameof(MinX), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(DEFAULT_MIN, FrameworkPropertyMetadataOptions.AffectsRender, OnRangePropertyChanged)
            );

        public static readonly DependencyProperty MinYProperty =
            DependencyProperty.Register(nameof(MinY), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(DEFAULT_MIN, FrameworkPropertyMetadataOptions.AffectsRender, OnRangePropertyChanged)
            );

        public static readonly DependencyProperty CoordinateFrameProperty =
            DependencyProperty.Register(nameof(CoordinateFrame), typeof(FrameworkElement), THIS_TYPE,
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty TitleFontFamilyProperty =
            DependencyProperty.Register(nameof(TitleFontFamily), typeof(FontFamily), THIS_TYPE,
                new FrameworkPropertyMetadata(new FontFamily("Microsoft Yahei UI"), FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register(nameof(TitleFontSize), typeof(double), THIS_TYPE,
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), THIS_TYPE,
                new FrameworkPropertyMetadata(DEFAULT_TITLE, FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public event RoutedEventHandler? RangeRelatedPropertyChanged;
        public event RoutedEventHandler? ViewPortSizeChanged;

        private static void OnRangePropertyChanged(object obj, DependencyPropertyChangedEventArgs e)
        {
            var @this = (CurvesChart)obj;
            @this.RangeRelatedPropertyChanged?.Invoke(@this, new RoutedEventArgs());
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

        public FrameworkElement CoordinateFrame
        {
            get => (FrameworkElement)GetValue(CoordinateFrameProperty);
            set => SetValue(CoordinateFrameProperty, value);
        }

        public FontFamily TitleFontFamily
        {
            get => (FontFamily)GetValue(TitleFontFamilyProperty);
            set => SetValue(TitleFontFamilyProperty, value);
        }

        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public double ViewPortWidth => ActualWidth;

        public double ViewPortHeight => ActualHeight;

        public CurvesChart()
        {
            InitializeComponent();

            Loaded += CurvesChart_Loaded;
            Unloaded += CurvesChart_Unloaded;
        }

        private void CurvesChart_Unloaded(object sender, RoutedEventArgs e)
        {
            SizeChanged -= CurvesChart_SizeChanged;
        }

        private void CurvesChart_Loaded(object sender, RoutedEventArgs e)
        {
            SizeChanged += CurvesChart_SizeChanged;
        }

        private void CurvesChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewPortSizeChanged?.Invoke(this, new RoutedEventArgs());
        }
    }
}
