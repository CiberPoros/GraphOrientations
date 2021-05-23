using System;
using CommandLine;
using GraphOrientations.Writers;

namespace GraphOrientations
{
    internal class EntryPoint
    {
        static void Main(string[] args)
        {
            var consoleArguments = new Options();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var startTime = DateTime.Now;
                    var reader = new GraphsReader();
                    var mapper = new GraphsMapper();
                    var orientator = new GraphOrientator();
                    IWriter writer = o.WriteGraphsToFile ? new FileWriterCustom(o.FileName) : new ConsoleWriter();
                    var n = o.VertexCount;

                    var totalCount = 0;

                    if (o.CalculateOnly)
                    {
                        foreach (var g6Graph in reader.ReadGraphs(n))
                        {
                            var graph = mapper.FromG6(g6Graph);

                            foreach (var oriented in orientator.Orient(graph))
                            {
                                totalCount++;
                            }
                        }

                        Console.WriteLine($"Total graphs count: {totalCount}.");
                        Console.WriteLine($"Total calculating time in seconds: {(DateTime.Now - startTime).TotalSeconds}.");

                        return;
                    }

                    foreach (var g6Graph in reader.ReadGraphs(n))
                    {
                        var graph = mapper.FromG6(g6Graph);

                        writer.WriteLine($"Current graph: {g6Graph}");
                        writer.WriteLine();

                        foreach (var oriented in orientator.Orient(graph))
                        {
                            for (int i = 0; i < n; i++)
                            {
                                for (int j = 0, jMask = 1; j < n; j++, jMask <<= 1)
                                {
                                    writer.Write((oriented[i] & jMask) == 0 ? 0 : 1);
                                }
                                writer.WriteLine();
                            }
                            writer.WriteLine();
                            totalCount++;
                        }
                        writer.WriteLine();                
                    }

                    Console.WriteLine($"Total graphs count: {totalCount}.");
                    Console.WriteLine($"Total calculating time in seconds: {(DateTime.Now - startTime).TotalSeconds}.");
                });
        }
    }
}
