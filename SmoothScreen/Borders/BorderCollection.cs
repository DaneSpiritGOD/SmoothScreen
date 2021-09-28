using System.Collections;
using System.Collections.Generic;

namespace SmoothScreen.Borders
{
	class BorderCollection<T> : IEnumerable<T> where T : BorderBase
	{
		readonly IList<T> items;

		public BorderCollection()
		{
			items = new List<T>();
		}

		public void Add(T item)
		{
			var index = BinarySearchToInsert(items, 0, items.Count, item, Comparer<T>.Default);
			items.Insert(index, item);
		}

		public T this[int index] => items[index];

		static int BinarySearchToInsert(IList<T> items, int index, int length, T value, IComparer<T> comparer)
		{
			int num = index;
			int num2 = index + length - 1;
			while (num <= num2)
			{
				int num3 = num + (num2 - num >> 1);
				int num4 = comparer.Compare(items[num3], value);
				if (num4 == 0)
				{
					return num3;
				}
				if (num4 < 0)
				{
					num = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			return num;
		}

		public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
	}
}
