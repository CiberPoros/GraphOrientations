using System.Collections.Generic;
using System.Linq;

namespace GraphOrientations
{
    internal class GraphOrientator
    {
        public IEnumerable<int[]> Orient(int[] graph)
        {
            var codes = new HashSet<long>();
            var substitutions = Utils.EnumerateAllSubstitutions(graph.Length).ToArray();

            foreach (var orientedGraph in OrientInternal(graph))
            {
                var code = Utils.GetGraphCode(graph);

                if (codes.Contains(code))
                {
                    continue;
                }

                yield return graph;

                foreach (var substitution in substitutions)
                {
                    var currentGraph = Utils.UseSubstitution(graph, substitution);
                    var currentCode = Utils.GetGraphCode(currentGraph);
                    codes.Add(currentCode);
                }
            }
        }

        private IEnumerable<int[]> OrientInternal(int[] graph, int from = 0, int to = 1)
        {        
            if (from >= graph.Length)
            {
                yield return graph;
            }
            else
            {
                var toMask = 1 << to;
                int nextFrom = to >= graph.Length - 1 ? from + 1 : from;
                int nextTo = to == graph.Length - 1 ? nextFrom + 1 : to + 1;

                if ((graph[from] & toMask) != 0)
                {
                    var fromMask = 1 << from;
                    graph[to] ^= fromMask;
                    foreach (var val in OrientInternal(graph, nextFrom, nextTo))
                    {
                        yield return val;
                    }
                    graph[to] ^= fromMask;

                    graph[from] ^= toMask;
                    foreach (var val in OrientInternal(graph, nextFrom, nextTo))
                    {
                        yield return val;
                    }
                    graph[from] ^= toMask;
                }
                else
                {
                    foreach (var val in OrientInternal(graph, nextFrom, nextTo))
                    {
                        yield return val;
                    }
                }
            }
        }
    }
}
