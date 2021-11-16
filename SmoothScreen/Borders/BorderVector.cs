using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace SmoothScreen.Borders
{
	public struct BorderVector
	{
		public int X { get; }
		public int Y { get; }
		public double Angle { get; }

		public static readonly BorderVector TopUnit = new BorderVector(1, 0);
		public static readonly BorderVector RightUnit = new BorderVector(0, 1);
		public static readonly BorderVector BottomUnit = new BorderVector(-1, 0);
		public static readonly BorderVector LeftUnit = new BorderVector(0, -1);

		public BorderVector(int x, int y)
		{
			X = x;
			Y = y;

			var angle = Math.Atan2(y, x);
			Angle = (angle < 0 ? angle + 2* Math.PI : angle) / Math.PI * 180d;
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

		internal bool IsZero => X == 0 && Y == 0;

		internal static BorderVectorRelation GetRelation(BorderVector vector1, BorderVector vector2)
		{
			if (vector1.IsZero || vector2.IsZero)
			{
				throw new NotSupportedException();
			}

			var dot = Dot(vector1, vector2);
			if (dot == 0)
			{
				return BorderVectorRelation.Orthometric;
			}

			var vectorLength1 = vector1.LengthSquared();
			var vectorLength2 = vector2.LengthSquared();
			var vectorLengthProduct = vectorLength1 * vectorLength2;

			var isSameLength = dot * dot == vectorLengthProduct;
			return dot switch
			{
				> 0 => isSameLength ? BorderVectorRelation.SameLineSameDirection : BorderVectorRelation.AcuteAngle,
				< 0 => isSameLength ? BorderVectorRelation.SameLineReverseDirection : BorderVectorRelation.ObtuseAngle,
				_ => throw new NotImplementedException(),
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly float Length() => MathF.Sqrt(Dot(this, this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly int LengthSquared() => Dot(this, this);

		public readonly int CompareTo(BorderVector other)
			=> (int)Math.Truncate((Angle - other.Angle) * 100);

		public static bool IsUnit(BorderVector vector)
			=> vector.Equals(TopUnit) ||
			vector.Equals(RightUnit) ||
			vector.Equals(BottomUnit) ||
			vector.Equals(LeftUnit);

		public readonly bool Equals(BorderVector other) => X == other.X && Y == other.Y;

		public override string ToString() => $"[{X}, {Y}, {Angle}]";
	}
}
