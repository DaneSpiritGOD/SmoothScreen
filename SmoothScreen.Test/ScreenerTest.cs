using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class ScreenerTest
	{
		[TestCase(0, 0, Border.Left)]
		[TestCase(4, 0, Border.Top)]
		[TestCase(0, 4, Border.Left)]
		[TestCase(4, 4, Border.Left)]
		[TestCase(1, 3, Border.Left)]
		[TestCase(3, 1, Border.Top)]
		[TestCase(3, 3, Border.Left)]
		public void TestGetCloserBorder_TopLeft(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.topLeftRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(5, 0, Border.Top)]
		[TestCase(94, 0, Border.Top)]
		[TestCase(5, 4, Border.Top)]
		[TestCase(94, 4, Border.Top)]
		[TestCase(40, 3, Border.Top)]
		public void TestGetCloserBorder_Top(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.topRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(95, 0, Border.Top)]
		[TestCase(99, 0, Border.Right)]
		[TestCase(95, 4, Border.Right)]
		[TestCase(99, 4, Border.Right)]
		[TestCase(97, 1, Border.Top)]
		[TestCase(98, 2, Border.Right)]
		[TestCase(98, 1, Border.Right)]
		public void TestGetCloserBorder_TopRight(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.topRightRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(95, 5, Border.Right)]
		[TestCase(99, 5, Border.Right)]
		[TestCase(95, 94, Border.Right)]
		[TestCase(99, 94, Border.Right)]
		[TestCase(97, 40, Border.Right)]
		public void TestGetCloserBorder_Right(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.rightRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(95, 95, Border.Right)]
		[TestCase(99, 95, Border.Right)]
		[TestCase(99, 99, Border.Right)]
		[TestCase(95, 99, Border.Bottom)]
		[TestCase(98, 97, Border.Right)]
		[TestCase(97, 98, Border.Bottom)]
		[TestCase(98, 98, Border.Right)]
		public void TestGetCloserBorder_BottomRight(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.bottomRightRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(5, 95, Border.Bottom)]
		[TestCase(94, 95, Border.Bottom)]
		[TestCase(5, 99, Border.Bottom)]
		[TestCase(94, 99, Border.Bottom)]
		[TestCase(40, 97, Border.Bottom)]
		public void TestGetCloserBorder_Bottom(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.bottomRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(0, 95, Border.Left)]
		[TestCase(4, 95, Border.Left)]
		[TestCase(0, 99, Border.Left)]
		[TestCase(4, 99, Border.Bottom)]
		[TestCase(3, 97, Border.Bottom)]
		[TestCase(2, 96, Border.Left)]
		[TestCase(2, 97, Border.Left)]
		public void TestGetCloserBorder_BottomLeft(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.bottomLeftRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(0, 5, Border.Left)]
		[TestCase(4, 5, Border.Left)]
		[TestCase(0, 94, Border.Left)]
		[TestCase(4, 94, Border.Left)]
		[TestCase(2, 40, Border.Left)]
		public void TestGetCloserBordert_Left(int x, int y, Border expectedBorder)
		{
			var point = new Point(x, y);
			Assert.That(screener.leftRect.Contains(point), Is.True);
			Assert.That(screener.GetCloserBorder(point), Is.EqualTo(expectedBorder));
		}

		[TestCase(-10, -20)]
		[TestCase(55, 55)]
		public void TestGetCloserBordert(int x, int y)
		{
			Assert.That(screener.GetCloserBorder(new Point(x, y)), Is.EqualTo(Border.None));
		}

		private Screener screener;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 100), 5);
		}
	}
}
