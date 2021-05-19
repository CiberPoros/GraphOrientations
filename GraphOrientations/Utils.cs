using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphOrientations
{
    public static class Utils
    {
        public static IEnumerable<int[]> GetAllSubstitutions(int n)
        {
            return GetAllSubstitutionsInternal(n, new int[n]);
        }

        public static IEnumerable<int[]> GetAllSubstitutionsInternal(int n, int[] substitution, int currentIndex = 0, int usedMask = 0)
        {
            for (int i = 0, iMask = 1; i < n; i++, iMask <<= 1)
            {
                if ((usedMask & iMask) != 0)
                {
                    continue;
                }

                substitution[currentIndex] = i;

                if (i == n - 1)
                {
                    yield return substitution.ToArray();
                }
                else
                {
                    foreach (var val in GetAllSubstitutionsInternal(n, substitution, currentIndex + 1, usedMask | iMask))
                    {
                        yield return val;
                    }
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
                    if ((result[i] & jmask) == 0)
                    {
                        continue;
                    }

                    result[substitution[i]] |= 1 << substitution[j];
                    result[substitution[j]] |= 1 << substitution[i];
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
