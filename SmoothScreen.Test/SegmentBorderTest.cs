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
			var border2 = CreateBorder(BorderVector.RightUnit, new Point(99, 0), 100);
			TestCompareException(border1, border2, "Same axis is required.");
		}

		[TestCase(0, 99, Description = "Orthometric")]
		[TestCase(10, 99, Description = "Other")]
		public void TestCompare_NotInSameLineException(int startX2, int startY2)
		{
			var border1 = CreateBorder(BorderVector.TopUnit, new Point(0, 0), 100);
			var border2 = CreateBorder(BorderVector.TopUnit, new Point(startX2, startY2), 10);
			TestCompareException(border1, border2, "Not in same line.");
		}
	}
}
