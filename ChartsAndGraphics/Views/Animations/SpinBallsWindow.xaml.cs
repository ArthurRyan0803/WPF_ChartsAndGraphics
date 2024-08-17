using System.Diagnostics;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace ChartsAndGraphics.Views.Animations
{
    public class PolarTransformProxy: DependencyObject
    {
        private static Type _typeOfThis = typeof(PolarTransformProxy);

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(nameof(Angle), typeof(double), _typeOfThis,
                new FrameworkPropertyMetadata(
                        0.0,
                        FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                        (obj, e) =>
                        {
                            var @this = ((PolarTransformProxy)obj);
                            @this.SetOffsetWithAngle((double)e.NewValue);
                        } 
                    )
            );

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), _typeOfThis,
                    new FrameworkPropertyMetadata(
                        0.0,
                        FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                        (obj, e) =>
                        {
                            var @this = ((PolarTransformProxy)obj);
                            @this.SetOffsetWithRadius((double)e.NewValue);
                        }
                    )
                );

        public double Angle
        {
            get => (double)GetValue(AngleProperty);  
            set => SetValue(AngleProperty, value);
        }

        public double Radius
        {
            get => (double)GetValue(RadiusProperty); 
            set => SetValue(RadiusProperty, value);
        }

        private MatrixTransform _transform;
        public MatrixTransform Transform { get => _transform; set => _transform = value; }

        public PolarTransformProxy() { _transform = new MatrixTransform(); }

        public PolarTransformProxy(MatrixTransform matrixTransform)
        {
            _transform = matrixTransform;
            _transform.Matrix = Matrix.Identity;
        }

        private void SetOffsetWithRadius(double radius)
        {
            var radian = Angle * Math.PI / 180;
            double x = Math.Cos(radian) * radius;
            double y = Math.Sin(radian) * radius;

            Matrix m = Matrix.Identity;
            m.OffsetX = x;
            m.OffsetY = y;
            _transform.Matrix = m;
        }

        private void SetOffsetWithAngle(double angle)
        {
            var radian = angle * Math.PI / 180;
            double x = Math.Cos(radian) * Radius;
            double y = Math.Sin(radian) * Radius;

            Matrix m = Matrix.Identity;
            m.OffsetX = x;
            m.OffsetY = y;
            _transform.Matrix = m;
        }

        private void SetOffset(double angle, double radius)
        {
            var radian = angle * Math.PI / 180;
            double x = Math.Cos(radian) * radius;
            double y = Math.Sin(radian) * radius;

            Matrix m = Matrix.Identity;
            m.OffsetX = x;
            m.OffsetY = y;
            _transform.Matrix = m;
        }
    }


    /// <summary>
    /// SpinBallWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SpinBallsWindow : Window
    {
        public SpinBallsWindow()
        {
            InitializeComponent();
        }
    }
}
