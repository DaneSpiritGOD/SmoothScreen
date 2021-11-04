using System.Drawing;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class ScreenExtensionsTest
	{
		[Test]
		public void TestGetTopBorder()
		{
			Assert.That(screener.GetTopBorder(), Is.EqualTo(new Border(screener, BorderVector.TopUnit, new Point(0, 0), 100)));
		}

		[Test]
		public void TestGetRightBorder()
		{
			Assert.That(screener.GetRightBorder(), Is.EqualTo(new Border(screener, BorderVector.RightUnit, new Point(99, 0), 50)));
		}

		[Test]
		public void TestGetBottomBorder()
		{
			Assert.That(screener.GetBottomBorder(), Is.EqualTo(new Border(screener, BorderVector.BottomUnit, new Point(99, 49), 100)));
		}

		[Test]
		public void TestGetLeftBorder()
		{
			Assert.That(screener.GetLeftBorder(), Is.EqualTo(new Border(screener, BorderVector.LeftUnit, new Point(0, 49), 50)));
		}

		Screener screener;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 50), 5, 10);
		}
	}
}
