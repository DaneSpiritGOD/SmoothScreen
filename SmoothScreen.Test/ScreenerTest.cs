using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class ScreenerTest
	{
		[TestCase(10, 20, true)]
		[TestCase(109, 20, true)]
		[TestCase(10, 319, true)]
		[TestCase(109, 319, true)]
		[TestCase(15, 25, false)]
		[TestCase(104, 25, false)]
		[TestCase(15, 314, false)]
		[TestCase(104, 314, false)]
		public void TestIsCloseToBorder(int x, int y, bool expectedResult)
		{
			var screener = new Screener(new Rectangle(10, 20, 100, 300), 5);
			Assert.That(screener.IsCloseToBorder(new Point(x,y)), Is.EqualTo(expectedResult));
		}
	}
}
