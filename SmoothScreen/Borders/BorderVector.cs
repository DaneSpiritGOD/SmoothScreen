using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace SmoothScreen.Borders
{
	public struct BorderVector
	{
		public int X { get; }
		public int Y { get; }
		readonly double? angle;

		public static readonly BorderVector TopUnit = new BorderVector(1, 0, 0);
		public static readonly BorderVector RightUnit = new BorderVector(0, 1, 90);
		public static readonly BorderVector BottomUnit = new BorderVector(-1, 0, 180);
		public static readonly BorderVector LeftUnit = new BorderVector(0, -1, 270);

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

		public BorderVector(Point start, Point end) : this(end.X - start.X, end.Y - start.Y)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Dot(BorderVector left, BorderVector right) => left.X * right.X + left.Y * right.Y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator *(BorderVector left, int right) => new BorderVector(left.X * right, left.Y * right);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator *(int left, BorderVector right) => right * left;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator +(BorderVector left, BorderVector right) => new BorderVector(left.X + right.X, left.Y + right.Y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static BorderVector operator -(BorderVector left, BorderVector right) => new BorderVector(left.X - right.X, left.Y - right.Y);

		internal static BorderVectorRelation GetRelation(BorderVector vector1, BorderVector vector2)
		{
			var dot = Dot(vector1, vector2);
			if (dot == 0)
			{
				return BorderVectorRelation.Orthometric;
			}

			var vectorLength1 = vector1.LengthSquared();
			var vectorLength2 = vector2.LengthSquared();
			var vectorLengthProduct = vectorLength1 * vectorLength2;

			if (dot * dot != vectorLengthProduct)
			{
				return BorderVectorRelation.Other;
			}

			return dot switch
			{
				> 0 => BorderVectorRelation.SameLineSameDirection,
				< 0 => BorderVectorRelation.SameLineReverseDirection,
				_ => throw new NotImplementedException(),
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float Length()
		{
			var x = Dot(this, this);
			return MathF.Sqrt(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly int LengthSquared()
		{
			return Dot(this, this);
		}

		public readonly int CompareTo(BorderVector other)
		{
			var angle = this.angle - other.angle;
			return angle.HasValue? (int)angle.Value : throw new NotSupportedException();
		}

		public readonly bool Equals(BorderVector other) => X == other.X && Y == other.Y && angle == other.angle;
	}
}
