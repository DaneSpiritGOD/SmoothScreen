using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class MouseStateCollectionTest
	{
		[Test]
		public void TestThis()
		{
			var screen = new Screener(new Rectangle(0, 0, 10, 10), 1, 1);
			var collection = new MouseStateCollection(3);
			collection.Fill(new MouseState { Screen = screen, Point = new Point(0,0) });
			collection.Fill(new MouseState { Screen = screen, Point = new Point(1,1) });
			collection.Fill(new MouseState { Screen = screen, Point = new Point(2,2) });

			collection[0].Point.Equals(new Point(2, 2));
			collection[1].Point.Equals(new Point(1, 1));
			collection[2].Point.Equals(new Point(0, 0));
		}

		[Test]
		public void TestIEnumerable()
		{
			var collection = new MouseStateCollection(3);
			CollectionAssert.AreEqual(collection, new string[0]);

			var screen = new Screener(new Rectangle(0, 0, 10, 10), 1, 1);

			collection.Fill(new MouseState { Screen = screen, Point = new Point(0, 0) });
			CollectionAssert.AreEqual(collection, new [] { new MouseState { Screen = screen, Point = new Point(0, 0) } });

			collection.Fill(new MouseState { Screen = screen, Point = new Point(1, 1) });
			CollectionAssert.AreEqual(collection, new []
			{
				new MouseState { Screen = screen, Point = new Point(1, 1) },
				new MouseState { Screen = screen, Point = new Point(0, 0) }
			});

			collection.Fill(new MouseState { Screen = screen, Point = new Point(2, 2) });
			CollectionAssert.AreEqual(collection, new [] 
			{ 
				new MouseState { Screen = screen, Point = new Point(2, 2) },
				new MouseState { Screen = screen, Point = new Point(1, 1) },
				new MouseState { Screen = screen, Point = new Point(0, 0) }
			});
		}
	}
}
