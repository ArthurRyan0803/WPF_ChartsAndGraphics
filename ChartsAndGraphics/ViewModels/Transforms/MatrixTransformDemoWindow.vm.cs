using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism;
using Prism.Mvvm;
using System.ComponentModel;

namespace ChartsAndGraphics.ViewModels.Transforms
{
    public class MatrixTransformDemoWindowViewModel: BindableBase
    {
        private Matrix _matrix = Matrix.Identity;
        public Matrix Matrix
        {
            get => _matrix;
            set => SetProperty(ref _matrix, value);
        }

        public double M11
        {
            get => _matrix.M11;
            set 
            {
                _matrix.M11 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(M11)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Matrix)));
            }
        }

        public double M12
        {
            get => _matrix.M12;
            set
            {
                _matrix.M12 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(M12)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Matrix)));
            }
        }

        public double M21
        {
            get => _matrix.M21;
            set
            {
                _matrix.M21 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(M21)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Matrix)));
            }
        }

        public double M22
        {
            get => _matrix.M22;
            set
            {
                _matrix.M22 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(M22)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Matrix)));
            }
        }

        public double OffsetX
        {
            get => _matrix.OffsetX;
            set
            {
                _matrix.OffsetX = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(OffsetX)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Matrix)));
            }
        }

        public double OffsetY
        {
            get => _matrix.OffsetY;
            set
            {
                _matrix.OffsetY = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(OffsetY)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Matrix)));
            }
        }
    }
}
