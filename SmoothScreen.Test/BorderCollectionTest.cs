using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	abstract class BorderCollectionTest<T> where T : BorderBase
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		public void TestAdd_Single(int indexToAdd)
		{
			// Arrange & Act
			var collection = new BorderCollection<T>();
			for (var index = borders.Length - 1; index >= 0; --index)
			{
				if (index != indexToAdd)
				{
					continue;
				}

				collection.Add(borders[index]);
			}

			// Assert
			Assert.That(collection, Is.EqualTo(new [] { borders[indexToAdd] }));
		}

		protected T[] borders;

		[SetUp]
		public void SetUp()
		{
			var screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
			borders = CreateBorders(screener);
		}

		protected abstract T[] CreateBorders(Screener screener);
	}

	class BorderCollectionTest : BorderCollectionTest<Border>
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		public void TestAdd_SameUnit(int indexToAdd)
		{
			// Arrange
			var collection = new BorderCollection<Border>();

			// Act
			collection.Add(borders[indexToAdd]);

			// Assert
			Assert.That(() => collection.Add(borders[indexToAdd]), Throws.TypeOf<BorderException>());
		}

		protected override Border[] CreateBorders(Screener screener)
			=> new[]
			{
				new Border(screener, BorderVector.TopUnit, new Point(0, 0), 100),
				new Border(screener, BorderVector.RightUnit, new Point(99, 0), 100),
				new Border(screener, BorderVector.BottomUnit, new Point(99, 99), 100),
				new Border(screener, BorderVector.LeftUnit, new Point(0, 99), 100),
			};
	}
}
