using System;

namespace InfiniteAxisUtility
{
	public class ResponseCurve
	{
		/// <summary>
		/// CurveType represents the different mathematical equations used for considerations
		/// </summary>
		public enum CurveType
		{
			/// <summary>
			/// CurveTypeUnknown represents an unset curve type, this is considered invalid
			/// </summary>
			// ReSharper disable once UnusedMember.Global
			Unknown = 0,

			/// <summary>
			/// CurveTypeLinear represents a linear curve
			/// </summary>
			Linear = 1,

			/// <summary>
			/// CurveTypePolynomial represents a polynomial curve
			/// </summary>
			Polynomial = 2,

			/// <summary>
			/// CurveTypeLogistic represents a logistic curve
			/// </summary>
			Logistic = 3,

			/// <summary>
			/// CurveTypeLogit represents a logit curve
			/// </summary>
			Logit = 4,

			/// <summary>
			/// Normal represents a normal curve
			/// </summary>
			Normal = 5,

			/// <summary>
			/// Sine represents a sine curve
			/// </summary>
			Sine = 6
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public readonly CurveType Type;
		// ReSharper disable once MemberCanBePrivate.Global
		public readonly double Slope;
		// ReSharper disable once MemberCanBePrivate.Global
		public readonly double Exponent;
		// ReSharper disable once MemberCanBePrivate.Global
		public readonly double XShift;
		// ReSharper disable once MemberCanBePrivate.Global
		public readonly double YShift;

		#region Curve Presets

		//Curve Presets based on https://github.com/apoch/curvature/blob/master/Widgets/EditWidgetCurvePresets.cs#L216

		/// <summary>
		/// Linear represents a upward slope
		/// </summary>
		// ReSharper disable once UnusedMember.Global
		public static readonly ResponseCurve Linear = new ResponseCurve(CurveType.Linear, 1.0, 0.0, 0.0, 0.0);

		/// <summary>
		/// Inverse Linear represents a downward slope
		/// </summary>
		// ReSharper disable once UnusedMember.Global
		public static readonly ResponseCurve Inverse_Linear = new ResponseCurve(ResponseCurve.CurveType.Polynomial, -1.0, 4.0, 0.0, 1.0);

		// TODO Review equations
		// public static readonly ResponseCurve Constant = new ResponseCurve(CurveType.Linear, 0.0, 0.0, 0.0, 0.5);
		// "Basic quadric lower left" = new ResponseCurve(CurveType.Polynomial, 1.0, 4.0, 1.0, 0.0);
		// "Basic quadric lower right" = new ResponseCurve(ResponseCurve.CurveType.Polynomial, 1.0, 4.0, 0.0, 0.0);
		// "Basic quadric upper left" = new ResponseCurve(ResponseCurve.CurveType.Polynomial, -1.0, 4.0, 1.0, 1.0);
		// "Basic quadric upper right" = new ResponseCurve(ResponseCurve.CurveType.Polynomial, -1.0, 4.0, 0.0, 1.0);
		// "Standard cooldown" = new ResponseCurve(ResponseCurve.CurveType.Polynomial, 1.0, 6.0, 0.0, 0.0);
		// "Standard runtime" = new ResponseCurve(ResponseCurve.CurveType.Polynomial, -1.0, 6.0, 0.0, 1.0);
		// "Basic logistic" = new ResponseCurve(ResponseCurve.CurveType.Logistic, 1.0, 1.0, 0.0, 0.0);
		// "Inverse logistic" = new ResponseCurve(ResponseCurve.CurveType.Logistic, -1.0, 1.0, 0.0, 1.0);
		// "Basic logit" = new ResponseCurve(ResponseCurve.CurveType.Logit, 1.0, 1.0, 0.0, 0.0);
		// "Inverse logit" = new ResponseCurve(ResponseCurve.CurveType.Logit, -1.0, 1.0, 0.0, 0.0);
		// "Basic bell curve" = new ResponseCurve(ResponseCurve.CurveType.Normal, 1.0, 1.0, 0.0, 0.0);
		// "Inverse bell curve" = new ResponseCurve(ResponseCurve.CurveType.Normal, -1.0, 1.0, 0.0, 1.0);
		// "Basic sine wave" = new ResponseCurve(ResponseCurve.CurveType.Sine, 1.0, 1.0, 0.0, 0.0);
		// "Inverse sine wave" = new ResponseCurve(ResponseCurve.CurveType.Sine, -1.0, 1.0, 0.0, 0.0);
		#endregion

		public ResponseCurve(CurveType type, double slope, double exponent, double xShift, double yShift)
		{
			Type = type;
			Slope = slope;
			Exponent = exponent;
			XShift = xShift;
			YShift = yShift;
		}

		/// <summary>
		/// ComputeValue applies the math equation for each curve type and applies the category bonus
		/// </summary>
		/// <param name="x">The value to apply the equation against</param>
		/// <returns>The score resulting from the calculation</returns>
		public virtual double ComputeValue(double x)
		{
			var score = 0.0;

			switch (Type) {
				case CurveType.Linear:
					score = Sanitize((Slope * (x - XShift)) + YShift);
					break;
				case CurveType.Polynomial:
					score = Sanitize((Slope * Math.Pow(x - XShift, Exponent)) + YShift);
					break;
				case CurveType.Logistic:
					score = Sanitize((Slope / (1 + Math.Exp(-10.0 * Exponent * (x - 0.5 - XShift)))) + YShift);
					break;
				case CurveType.Logit:
					score = Sanitize(Slope * Math.Log((x - XShift) / (1.0 - (x - XShift))) / 5.0 + 0.5 + YShift);
					break;
				case CurveType.Normal:
					score = Sanitize(Slope * Math.Exp(-30.0 * Exponent * (x - XShift - 0.5) * (x - XShift - 0.5)) + YShift);
					break;
				case CurveType.Sine:
					score = Sanitize(0.5 * Slope * Math.Sin(2.0 * Math.PI * (x - XShift)) + 0.5 + YShift);
					break;
			}

			return score;
		}

		/// <summary>
		/// Sanitize replaces any invalid values with their closest normalized value
		/// or returns the original value
		/// </summary>
		/// <param name="value"></param>
		/// <returns>double between 0.0 and 1.0</returns>
		protected virtual double Sanitize(double value) {
			if (double.IsInfinity(value))
			{
				return 0.0;
			}

			if (double.IsNaN(value))
			{
				return 0.0;
			}

			if (value < 0.0) {
				return 0.0;
			}

			if (value > 1.0)
			{
				return 1.0;
			}

			return value;
		}
    
	}
}
