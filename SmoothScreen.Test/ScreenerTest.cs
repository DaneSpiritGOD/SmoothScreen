using System;
using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class ScreenerTest
	{
		[TestCase(0, 0, "Left")]
		[TestCase(4, 0, "Top")]
		[TestCase(0, 4, "Left")]
		[TestCase(4, 4, "Left")]
		[TestCase(1, 3, "Left")]
		[TestCase(3, 1, "Top")]
		[TestCase(3, 3, "Left")]
		public void TestGetCloserBorder_TopLeft(int x, int y, string expectedBorder)
		{
			TestGetCloserBordertCore(x, y, expectedBorder, () => screener.topLeftRect);
		}

		[TestCase(5, 0, "Top")]
		[TestCase(94, 0, "Top")]
		[TestCase(5, 4, "Top")]
		[TestCase(94, 4, "Top")]
		[TestCase(40, 3, "Top")]
		public void TestGetCloserBorder_Top(int x, int y, string expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.topRect.Contains(point), Is.True);
			StringAssert.Contains(expectedBorder, screener.GetCloserBorder(point).GetType().Name);
		}

		[TestCase(95, 0, "Top")]
		[TestCase(99, 0, "Right")]
		[TestCase(95, 4, "Right")]
		[TestCase(99, 4, "Right")]
		[TestCase(97, 1, "Top")]
		[TestCase(98, 2, "Right")]
		[TestCase(98, 1, "Right")]
		public void TestGetCloserBorder_TopRight(int x, int y, string expectedBorder)
		{
			TestGetCloserBordertCore(x, y, expectedBorder, () => screener.topRightRect);
		}

		[TestCase(95, 5, "Right")]
		[TestCase(99, 5, "Right")]
		[TestCase(95, 94, "Right")]
		[TestCase(99, 94, "Right")]
		[TestCase(97, 40, "Right")]
		public void TestGetCloserBorder_Right(int x, int y, string expectedBorder)
		{
			TestGetCloserBordertCore(x, y, expectedBorder, () => screener.rightRect);
		}

		[TestCase(95, 95, "Right")]
		[TestCase(99, 95, "Right")]
		[TestCase(99, 99, "Right")]
		[TestCase(95, 99, "Bottom")]
		[TestCase(98, 97, "Right")]
		[TestCase(97, 98, "Bottom")]
		[TestCase(98, 98, "Right")]
		public void TestGetCloserBorder_BottomRight(int x, int y, string expectedBorder)
		{
			TestGetCloserBordertCore(x, y, expectedBorder, () => screener.bottomRightRect);
		}

		[TestCase(5, 95, "Bottom")]
		[TestCase(94, 95, "Bottom")]
		[TestCase(5, 99, "Bottom")]
		[TestCase(94, 99, "Bottom")]
		[TestCase(40, 97, "Bottom")]
		public void TestGetCloserBorder_Bottom(int x, int y, string expectedBorder)
		{
			TestGetCloserBordertCore(x, y, expectedBorder, () => screener.bottomRect);
		}

		[TestCase(0, 95, "Left")]
		[TestCase(4, 95, "Left")]
		[TestCase(0, 99, "Left")]
		[TestCase(4, 99, "Bottom")]
		[TestCase(3, 97, "Bottom")]
		[TestCase(2, 96, "Left")]
		[TestCase(2, 97, "Left")]
		public void TestGetCloserBorder_BottomLeft(int x, int y, string expectedBorder)
		{
			TestGetCloserBordertCore(x, y, expectedBorder, () => screener.bottomLeftRect);
		}

		[TestCase(0, 5, "Left")]
		[TestCase(4, 5, "Left")]
		[TestCase(0, 94, "Left")]
		[TestCase(4, 94, "Left")]
		[TestCase(2, 40, "Left")]
		public void TestGetCloserBordert_Left(int x, int y, string expectedBorder)
		{
			TestGetCloserBordertCore(x, y, expectedBorder, () => screener.leftRect);
		}

		void TestGetCloserBordertCore(int x, int y, string expectedBorder, Func<Rectangle> rect)
		{
			var point = new Point(x, y);
			Assert.That(rect().Contains(point), Is.True);
			StringAssert.StartsWith(expectedBorder, screener.GetCloserBorder(point).GetType().Name);
		}

		[TestCase(-10, -20)]
		[TestCase(55, 55)]
		public void TestGetCloserBordert(int x, int y)
		{
			StringAssert.StartsWith("None", screener.GetCloserBorder(new Point(x, y)).GetType().Name);
		}

		private Screener screener;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 100), 5);
		}
	}
}
