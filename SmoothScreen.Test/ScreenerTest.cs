using System.Drawing;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class ScreenerTest
	{
		[Test]
		public void TestEqual()
		{
			Assert.That(screener, Is.EqualTo(new Screener(new Rectangle(0, 0, 100, 50), 5, 10)));
		}

		[TestCase(-10, -10)]
		[TestCase(109, -10)]
		[TestCase(-10, 49)]
		[TestCase(109, 49)]
		public void TestExpandScreen(int x, int y)
		{
			Assert.That(expandScreen.Own(new Point(x, y)), Is.True);
		}

		[Test]
		public void TestTopBorder()
		{
			Assert.That(screener.TopBorder, Is.EqualTo(new Border(screener, BorderVector.TopUnit, new Point(0, 0), 100)));
			Assert.That(screener.TopBorder, Is.SameAs(screener.TopBorder));
		}

		[Test]
		public void TestRightBorder()
		{
			Assert.That(screener.RightBorder, Is.EqualTo(new Border(screener, BorderVector.RightUnit, new Point(99, 0), 50)));
			Assert.That(screener.RightBorder, Is.SameAs(screener.RightBorder));
		}

		[Test]
		public void TestBottomBorder()
		{
			Assert.That(screener.BottomBorder, Is.EqualTo(new Border(screener, BorderVector.BottomUnit, new Point(99, 49), 100)));
			Assert.That(screener.BottomBorder, Is.SameAs(screener.BottomBorder));
		}

		[Test]
		public void TestLeftBorder()
		{
			Assert.That(screener.LeftBorder, Is.EqualTo(new Border(screener, BorderVector.LeftUnit, new Point(0, 49), 50)));
			Assert.That(screener.LeftBorder, Is.SameAs(screener.LeftBorder));
		}

		Screener screener;
		Screener expandScreen;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 50), 5, 10);
			expandScreen = screener.Expand();
		}
	}
}
