using System.Drawing;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class SegmentBorderTest : BorderBaseTest<SegmentBorder>
	{
		[Test]
		public void TestCompare_RequireSameUnit()
		{
			var border1 = CreateBorder(BorderVector.TopUnit, new Point(0, 0), 100);
			var border2 = CreateBorder(BorderVector.RightUnit, new Point(99, 0), 50);
			TestCompareException(border1, border2, "Same unit is required.");
		}

		[TestCase(0, 49, Description = "Orthometric")]
		[TestCase(10, 49, Description = "Other")]
		public void TestCompare_NotInSameLine(int startX2, int startY2)
		{
			var border1 = CreateBorder(BorderVector.TopUnit, new Point(0, 0), 100);
			var border2 = CreateBorder(BorderVector.TopUnit, new Point(startX2, startY2), 10);
			TestCompareException(border1, border2, "Not in same line.");
		}

		[TestCase(10, 0, 10, 15, 0, 15)]
		[TestCase(10, 0, 10, 19, 0, 15)]
		[TestCase(10, 0, 10, 10, 0, 10)]
		public void TestCompare_Overlap(int startX1, int startY1, int length1, int startX2, int startY2, int length2)
		{
			var border1 = CreateBorder(BorderVector.TopUnit, new Point(startX1, startY1), length1);
			var border2 = CreateBorder(BorderVector.TopUnit, new Point(startX2, startY2), length2);

			TestCompareException(border1, border2, "Segment borders overlaps.");
			TestCompareException(border2, border1, "Segment borders overlaps.");
		}

		[TestCase(10, 0, 10, 20, 0, 15)]
		[TestCase(10, 0, 10, 21, 0, 15)]
		public void TestCompare_Correct(int startX1, int startY1, int length1, int startX2, int startY2, int length2)
		{
			var border1 = CreateBorder(BorderVector.TopUnit, new Point(startX1, startY1), length1);
			var border2 = CreateBorder(BorderVector.TopUnit, new Point(startX2, startY2), length2);

			Assert.That(border1.CompareTo(border2), Is.LessThan(0));
			Assert.That(border2.CompareTo(border1), Is.GreaterThan(0));
		}
	}
}
