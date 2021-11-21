//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using SmoothScreen.Borders;

//namespace SmoothScreen
//{
//	abstract class BorderBase
//	{
//		protected readonly Screener screener;
//		public LineVector Unit { get; }
//		protected readonly Point startPoint;
//		public int Length { get; }

//		public BorderBase(Screener screener, LineVector unit, Point startPoint, int length)
//		{
//			if (!LineVector.IsUnit(unit))
//			{
//				throw new BorderException("Non-unit BorderVector is passed as unit.");
//			}
			
//			if (!screener.Contains(startPoint))
//			{
//				throw new BorderException("Start point is not in screen.");
//			}

//			var vector = (length - 1) * unit;
//			var endPointVector = new LineVector(startPoint) + vector;
//			var endPoint = new Point(endPointVector.X, endPointVector.Y);
//			if (!screener.Contains(endPoint))
//			{
//				throw new BorderException("End point is not in screen.");
//			}

//			this.screener = screener;
//			Unit = unit;
//			this.startPoint = startPoint;
//			Length = length;
//		}

//		public override bool Equals(object obj)
//			=> obj is BorderBase @base &&
//			EqualityComparer<Screener>.Default.Equals(screener, @base.screener) &&
//			EqualityComparer<LineVector>.Default.Equals(Unit, @base.Unit) &&
//			startPoint.Equals(@base.startPoint) &&
//			Length == @base.Length;

//		public override int GetHashCode() => HashCode.Combine(screener, Unit, startPoint, Length);

//		public override string ToString() => $"S{screener} U{Unit} P{startPoint} L{Length}";
//	}

//	class Border : BorderBase, IComparable<Border>
//	{
//		public Border(Screener screener, LineVector unit, Point location, int length) : base(screener, unit, location, length)
//		{
//		}

//		public BorderCollection<SegmentBorder> Segments { get; } = new BorderCollection<SegmentBorder>();

//		public int CompareTo(Border other)
//		{
//			if (!other.screener.Equals(screener))
//			{
//				throw new BorderException("Same screen is required.");
//			}

//			if (Unit.Equals(other.Unit))
//			{
//				throw new BorderException("Distinct unit is required.");
//			}

//			return Unit.CompareTo(other.Unit);
//		}

//		public static bool SegmentIfClingToEachOther(Border border1, Border border2)
//		{
//			var relation = LineVector.GetRelation(border1.Unit, border2.Unit);
//			if (relation != BorderVectorRelation.SameLineReverseDirection)
//			{
//				return false;
//			}

//			if (CalculateUnitDistance() != 1)
//			{
//				return false;
//			}

//			var startVector = new LineVector(border1.startPoint, border2.startPoint);
//			var angle = border1.Unit.Angle - startVector.Angle;
//			if (angle <=0 || angle >= 90)
//			{
//				return false;
//			}

//			if (LineVector.Dot(startVector, border1.Unit) >= (border1.Length + border2.Length))
//			{
//				return false;
//			}

//			return true;

//			int CalculateUnitDistance()
//			{
//				switch (border1.Unit)
//				{
//					case var top1 when top1.Equals(LineVector.TopUnit):
//					case var right1 when right1.Equals(LineVector.RightUnit):
//						return Math.Abs(border1.startPoint.Y - border2.startPoint.Y);
//					case var bottom1 when bottom1.Equals(LineVector.BottomUnit):
//					case var left1 when left1.Equals(LineVector.LeftUnit):
//						return Math.Abs(border1.startPoint.X - border2.startPoint.X);
//					default:
//						throw new NotSupportedException();
//				}
//			}
//		}
//	}

//	class SegmentBorder : BorderBase, IComparable<SegmentBorder>
//	{
//		public SegmentBorder(Screener screener, LineVector unit, Point startPoint, int length)
//			: base(screener, unit, startPoint, length)
//		{
//		}

//		public int CompareTo(SegmentBorder other)
//		{
//			if (!other.screener.Equals(screener))
//			{
//				throw new BorderException("Same screen is required.");
//			}

//			if (!Unit.Equals(other.Unit))
//			{
//				throw new BorderException("Same unit is required.");
//			}

//			var startVector = new LineVector(startPoint, other.startPoint);
//			if (startVector.IsZero)
//			{
//				throw new BorderException("Segment borders overlaps.");
//			}

//			var startVectorLength = startVector.Length();
//			var startRelation = LineVector.GetRelation(startVector, Unit);
//			switch (startRelation)
//			{
//				case BorderVectorRelation.SameLineSameDirection:
//					return startVectorLength >= Length ? -1 : throw new BorderException("Segment borders overlaps.");
//				case BorderVectorRelation.SameLineReverseDirection:
//					return startVectorLength >= other.Length ? 1 : throw new BorderException("Segment borders overlaps.");
//				default:
//					throw new BorderException("Not in same line.");
//			}
//		}
//	}
//}