namespace InfiniteAxisUtility
{
	/// <summary>
	/// The IConsideration interface represents a piece of data used to determine if a behavior should happen
	/// </summary>
	public interface IConsideration
	{
		/// <summary>
		/// The name of the consideration (Hunger, Fatigue, ect)
		/// </summary>
		string Name
		{
			get;
			set;
		}

		/// <summary>
		/// The mathematical equation used to model the data
		/// </summary>
		ResponseCurve Curve
		{
			get;
			set;
		}

		/// <summary>
		/// Normalize calculates the score based on the curve type
		/// </summary>
		/// <returns>double between 0.0 and 1.0</returns>
		double Calculate();
	}

	/// <summary>
	/// The IConsideration interface represents a piece of data used to determine if a behavior should happen
	/// This class implements the IConsideration interface and provides a consistent structure to build on
	/// </summary>
	public abstract class Consideration: IConsideration
	{
		protected Consideration(string name, ResponseCurve curve)
		{
			Name = name;
			Curve = curve;
		}

		public string Name { get; set; }
		public ResponseCurve Curve { get; set; }

		public abstract double Calculate();

		/// <summary>
		/// Calculate calls the response curve's method to compute the value
		/// </summary>
		/// <param name="x">The input value to pass into the equation</param>
		/// <returns></returns>
		protected double Normalize(double x)
		{
			return Curve.ComputeValue(x);
		}
	}
}
