using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SmoothScreen.Borders
{
	public struct BorderVector
	{
		public int X { get; }
		public int Y { get; }
		readonly double? angle;

		BorderVector(int x, int y, double angle) : this(x, y)
		{
			this.angle = angle;
		}

		public BorderVector(int x, int y)
		{
			X = x;
			Y = y;
			angle = null;
		}

		public BorderVector(Point point) : this(point.X, point.Y)
		{
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int operator *(BorderVector left, BorderVector right) => left.X * right.X + left.Y * right.Y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator *(BorderVector left, int right) => new BorderVector(left.X * right, left.Y * right);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator *(int left, BorderVector right) => right * left;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator +(BorderVector left, BorderVector right) => new BorderVector(left.X + right.X, left.Y + right.Y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator -(BorderVector left, BorderVector right) => new BorderVector(left.X - right.X, left.Y - right.Y);

		public static readonly BorderVector TopUnit = new BorderVector(1, 0, 0);
		public static readonly BorderVector RightUnit = new BorderVector(0, 1, 90);
		public static readonly BorderVector BottomUnit = new BorderVector(-1, 0, 180);
		public static readonly BorderVector LeftUnit = new BorderVector(0, -1, 270);

		public int CompareTo(BorderVector other)
		{
			var angle = this.angle - other.angle;
			return angle.HasValue? (int)angle.Value : throw new NotSupportedException();
		}

		public bool Equals(BorderVector other) => X == other.X && Y == other.Y && angle == other.angle;
	}
}
