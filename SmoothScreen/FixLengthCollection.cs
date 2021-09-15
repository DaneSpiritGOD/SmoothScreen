using System;
using System.Collections;
using System.Collections.Generic;

namespace SmoothScreen
{
	class FixLengthCollection<T> : IEnumerable<T>
	{
		int head;

		public FixLengthCollection(int capacity)
		{
			items = new T[capacity];
			head = 0;
			Length = 0;
		}

		readonly T[] items;

		public int Capacity => items.Length;

		public void Fill(T item)
		{
			if (Length < Capacity)
			{
				items[Length++] = item;
			}
			else
			{
				items[head] = item;
				head = GetItemsIndex(1);
			}
		}

		public int Length { get; private set; }

		public T this[int index] => Length > 0 && index >=0 && index < Length ? items[GetItemsIndex(index)] : throw new IndexOutOfRangeException("No items.");

		virtual protected int GetItemsIndex(int index) => (head + index) % Capacity;

		public IEnumerator<T> GetEnumerator()
		{
			if (Length == 0)
			{
				yield break;
			}

			for (var index = 0; index < Length; ++index)
			{
				yield return this[index];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if DEBUG
		internal T[] ItemsForTest => items;
#endif
	}
}
