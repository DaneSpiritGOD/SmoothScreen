using System;
using System.Collections.Generic;
using System.Drawing;
using SmoothScreen.Borders;

namespace SmoothScreen
{
	abstract class BorderBase
	{
		protected readonly Screener screener;
		public BorderVector Unit { get; }
		protected readonly Point startPoint;
		public int Length { get; }

		public BorderBase(Screener screener, BorderVector unit, Point startPoint, int length)
		{
			if (BorderVector.IsAxis(unit))
			{
				throw new BorderException("Non-unit BorderVector is passed as unit.");
			}
			
			if (!screener.Contains(startPoint))
			{
				throw new BorderException("Start point is not in screen.");
			}

			var vector = (length - 1) * unit;
			var endPointVector = new BorderVector(startPoint) + vector;
			var endPoint = new Point(endPointVector.X, endPointVector.Y);
			if (!screener.Contains(endPoint))
			{
				throw new BorderException("End point is not in screen.");
			}

			this.screener = screener;
			Unit = unit;
			this.startPoint = startPoint;
			Length = length;
		}

		public override bool Equals(object obj)
			=> obj is BorderBase @base &&
			EqualityComparer<Screener>.Default.Equals(screener, @base.screener) &&
			EqualityComparer<BorderVector>.Default.Equals(Unit, @base.Unit) &&
			startPoint.Equals(@base.startPoint) &&
			Length == @base.Length;

		public override int GetHashCode() => HashCode.Combine(screener, Unit, startPoint, Length);

		public override string ToString() => $"S{screener} U{Unit} P{startPoint} L{Length}";
	}

	class Border : BorderBase, IComparable<Border>
	{
		public Border(Screener screener, BorderVector unit, Point location, int length) : base(screener, unit, location, length)
		{
		}

		public BorderCollection<SegmentBorder> Segments { get; } = new BorderCollection<SegmentBorder>();

		public int CompareTo(Border other)
		{
			if (!other.screener.Equals(screener))
			{
				throw new BorderException("Same screen is required.");
			}

			if (Unit.Equals(other.Unit))
			{
				throw new BorderException("Distinct axis is required.");
			}

			return Unit.CompareTo(other.Unit);
		}

		public static bool SegmentIfClingToEachOther(Border border1, Border border2)
		{
			var relation = BorderVector.GetRelation(border1.Unit, border2.Unit);
			if (relation != BorderVectorRelation.SameLineReverseDirection)
			{
				return false;
			}

			var startVector = new BorderVector(border1.startPoint, border2.startPoint);
			if (startVector.IsZero)
			{
				return false;
			}

			var startRelation = BorderVector.GetRelation(startVector, border1.Unit);
			if (startRelation != BorderVectorRelation.AcuteAngle)
			{
				return false;
			}

			if (startVector.Length() >= (border1.Length + border2.Length))
			{
				return false;
			}

			return true;
		}
	}

	class SegmentBorder : BorderBase, IComparable<SegmentBorder>
	{
		public SegmentBorder(Screener screener, BorderVector unit, Point startPoint, int length)
			: base(screener, unit, startPoint, length)
		{
		}

		public int CompareTo(SegmentBorder other)
		{
			if (!other.screener.Equals(screener))
			{
				throw new BorderException("Same screen is required.");
			}

			if (!Unit.Equals(other.Unit))
			{
				throw new BorderException("Same axis is required.");
			}

			var startVector = new BorderVector(startPoint, other.startPoint);
			if (startVector.IsZero)
			{
				throw new BorderException("Segment borders overlaps.");
			}

			var startVectorLength = startVector.Length();
			var startRelation = BorderVector.GetRelation(startVector, Unit);
			switch (startRelation)
			{
				case BorderVectorRelation.SameLineSameDirection:
					return startVectorLength >= Length ? -1 : throw new BorderException("Segment borders overlaps.");
				case BorderVectorRelation.SameLineReverseDirection:
					return startVectorLength >= other.Length ? 1 : throw new BorderException("Segment borders overlaps.");
				default:
					throw new BorderException("Not in same line.");
			}
		}
	}
}