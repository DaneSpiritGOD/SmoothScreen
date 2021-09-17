using System;
using System.Drawing;
using SmoothScreen.Borders;

namespace SmoothScreen
{
	internal abstract class Border : IComparable<Border>
	{
		protected abstract int Order { get; }
		
		public Screener Owner { get; }
		public Line Line { get; }

		protected Border(Screener screen, Line line)
		{
			Owner = screen;
			Line = line;
		}

		protected bool IsSameType(Border other) => other.Order == Order;

		protected virtual int CompareToSameType(Border other) => throw new NotSupportedException();
		protected virtual int CompareToDifferentType(Border other) => Order - other.Order;

		public int CompareTo(Border other)
		{
			if (!other.Owner.Equals(Owner))
			{
				throw new NotSameScreenException();
			}

			return IsSameType(other) ? CompareToSameType(other) : CompareToDifferentType(other);
		}

		protected abstract class SegmentBorder<TParent> : Border where TParent : Border
		{
			protected readonly TParent parent;

			protected SegmentBorder(TParent parent, Line line) : base(parent.Owner, line)
			{
				this.parent = parent;
			}

			protected abstract void EnsureSameAxisButNoOverlap(Border other);
			protected abstract int CompareToDifferentTypeCore(Border other);

			protected override int CompareToDifferentType(Border other)
			{
				EnsureSameAxisButNoOverlap(other);
				return CompareToDifferentTypeCore(other);
			}
		}
	}

	class TopBorder : Border
	{
		protected override int Order => 100;

		public TopBorder(Screener screen, Line line) : base(screen, line)
		{
			if (line.Start.Y != line.End.Y || line.Start.X >= line.End.X)
			{
				throw new DistinctAxisBorderException();
			}
		}

		class SegmentTopBorder : SegmentBorder<TopBorder>
		{
			protected override int Order => 150;

			public SegmentTopBorder(TopBorder parent, Line line) : base(parent, line)
			{
			}

			protected override void EnsureSameAxisButNoOverlap(Border other)
			{
				if (other.GetStartY() != this.GetStartY() ||
					other.GetStartX() <= this.GetEndX() ||
					other.GetEndX() >= this.GetStartX())
				{
					throw new OverlappedBorderException();
				}
			}

			protected override int CompareToDifferentTypeCore(Border other) => this.GetStartX() - other.GetStartX();
		}
	}

	class RightBorder : Border
	{
		protected override int Order => 200;

		public RightBorder(Screener screen, Line line) : base(screen, line)
		{
			if (line.Start.X != line.End.X || line.Start.Y >= line.End.Y)
			{
				throw new DistinctAxisBorderException();
			}
		}

		class SegmentRightBorder : SegmentBorder<TopBorder>
		{
			protected override int Order => 250;

			public SegmentRightBorder(TopBorder parent, Line line) : base(parent, line)
			{
			}

			protected override void EnsureSameAxisButNoOverlap(Border other)
			{
				if (other.GetStartX() != this.GetStartX() ||
					other.GetStartY() <= this.GetEndY() ||
					other.GetEndY() >= this.GetStartY())
				{
					throw new OverlappedBorderException();
				}
			}

			protected override int CompareToDifferentTypeCore(Border other) => this.GetStartY() - other.GetStartY();
		}
	}

	class BottomBorder : Border
	{
		protected override int Order => 300;

		public BottomBorder(Screener screen, Line line) : base(screen, line)
		{
			if (line.Start.Y != line.End.Y || line.Start.X <= line.End.X)
			{
				throw new DistinctAxisBorderException();
			}
		}

		class SegmentBottomBorder : SegmentBorder<TopBorder>
		{
			protected override int Order => 350;

			public SegmentBottomBorder(TopBorder parent, Line line) : base(parent, line)
			{
			}

			protected override void EnsureSameAxisButNoOverlap(Border other)
			{
				if (other.GetStartY() != this.GetStartY() ||
					other.GetStartX() >= this.GetEndX() ||
					other.GetEndX() <= this.GetStartX())
				{
					throw new OverlappedBorderException();
				}
			}

			protected override int CompareToDifferentTypeCore(Border other) => other.GetStartX() - this.GetStartX();
		}
	}

	class LeftBorder : Border
	{
		protected override int Order => 400;

		public LeftBorder(Screener screen, Line line) : base(screen, line)
		{
			if (line.Start.X != line.End.X || line.Start.X >= line.End.X)
			{
				throw new DistinctAxisBorderException();
			}
		}

		class SegmentLeftBorder : SegmentBorder<TopBorder>
		{
			protected override int Order => 450;

			public SegmentLeftBorder(TopBorder parent, Line line) : base(parent, line)
			{
			}

			protected override void EnsureSameAxisButNoOverlap(Border other)
			{
				if (other.GetStartY() != this.GetStartY() ||
					other.GetStartY() >= this.GetEndY() ||
					other.GetEndY() <= this.GetStartY())
				{
					throw new OverlappedBorderException();
				}
			}

			protected override int CompareToDifferentTypeCore(Border other) => other.GetStartY() - this.GetStartY();
		}
	}

	class NoneBorder : Border
	{
		static readonly Line LineForNone = new Line(new Point(int.MinValue, int.MinValue), new Point(int.MinValue, int.MinValue));

		protected override int Order => int.MinValue;

		public NoneBorder(Screener screen) : base(screen, LineForNone)
		{
		}
	}
}
