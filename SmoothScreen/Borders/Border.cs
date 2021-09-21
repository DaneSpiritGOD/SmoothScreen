using System;
using System.Drawing;
using System.Windows.Forms;
using SmoothScreen.Borders;

namespace SmoothScreen
{
	abstract class BorderBase : IComparable<BorderBase>
	{
		readonly Screener screener;

		public BorderBase(Screener screener, BorderVector unit, Point location, int length)
		{
			this.screener = screener;
			Unit = unit;
			Location = location;
			Length = length;
		}

		public BorderVector Unit { get; }
		public Point Location { get; }
		public int Length { get; }

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
			if (Unit == other.Unit)
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
			if (Unit != other.Unit)
			{
				throw new DistinctAxisBorderException();
			}

			//return Unit.CompareTo(other.Unit);
		}

		protected override int CompareToCore(BorderBase other)
		{
			return CompareTo((SegmentBorder)other);
		}
	}
}