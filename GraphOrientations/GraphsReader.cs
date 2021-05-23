using System.Collections.Generic;
using System.Diagnostics;

namespace GraphOrientations
{
    internal class GraphsReader
    {
        public IEnumerable<string> ReadGraphs(int vectexCount)
        {
            Process compiler = new Process();
            compiler.StartInfo.FileName = "geng.exe";
            compiler.StartInfo.Arguments = $"{vectexCount} -c";
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.Start();

            for (; ; )
            {
                var current = compiler.StandardOutput.ReadLine();

                if (string.IsNullOrEmpty(current))
                {
                    break;
                }

                yield return current;
            }
        }
    }
}
