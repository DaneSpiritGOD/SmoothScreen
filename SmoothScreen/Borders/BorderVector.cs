using System;

namespace SmoothScreen.Borders
{
	public class BorderVector : IComparable<BorderVector>
	{
		public int X { get; }
		public int Y { get; }
		public double Angle { get; }

		BorderVector(int x, int y, double angle)
		{
			X = x;
			Y = y;
			Angle = angle;
		}

		public static readonly BorderVector TopUnit = new BorderVector(1, 0, 0);
		public static readonly BorderVector RightUnit = new BorderVector(0, 1, 90);
		public static readonly BorderVector BottomUnit = new BorderVector(-1, 0, 180);
		public static readonly BorderVector LeftUnit = new BorderVector(0, -1, 270);

		static int Dot(BorderVector vector1, BorderVector vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y;

		public int CompareTo(BorderVector other) => (int)(Angle - other.Angle);
	}
}
