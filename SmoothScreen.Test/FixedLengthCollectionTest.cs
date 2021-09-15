using System;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class FixedLengthCollectionTest
	{
		[Test]
		public void TestCapacity()
		{
			var collection = new FixLengthCollection<string>(3);
			Assert.That(collection.Capacity, Is.EqualTo(collection.ItemsForTest.Length));
		}

		[Test]
		public void TestFill()
		{
			var collection = new FixLengthCollection<string>(3);
			CollectionAssert.AreEqual(collection.ItemsForTest, new string[] { null, null, null });

			collection.Fill("1");
			CollectionAssert.AreEqual(collection.ItemsForTest, new string[] { "1", null, null });

			collection.Fill("2");
			CollectionAssert.AreEqual(collection.ItemsForTest, new string[] { "1", "2", null });

			collection.Fill("3");
			CollectionAssert.AreEqual(collection.ItemsForTest, new string[] { "1", "2", "3" });

			collection.Fill("4");
			CollectionAssert.AreEqual(collection.ItemsForTest, new string[] { "4", "2", "3" });

			collection.Fill("5");
			CollectionAssert.AreEqual(collection.ItemsForTest, new string[] { "4", "5", "3" });

			collection.Fill("6");
			CollectionAssert.AreEqual(collection.ItemsForTest, new string[] { "4", "5", "6" });
		}

		[TestCase(0, 0)]
		[TestCase(1, 1)]
		[TestCase(2, 2)]
		[TestCase(3, 3)]
		[TestCase(4, 3)]
		[TestCase(5, 3)]
		[TestCase(6, 3)]
		public void TestLength(int count, int expectedLength)
		{
			var collection = new FixLengthCollection<string>(3);
			for (var index = 0; index < count; ++index)
			{
				collection.Fill(index.ToString());
			}

			Assert.That(collection.Length, Is.EqualTo(expectedLength));
		}

		[Test]
		public void TestIEnumerable()
		{
			var collection = new FixLengthCollection<string>(3);
			CollectionAssert.AreEqual(collection, new string[0]);

			collection.Fill("1");
			CollectionAssert.AreEqual(collection, new string[] { "1"});

			collection.Fill("2");
			CollectionAssert.AreEqual(collection, new string[] { "1", "2"});

			collection.Fill("3");
			CollectionAssert.AreEqual(collection, new string[] { "1", "2", "3" });

			collection.Fill("4");
			CollectionAssert.AreEqual(collection, new string[] { "2", "3", "4" });

			collection.Fill("5");
			CollectionAssert.AreEqual(collection, new string[] { "3", "4", "5" });

			collection.Fill("6");
			CollectionAssert.AreEqual(collection, new string[] { "4", "5", "6" });
		}

		[Test]
		public void TestThis()
		{
			var collection = new FixLengthCollection<string>(3);
			collection.Fill("1");
			collection.Fill("2");
			collection.Fill("3");

			Assert.That(collection[0], Is.EqualTo("1"));
			Assert.That(collection[1], Is.EqualTo("2"));
			Assert.That(collection[2], Is.EqualTo("3"));

			collection.Fill("4");
			collection.Fill("5");

			Assert.That(collection[0], Is.EqualTo("3"));
			Assert.That(collection[1], Is.EqualTo("4"));
			Assert.That(collection[2], Is.EqualTo("5"));
		}

		[TestCase(-1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public void TestIndexOutOfRangeException_LengthGreaterThanZero(int index)
		{
			var collection = new FixLengthCollection<string>(3);
			collection.Fill("1");
			collection.Fill("2");

			Assert.That(() => collection[index], Throws.InstanceOf<IndexOutOfRangeException>());
		}

		[TestCase(-1)]
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public void TestIndexOutOfRangeException_LengthEqualZero(int index)
		{
			var collection = new FixLengthCollection<string>(3);
			Assert.That(() => collection[index], Throws.InstanceOf<IndexOutOfRangeException>());
		}
	}
}
