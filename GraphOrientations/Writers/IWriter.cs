namespace GraphOrientations.Writers
{
    internal interface IWriter
    {
        void WriteLine(string s);
        void WriteLine();
        void Write(string s);
        void Write(int value);
    }
}
