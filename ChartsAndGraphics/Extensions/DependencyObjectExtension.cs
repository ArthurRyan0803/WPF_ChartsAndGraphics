using ChartsAndGraphics.Views.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChartsAndGraphics.Extensions
{
    public static class DependencyObjectExtension
    {
        public static T GetParent<T>(this FrameworkElement element)
        {
            if (element.Parent is not T p)
                throw new InvalidOperationException($"The parent of {element.GetType().Name} is not {nameof(T)}");
            else
                return p;
        }
    }
}
