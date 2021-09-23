using System;
using System.Drawing;
using SmoothScreen.Borders;

namespace SmoothScreen
{
	abstract class BorderBase : IComparable<BorderBase>
	{
		protected readonly Screener screener;
		protected readonly BorderVector vector;

		protected readonly Point startPoint;
		protected readonly Point endPoint;
		public BorderVector Unit { get; }
		public int Length { get; }

		public BorderBase(Screener screener, BorderVector unit, Point startPoint, int length)
		{
			if (!screener.Contains(startPoint))
			{
				throw new BorderException("Location is not in screen.");
			}

			if (length > screener.Width || length > screener.Height)
			{
				throw new BorderException("Length is over bound.");
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
			this.endPoint = endPoint;
			Length = length;
		}

		protected abstract int CompareToCore(BorderBase other);

		public int CompareTo(BorderBase other)
		{
			if (other.screener != screener)
			{
				throw new BorderException("Same screen is required.");
			}

			if (other.GetType() != GetType())
			{
				throw new BorderException("Border type is distinct.");
			}

			return CompareToCore(other);
		}
	}

	class Border : BorderBase, IComparable<Border>
	{
		public Border(Screener screener, BorderVector unit, Point location, int length) : base(screener, unit, location, length)
		{
		}

		public int CompareTo(Border other)
		{
			if (Unit.Equals(other.Unit))
			{
				throw new BorderException("Same axis is required.");
			}

			return Unit.CompareTo(other.Unit);
		}

		protected override int CompareToCore(BorderBase other)
		{
			return CompareTo((Border)other);
		}
	}

	class SegmentBorder : BorderBase, IComparable<SegmentBorder>
	{
		public SegmentBorder(Screener screener, BorderVector unit, Point location, int length) : base(screener, unit, location, length)
		{
		}

		public int CompareTo(SegmentBorder other)
		{
			if (!Unit.Equals(other.Unit))
			{
				throw new BorderException("Distinct axis.");
			}

			var startVector = new BorderVector(startPoint, other.startPoint);
			var startVectorLength = startVector.Length();
			var startRelation = BorderVector.GetRelation(startVector, Unit);
			switch (startRelation)
			{
				case BorderVectorRelation.SameLineSameDirection:
					return startVectorLength >= Length ? -1 : throw new BorderException("Segment borders overlaps.");
				case BorderVectorRelation.SameLineReverseDirection:
					return other.Length >= startVectorLength ? 1 : throw new BorderException("Segment borders overlaps.");
				case BorderVectorRelation.Orthometric:
				case BorderVectorRelation.Other:
				default:
					throw new BorderException("Not in same line.");
			}
		}

		protected override int CompareToCore(BorderBase other)
		{
			return CompareTo((SegmentBorder)other);
		}
	}
}