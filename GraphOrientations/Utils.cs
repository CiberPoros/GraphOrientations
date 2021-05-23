using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphOrientations
{
    internal static class Utils
    {
        public static IEnumerable<int[]> EnumerateAllSubstitutions(int n)
        {
            var vertexNumbers = new int[n];
            bool[] used = new bool[n];

            return Enumerate(used, 0);

            IEnumerable<int[]> Enumerate(bool[] used, int deep)
            {
                if (deep == n)
                {
                    yield return vertexNumbers.ToArray();
                }

                for (int i = 0; i < n; i++)
                {
                    if (used[i])
                    {
                        continue;
                    }

                    used[i] = true;
                    vertexNumbers[deep] = i;

                    foreach (var val in Enumerate(used, deep + 1))
                        yield return val;

                    used[i] = false;
                }
            }
        }

        public static int[] UseSubstitution(int[] graph, int[] substitution)
        {
            var result = new int[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0, jmask = 1; j < graph.Length; j++, jmask <<= 1)
                {
                    if ((graph[i] & jmask) == 0)
                    {
                        continue;
                    }

                    result[substitution[i]] |= 1 << substitution[j];
                }
            }

            return result;
        }

        public static long GetGraphCode(int[] graph)
        {
            if (graph.Length > 8)
            {
                throw new ArgumentException("Method don't works with graphs 9 or more degree.");
            }

            var result = 0L;
            var currentMask = 1L;

            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0, jmask = 1; j < graph.Length; j++, jmask <<= 1)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if ((graph[i] & jmask) != 0)
                    {
                        result |= currentMask;
                    }

                    currentMask <<= 1;
                }
            }

            return result;
        }
    }
}
