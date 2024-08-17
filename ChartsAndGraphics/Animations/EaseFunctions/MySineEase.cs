namespace System.Windows.Media.Animation
{
    class MySineEase : EasingFunctionBase
    {
        protected override Freezable CreateInstanceCore()
        {
            return new MyCosineEase();
        }

        protected override double EaseInCore(double normalizedTime)
        {
            return 1 + Math.Sin(Math.PI * 0.5 * normalizedTime);
        }
    }
}
