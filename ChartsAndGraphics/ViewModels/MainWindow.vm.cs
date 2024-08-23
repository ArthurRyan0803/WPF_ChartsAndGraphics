using ChartsAndGraphics.Views.Animations;
using ChartsAndGraphics.Views.Charts;
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

        public ICommand OpenCurveChartWindowCommand { get; private set; }
        //public ICommand OpenDataCollectionChartWindowCommand { get; private set; }

        public MainWindowViewModel(IContainerProvider container)
        {
            _container = container;

            OpenScaleTransformDemoWindowCommand = new DelegateCommand(OpenScaleTransformDemoWindow);
            OpenMatrixTransformDemoWindowCommand = new DelegateCommand(OpenMatrixTransformDemoWindow);
            OpenRotateTransformDemoWindowCommand = new DelegateCommand(OpenRotateTransformDemoWindow);
            OpenSpinBallDemoWindowCommand = new DelegateCommand(OpenSpinBallWindow);
            OpenCurveChartWindowCommand = new DelegateCommand(OpenCurveChartWindow);
            //OpenDataCollectionChartWindowCommand = new DelegateCommand(OpenDataCollectionChartWindow);
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
            var window = _container.Resolve<SpinBallsWindow>();
            window.Show();
        }

        private void OpenCurveChartWindow()
        {
            var window = _container.Resolve<CurveChartWindow>();
            window.Show();
        }

        //private void OpenDataCollectionChartWindow()
        //{
        //    var window = _container.Resolve<DataCollectionChartWindow>();
        //    window.Show();
        //}
    }
}
