using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoothScreen.Test
{
	static class SequenceGenerator
	{
		public static IEnumerable<int[]> CreateAllPossibleSequences(int maxLayerCount)
		{
			for (var maxLayerIndex = 1; maxLayerIndex <= maxLayerCount; ++maxLayerIndex)
			{
				var existingIndices = Enumerable.Empty<int>();
				for (var layerIndex = 1; layerIndex <= maxLayerIndex; ++layerIndex)
				{
					var sequences = CreateSequence(existingIndices, layerIndex, maxLayerCount);
					foreach (var sequence in sequences)
					{
						existingIndices = sequence;
					}
				}
				yield return existingIndices.ToArray();
			}
		}

		public  static int[] CreateSequenceForSingle() => CreateAllPossibleSequences(1).Select(x => x.Single()).ToArray();

		static IEnumerable<IEnumerable<int>> CreateSequence(IEnumerable<int> existingIndices, int maxLayerCount, int maxLength)
		{
			if (existingIndices.Count() > maxLayerCount)
				yield break;

			for (var index = 0; index < maxLength; ++index)
			{
				if (existingIndices.Contains(index))
				{
					continue;
				}

				yield return existingIndices.Append(index);
			}
		}
	}
}
