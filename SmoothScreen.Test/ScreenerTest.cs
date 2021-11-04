using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class ScreenerTest
	{
		[Test]
		public void TestEqual()
		{
			Assert.That(screener, Is.EqualTo(new Screener(new Rectangle(0, 0, 100, 100), 5, 10)));
		}

		[TestCase(-10, -10)]
		[TestCase(109, -10)]
		[TestCase(-10, 109)]
		[TestCase(109, 109)]
		public void TestExpandScreen(int x, int y)
		{
			Assert.That(expandScreen.Own(new Point(x, y)), Is.True);
		}

		Screener screener;
		Screener expandScreen;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
			expandScreen = screener.Expand();
		}
	}
}
