using System;
using System.Collections.Generic;

namespace GraphOrientations
{
    internal class GraphsMapper
    {
        public int[] FromG6(string g6)
        {
            var n = g6[0] - '?';
            var result = new List<int>(n);
            for (int i = 0; i < n; i++)
                result.Add(0);

            var rOffset = 32;
            var k = 1;
            var val = g6[k] - '?';

            var it = 1;

            for (int i = 0; i < n; i++)
            {
                var offset = 1;
                for (int j = 0; j < i; j++)
                {
                    if ((val & rOffset) > 0)
                    {
                        result[i] |= offset;
                        result[j] |= it;
                    }

                    rOffset >>= 1;
                    offset <<= 1;

                    if (rOffset == 0)
                    {
                        rOffset = 32;
                        k++;
                        if (k < g6.Length)
                            val = g6[k] - '?';
                        else if (j != i - 1)
                        {
                            throw new Exception("Ошибка формата g6");
                        }
                    }
                }

                it <<= 1;
            }

            return result.ToArray();
        }
    }
}
