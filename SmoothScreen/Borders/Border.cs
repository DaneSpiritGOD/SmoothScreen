using System;
using System.Drawing;
using SmoothScreen.Borders;

namespace SmoothScreen
{
	internal abstract class Border : IComparable<Border>
	{
		protected abstract int Order { get; }
		
		public Screener Owner { get; }
		protected readonly Line line;

		protected Border(Screener screen, Line line)
		{
			Owner = screen;
			this.line = line;
		}

		protected bool IsSameType(Border other) => other.Order == Order;

		protected virtual int CompareToSameType(Border other) => throw new NotSupportedException();
		protected virtual int CompareToDifferentType(Border other) => other.Order - Order;

		public int CompareTo(Border other) => IsSameType(other) ? CompareToSameType(other) : CompareToDifferentType(other);

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
				//if (other.line)
			}

			protected override int CompareToDifferentTypeCore(Border other)
			{
			}
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
	}

	class LeftBorder : Border
	{
		protected override int Order => 0;

		public LeftBorder(Screener screen, Line line) : base(screen, line)
		{
			if (line.Start.X != line.End.X || line.Start.X >= line.End.X)
			{
				throw new DistinctAxisBorderException();
			}
		}
	}

	class NoneBorder : Border
	{
		static readonly Line LineForNone = new Line(new Point(0, 0), new Point(0, 0));

		protected override int Order => throw new NotSupportedException();

		public NoneBorder(Screener screen, Line line) : base(screen, LineForNone)
		{
		}
	}
}
