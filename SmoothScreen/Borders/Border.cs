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
			EnsureConstructorParameters(screen, line);

			Owner = screen;
			Line = line;
		}

		// should not change state inside the overridden method
		protected abstract void EnsureConstructorParameters(Screener screen, Line line);

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

			protected override void EnsureConstructorParameters(Screener screen, Line line)
			{
				parent.EnsureConstructorParameters(screen, line);
				EnsureSubsetOfParent();
			}

			protected abstract void EnsureSubsetOfParent();

			protected abstract void EnsureSameAxisButNoOverlap(Border other);
			protected abstract int CompareToDifferentTypeCore(Border other);

			protected override int CompareToDifferentType(Border other)
			{
				EnsureSameAxisButNoOverlap(other);
				return CompareToDifferentTypeCore(other);
			}

#if DEBUG
			public override Border GetSegmentBorderForTest(Line line) => throw new NotSupportedException();
#endif
		}

#if DEBUG
		public abstract Border GetSegmentBorderForTest(Line line);
#endif
	}

	class TopBorder : Border
	{
		protected override int Order => 100;

		public TopBorder(Screener screen, Line line) : base(screen, line)
		{
		}

		protected override void EnsureConstructorParameters(Screener screen, Line line)
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

			protected override void EnsureSubsetOfParent()
			{
				if (parent.GetStartY() != this.GetStartY() ||
					parent.GetStartX() >= this.GetStartX() ||
					parent.GetEndX() <= this.GetEndX())
				{
					throw new SegmentBorderNotSubsetOfParentException();
				}
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

#if DEBUG
		public override Border GetSegmentBorderForTest(Line line) => new SegmentTopBorder(this, line);
#endif
	}

	class RightBorder : Border
	{
		protected override int Order => 200;

		public RightBorder(Screener screen, Line line) : base(screen, line)
		{
		}

		protected override void EnsureConstructorParameters(Screener screen, Line line)
		{
			if (line.Start.X != line.End.X || line.Start.Y >= line.End.Y)
			{
				throw new DistinctAxisBorderException();
			}
		}

		class SegmentRightBorder : SegmentBorder<RightBorder>
		{
			protected override int Order => 250;

			public SegmentRightBorder(RightBorder parent, Line line) : base(parent, line)
			{
			}

			protected override void EnsureSubsetOfParent()
			{
				if (parent.GetStartX() != this.GetStartX() ||
					parent.GetStartY() >= this.GetStartY() ||
					parent.GetEndY() <= this.GetEndY())
				{
					throw new SegmentBorderNotSubsetOfParentException();
				}
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

#if DEBUG
		public override Border GetSegmentBorderForTest(Line line) => new SegmentRightBorder(this, line);
#endif
	}

	class BottomBorder : Border
	{
		protected override int Order => 300;

		public BottomBorder(Screener screen, Line line) : base(screen, line)
		{
		}

		protected override void EnsureConstructorParameters(Screener screen, Line line)
		{
			if (line.Start.Y != line.End.Y || line.Start.X <= line.End.X)
			{
				throw new DistinctAxisBorderException();
			}
		}

		class SegmentBottomBorder : SegmentBorder<BottomBorder>
		{
			protected override int Order => 350;

			public SegmentBottomBorder(BottomBorder parent, Line line) : base(parent, line)
			{
			}

			protected override void EnsureSubsetOfParent()
			{
				if (parent.GetStartY() != this.GetStartY() ||
					parent.GetStartX() <= this.GetStartX() ||
					parent.GetEndX() >= this.GetEndX())
				{
					throw new SegmentBorderNotSubsetOfParentException();
				}
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

#if DEBUG
		public override Border GetSegmentBorderForTest(Line line) => new SegmentBottomBorder(this, line);
#endif
	}

	class LeftBorder : Border
	{
		protected override int Order => 400;

		public LeftBorder(Screener screen, Line line) : base(screen, line)
		{
		}

		protected override void EnsureConstructorParameters(Screener screen, Line line)
		{
			if (line.Start.X != line.End.X || line.Start.X >= line.End.X)
			{
				throw new DistinctAxisBorderException();
			}
		}

		class SegmentLeftBorder : SegmentBorder<LeftBorder>
		{
			protected override int Order => 450;

			public SegmentLeftBorder(LeftBorder parent, Line line) : base(parent, line)
			{
			}

			protected override void EnsureSubsetOfParent()
			{
				if (parent.GetStartX() != this.GetStartX() ||
					parent.GetStartY() <= this.GetStartY() ||
					parent.GetEndY() >= this.GetEndY())
				{
					throw new SegmentBorderNotSubsetOfParentException();
				}
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

#if DEBUG
		public override Border GetSegmentBorderForTest(Line line) => new SegmentLeftBorder(this, line);
#endif
	}

	class NoneBorder : Border
	{
		static readonly Line LineForNone = new Line(new Point(int.MinValue, int.MinValue), new Point(int.MinValue, int.MinValue));

		protected override int Order => int.MinValue;

		public NoneBorder(Screener screen) : base(screen, LineForNone)
		{
		}

		protected override void EnsureConstructorParameters(Screener screen, Line line) { }

#if DEBUG
		public override Border GetSegmentBorderForTest(Line line) => throw new NotSupportedException();
#endif
	}
}
