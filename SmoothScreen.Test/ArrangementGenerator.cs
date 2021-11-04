using System;
using System.Collections.Generic;
using System.Linq;

namespace SmoothScreen.Test
{
	static class ArrangementGenerator
	{
		public static IEnumerable<IEnumerable<int>> CreateAll(int n)
		{
			return Enumerable.Range(1, n)
				.Select(x => Create(n, x))
				.Aggregate(
				new List<IEnumerable<int>>(),
				(list, x) =>
				{
					list.AddRange(x);
					return list;
				});
		}

		public static IEnumerable<int> CreateForSingleElement(int n) => Create(n, 1).Select(x => x.Single()).ToArray();

		public static IEnumerable<IEnumerable<int>> Create(int n, int r)
		{
			return Create(Array.Empty<int>(), n, r);
		}

		static IEnumerable<IEnumerable<int>> Create(IEnumerable<int> picked, int n, int r)
		{
			if (r <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(r));
			}

			if (picked.Count() >= r)
			{
				return new[] { picked };
			}

			var list = new List<IEnumerable<int>>();

			var availables = GetAvailableToPickUp(picked, n);
			foreach (var available in availables)
			{
				var picked2 = picked.Append(available);
				list.AddRange(Create(picked2, n, r));
			}

			return list;
		}

		static IEnumerable<int> GetAvailableToPickUp(IEnumerable<int> picked, int n)
		{
			if (n <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(n));
			}

			for (var index = 0; index < n; ++index)
			{
				if (picked.Contains(index))
				{
					continue;
				}

				yield return index;
			}
		}
	}
}
