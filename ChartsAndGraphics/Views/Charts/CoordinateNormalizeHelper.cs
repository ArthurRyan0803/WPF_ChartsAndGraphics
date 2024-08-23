using System.Windows;

namespace ChartsAndGraphics.Views.Charts
{
    class CoordinateNormalizeHelper
    {
        private IChartCanvas _canvas;

        public CoordinateNormalizeHelper(IChartCanvas canvas)
        {
            _canvas = canvas;
        }

        public double NormalizeX(double x)
        {
            return (x - _canvas.MinX) / (_canvas.MaxX - _canvas.MinX) * _canvas.ViewportWidth;
        }

        public double NormalizeY(double y)
        {
            return _canvas.ViewpotHeight - (y - _canvas.MinY) / (_canvas.MaxY - _canvas.MinY) * _canvas.ViewpotHeight;
        }

        public Point NormalizePoint(Point point)
        {
            return new Point(NormalizeX(point.X), NormalizeY(point.Y));
        }
    }
}
