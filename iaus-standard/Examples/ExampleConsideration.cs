namespace InfiniteAxisUtility.Examples
{
    /// <summary>
    /// ExampleConsideration is an example of a consideration
    /// It takes two values `x` and `y` and normalizes the value by dividing `x` by `y`.
    /// </summary>
// ReSharper disable once UnusedType.Global
    public class ExampleConsideration : Consideration, IConsideration
    {
        private readonly double _x;
        private readonly double _y;

        public ExampleConsideration(double x, double y) : base("Example", ResponseCurve.Linear)
        {
            _x = x;
            _y = y;
        }

        public override double Calculate()
        {
            return Normalize(_x/_y);
        }
    }
}
