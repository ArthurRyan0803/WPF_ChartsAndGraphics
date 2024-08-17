using ChartsAndGraphics.Views.Animations;
using ChartsAndGraphics.Views.Transforms;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChartsAndGraphics.ViewModels
{
    public class MainWindowViewModel: Prism.Mvvm.BindableBase
    {
        private readonly IContainerProvider _container;

        public ICommand OpenScaleTransformDemoWindowCommand { get; private set; }
        public ICommand OpenMatrixTransformDemoWindowCommand { get; private set; }
        public ICommand OpenRotateTransformDemoWindowCommand { get; private set; }
        public ICommand OpenSpinBallDemoWindowCommand { get; private set; }
        
        public MainWindowViewModel(IContainerProvider container)
        {
            _container = container;

            OpenScaleTransformDemoWindowCommand = new DelegateCommand(OpenScaleTransformDemoWindow);
            OpenMatrixTransformDemoWindowCommand = new DelegateCommand(OpenMatrixTransformDemoWindow);
            OpenRotateTransformDemoWindowCommand = new DelegateCommand(OpenRotateTransformDemoWindow);
            OpenSpinBallDemoWindowCommand = new DelegateCommand(OpenSpinBallWindow);
        }

        private void OpenScaleTransformDemoWindow()
        {
            var window = _container.Resolve<ScaleTansformDemoWindow>();
            window.Show();
        }

        private void OpenMatrixTransformDemoWindow()
        {
            var window = _container.Resolve<MatrixTransformDemoWindow>();
            window.Show();
        }

        private void OpenRotateTransformDemoWindow()
        {
            var window = _container.Resolve<RotateTransformDemoWindow>();
            window.Show();
        }

        private void OpenSpinBallWindow()
        {
            var window = _container.Resolve<SpinBallWindow>();
            window.Show();
        }
    }
}
