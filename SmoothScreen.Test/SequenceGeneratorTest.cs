using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class SequenceGeneratorTest
	{
		[Test]
		public void TestCreate_PickUpZero()
		{
			// Arrange & Act
			var sequences = ArrangementGenerator.Create(3, 0);

			// Assert
			Assert.That(sequences, Has.Exactly(0).Items);
		}

		[Test]
		public void TestCreate_ZeroItems()
		{
			// Arrange & Act
			var sequences = ArrangementGenerator.Create(0, 3);

			// Assert
			Assert.That(sequences, Has.Exactly(0).Items);
		}

		[Test]
		public void TestCreate_AllZero()
		{
			// Arrange & Act
			var sequences = ArrangementGenerator.Create(0, 0);

			// Assert
			Assert.That(sequences, Has.Exactly(0).Items);
		}

		[TestCase(1, new[] { 0, 1, 2})]
		[TestCase(2, new[] { 0, 1, 0, 2, 1, 0, 1, 2, 2, 0, 2, 1})]
		[TestCase(3, new[] { 0, 1, 2, 0, 2, 1, 1, 0, 2, 1, 2, 0, 2, 0, 1, 2, 1, 0 })]
		public void TestCreate(int n, int[] expectedResults)
		{
			// Arrange & Act
			var sequences = ArrangementGenerator.Create(3, n);

			// Assert
			Assert.That(sequences, Is.EquivalentTo(SplitIntoGroups(expectedResults, n)));
		}

		[Test]
		public void TestCreateForSingleElement()
		{
			// Arrange & Act
			var sequences = ArrangementGenerator.CreateForSingleElement(3);
			// Assert
			Assert.That(sequences, Is.EquivalentTo(new[] { 0, 1, 2 }));
		}

		[Test]
		public void TestCreateAll()
		{
			// Arrange & Act
			var sequences = ArrangementGenerator.CreateAll(3);
			// Assert
			Assert.That(
				sequences,
				Is.EquivalentTo(
					new[]
					{
						new []{ 0 },
						new []{ 1 },
						new []{ 2 },
						new []{ 0, 1 },
						new []{ 0, 2 },
						new []{ 1, 0 },
						new []{ 1, 2 },
						new []{ 2, 0 },
						new []{ 2, 1 },
						new []{ 0, 1, 2 },
						new []{ 0, 2, 1 },
						new []{ 1, 0, 2 },
						new []{ 1, 2, 0 },
						new []{ 2, 0, 1 },
						new []{ 2, 1, 0 },
					}));
		}

		static List<int[]> SplitIntoGroups(int[] items, int columns)
		{
			var list = new List<int[]>();
			var index = 0;
			while (index < items.Length)
			{
				var subGroup = new int[columns];
				Array.Copy(items, index, subGroup, 0, columns);
				list.Add(subGroup);
				index += columns;
			}
			return list;
		}
	}
}
