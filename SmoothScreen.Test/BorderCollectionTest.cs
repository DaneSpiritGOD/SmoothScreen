using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class BorderCollectionTest
	{
		[TestCaseSource(typeof(ArrangementGenerator), nameof(ArrangementGenerator.CreateAll), new object[] { Length })]
		public void TestAdd_AllSequence(IEnumerable<int> indicesToAdd)
		{
			// Arrange & Act
			var collection = new BorderCollection<Border>();
			foreach (var item in indicesToAdd)
			{
				collection.Add(borders[item]);
			}

			// Assert
			Assert.That(collection, Is.EqualTo(indicesToAdd.OrderBy(x => x).Select(x => borders[x])));
		}

		[TestCaseSource(typeof(ArrangementGenerator), nameof(ArrangementGenerator.CreateForSingleElement), new object[] { Length })]
		public void TestAdd_SameUnit(int indexToAdd)
		{
			// Arrange
			var collection = new BorderCollection<Border>();

			// Act
			collection.Add(borders[indexToAdd]);

			// Assert
			Assert.That(() => collection.Add(borders[indexToAdd]), Throws.TypeOf<BorderException>());
		}

		[SetUp]
		public void SetUp()
		{
			var screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
			borders = new[]
			{
				new Border(screener, BorderVector.TopUnit, new Point(0, 0), 100),
				new Border(screener, BorderVector.RightUnit, new Point(99, 0), 100),
				new Border(screener, BorderVector.BottomUnit, new Point(99, 99), 100),
				new Border(screener, BorderVector.LeftUnit, new Point(0, 99), 100),
			};
		}

		protected Border[] borders;

		const int Length = 4;
	}
}
