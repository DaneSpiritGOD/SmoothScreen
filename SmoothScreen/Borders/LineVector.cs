using System;
using System.Drawing;

namespace SmoothScreen.Borders
{
	public struct LineVector
	{
		public readonly Point Start { get; }
		public readonly Point End { get; }

		public readonly int X => End.X - Start.X;
		public readonly int Y => End.Y - Start.Y;

		double? angle;
		public double Angle
		{
			get
			{
				if (!angle.HasValue)
				{
					var angleV = Math.Atan2(Y, X);
					angle = (angleV < 0 ? angleV + 2 * Math.PI : angleV) / Math.PI * 180d;
				}
				return angle.Value;
			}
		}

		public bool IsZero => Start.Equals(End);

		public LineVector(Point point) : this(Point.Empty, point)
		{
		}

		public LineVector(Point start, Point end)
		{
			Start = start;
			End = end;
			angle = default;
		}
	}
}
