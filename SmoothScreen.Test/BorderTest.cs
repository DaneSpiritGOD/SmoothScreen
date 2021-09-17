using System;
using System.Drawing;
using NUnit.Framework;
using SmoothScreen.Borders;

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
		// TODO: Subset
		// TODO: Compare owner
		[TestCase(typeof(TopBorder), 0, 0, 1, 1)]
		[TestCase(typeof(TopBorder), 5, 0, 0, 0)]
		[TestCase(typeof(RightBorder), 0, 0, 1, 1)]
		[TestCase(typeof(RightBorder), 0, 5, 0, 0)]
		[TestCase(typeof(BottomBorder), 0, 0, 1, 1)]
		[TestCase(typeof(BottomBorder), 0, 0, 5, 0)]
		[TestCase(typeof(LeftBorder), 0, 0, 1, 1)]
		[TestCase(typeof(LeftBorder), 0, 0, 0, 5)]
		public void TestDistinctAxisBorderException(Type borderType, int startX, int startY, int endX, int endY)
		{
			var border = (Border)Activator.CreateInstance(borderType, screener);
			Assert.That(() => border.GetSegmentBorderForTest(new Line(startX, startY, endX, endY)), Throws.TypeOf<DistinctAxisBorderException>());
		}

		[TestCase(typeof(TopBorder), 0, 0, 99, 0)]
		[TestCase(typeof(TopBorder), -5, 0, 0, 0)]
		[TestCase(typeof(TopBorder), -10, 0, -5, 0)]
		[TestCase(typeof(TopBorder), 99, 0, 120, 0)]
		[TestCase(typeof(TopBorder), 120, 0, 130, 0)]
		[TestCase(typeof(TopBorder), -10, 0, 120, 0)]
		[TestCase(typeof(RightBorder), 99, 0, 99, 99)]
		//[TestCase(typeof(RightBorder), 99, 0, 99, 99)]
		//[TestCase(typeof(BottomBorder))]
		//[TestCase(typeof(LeftBorder))]
		public void TestSegmentBorderNotSubsetOfParentException(Type borderType, int startX, int startY, int endX, int endY)
		{
			var border = (Border)Activator.CreateInstance(borderType, screener);
			Assert.That(() => border.GetSegmentBorderForTest(new Line(startX, startY, endX, endY)), Throws.TypeOf<SegmentBorderNotSubsetOfParentException>());
		}

		Screener screener;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
		}
	}
}
