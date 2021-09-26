﻿using System;
using System.Drawing;
using SmoothScreen.Borders;

namespace SmoothScreen
{
	abstract class BorderBase
	{
		protected readonly Screener screener;
		protected readonly BorderVector vector;

		protected readonly Point startPoint;
		protected readonly Point endPoint;
		public BorderVector Unit { get; }
		public int Length { get; }

		public BorderBase(Screener screener, BorderVector unit, Point startPoint, int length)
		{
			switch (unit)
			{
				case var _ when unit.Equals(BorderVector.TopUnit):
				case var _ when unit.Equals(BorderVector.RightUnit):
				case var _ when unit.Equals(BorderVector.BottomUnit):
				case var _ when unit.Equals(BorderVector.LeftUnit):
					break;
				default:
					throw new BorderException("Non-unit BorderVector is passed as unit.");
			}
			
			if (!screener.Contains(startPoint))
			{
				throw new BorderException("Start point is not in screen.");
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
	}

	class Border : BorderBase, IComparable<Border>
	{
		public Border(Screener screener, BorderVector unit, Point location, int length) : base(screener, unit, location, length)
		{
		}

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
	}

	class SegmentBorder : BorderBase, IComparable<SegmentBorder>
	{
		public SegmentBorder(Screener screener, BorderVector unit, Point location, int length) : base(screener, unit, location, length)
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
	}
}