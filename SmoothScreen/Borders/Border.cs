using System;
using System.Drawing;
using SmoothScreen.Borders;

namespace SmoothScreen
{
	abstract class BorderBase : IComparable<BorderBase>
	{
		protected readonly Screener screener;
		protected readonly BorderVector vector;

		public BorderVector Unit { get; }
		public Point StartPoint { get; }
		readonly Point endPoint;
		public int Length { get; }

		public BorderBase(Screener screener, BorderVector unit, Point startPoint, int length)
		{
			if (!screener.Contains(startPoint))
			{
				throw new BorderException("location is not in screen");
			}

			if (length > screener.Width || length > screener.Height)
			{
				throw new BorderException("length is over bound");
			}

			var vector = (length - 1) * unit;
			var endPointVector = new BorderVector(startPoint) + vector;
			var endPoint = new Point(endPointVector.X, endPointVector.Y);
			if (!screener.Contains(endPoint))
			{
				throw new BorderException("end point is not in screen");
			}

			this.screener = screener;
			Unit = unit;
			StartPoint = startPoint;
			this.endPoint = endPoint;
			Length = length;
		}

		protected abstract int CompareToCore(BorderBase other);

		public int CompareTo(BorderBase other)
		{
			if (other.screener != screener)
			{
				throw new NotSameScreenException();
			}

			if (other.GetType() != GetType())
			{
				throw new NotSameKindOfBorderException();
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
				throw new SameAxisBorderException();
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
				throw new DistinctAxisBorderException();
			}
			
		}

		protected override int CompareToCore(BorderBase other)
		{
			return CompareTo((SegmentBorder)other);
		}
	}
}