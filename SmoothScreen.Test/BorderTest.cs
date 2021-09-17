using System;
using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class BorderTest
	{
		[TestCase(typeof(TopBorder), 0, 0, 99, 0)]
		[TestCase(typeof(RightBorder), 99, 0, 99, 99)]
		[TestCase(typeof(BottomBorder), 99, 99, 0, 99)]
		[TestCase(typeof(LeftBorder), 0, 99, 0, 0)]
		public void TestLine(Type borderType, int expectedStartX, int expectedStartY, int expectedEndX, int expectedEndY)
		{
			var border = (Border)Activator.CreateInstance(borderType, screener);
			Assert.That(border.Line, Is.EqualTo(new Line(expectedStartX, expectedStartY, expectedEndX, expectedEndY)));
		}

		// TODO: Constructor throw DistinctAxisBorderException
		// TODO: OverlapException
		[TestCase()]
		public void TestDistinctAxisBorderException_LeftBoarder()
		{
//			var left = new LeftBorder(screener, );
		}

		Screener screener;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
		}
	}
}
