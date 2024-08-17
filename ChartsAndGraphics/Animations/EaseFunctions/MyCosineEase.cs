namespace System.Windows.Media.Animation
{
    class MyCosineEase : EasingFunctionBase
    {
        protected override Freezable CreateInstanceCore()
        {
            return new MyCosineEase();
        }

        protected override double EaseInCore(double normalizedTime)
        {
            return Math.Cos(normalizedTime);
        }
    }
}
