using System.Windows;

namespace ChartsAndGraphics.Views.Charts
{
    class CoordinateNormalizeHelper
    {
        private ICurvesCanvas _canvas;

        public CoordinateNormalizeHelper(ICurvesCanvas canvas)
        {
            _canvas = canvas;
        }

        public double NormalizeX(double x)
        {
            return (x - _canvas.MinX) / (_canvas.MaxX - _canvas.MinX) * _canvas.ViewPortWidth;
        }

        public double NormalizeY(double y)
        {
            return _canvas.ViewPortHeight - (y - _canvas.MinY) / (_canvas.MaxY - _canvas.MinY) * _canvas.ViewPortHeight;
        }

        public Point NormalizePoint(Point point)
        {
            return new Point(NormalizeX(point.X), NormalizeY(point.Y));
        }
    }
}
