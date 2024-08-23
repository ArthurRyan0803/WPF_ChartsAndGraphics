using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChartsAndGraphics.Views.Charts
{
    public interface ICoordinateFrame
    {
        public Geometry FrameGeometry { get; }

        public void Refresh(IChartCanvas canvas);
    }
}
