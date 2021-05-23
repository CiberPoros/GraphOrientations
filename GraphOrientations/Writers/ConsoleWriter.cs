using System;

namespace GraphOrientations.Writers
{
    internal class ConsoleWriter : IWriter
    {
        public void Write(string s) => Console.Write(s);
        public void Write(int value) => Console.Write(value);
        public void WriteLine(string s) => Console.WriteLine(s);
        public void WriteLine() => Console.WriteLine();
    }
}
