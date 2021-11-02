using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	abstract class BorderCollectionTest<T> where T : BorderBase
	{
		public virtual void TestAdd_AllSequence(int[] indicesToAdd)
		{
			// Arrange & Act
			var collection = new BorderCollection<T>();
			for (var index = 0; index < indicesToAdd.Length; ++index)
			{
				collection.Add(borders[index]);
			}

			// Assert
			Assert.That(collection, Is.EqualTo(indicesToAdd.OrderBy(x => x).Select(x => borders[x])));
		}

		protected T[] borders;

		[SetUp]
		public void SetUp()
		{
			var screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
			borders = CreateOrderedBorders(screener);
		}

		protected abstract T[] CreateOrderedBorders(Screener screener);
	}

	class BorderCollectionTest : BorderCollectionTest<Border>
	{
		[TestCaseSource(typeof(SequenceGenerator), nameof(SequenceGenerator.CreateAllPossibleSequences), new object[] { 4 })]
		public override void TestAdd_AllSequence(int[] indicesToAdd)
		{
			base.TestAdd_AllSequence(indicesToAdd);
		}

		[TestCaseSource(typeof(SequenceGenerator), nameof(SequenceGenerator.CreateSequenceForSingle))]
		public void TestAdd_SameUnit(int indexToAdd)
		{
			// Arrange
			var collection = new BorderCollection<Border>();

			// Act
			collection.Add(borders[indexToAdd]);

			// Assert
			Assert.That(() => collection.Add(borders[indexToAdd]), Throws.TypeOf<BorderException>());
		}

		protected override Border[] CreateOrderedBorders(Screener screener)
			=> new[]
			{
				new Border(screener, BorderVector.TopUnit, new Point(0, 0), 100),
				new Border(screener, BorderVector.RightUnit, new Point(99, 0), 100),
				new Border(screener, BorderVector.BottomUnit, new Point(99, 99), 100),
				new Border(screener, BorderVector.LeftUnit, new Point(0, 99), 100),
			};
	}

	//class SegmentBorderTest : BorderCollectionTest<SegmentBorder>
	//{
	//	readonly SegmentBorder[] orderedBorders;

	//	public SegmentBorderTest(SegmentBorder[] orderedBorders)
	//	{
	//		this.orderedBorders = orderedBorders;
	//	}

	//	public void TestAdd_Overlapped(int indexToAdd)
	//	{
	//		// Arrange
	//		var collection = new BorderCollection<Border>();

	//		// Act
	//		collection.Add(borders[indexToAdd]);

	//		// Assert
	//		Assert.That(() => collection.Add(borders[indexToAdd]), Throws.TypeOf<BorderException>());
	//	}

	//	public void TestAdd_DifferentKindUnit(int indexToAdd)
	//	{
	//		// Arrange
	//		var collection = new BorderCollection<Border>();

	//		// Act
	//		collection.Add(borders[indexToAdd]);

	//		// Assert
	//		Assert.That(() => collection.Add(borders[indexToAdd]), Throws.TypeOf<BorderException>());
	//	}

	//	protected override Border[] CreateOrderedBorders(Screener screener) => orderedBorders;
	//}
}
