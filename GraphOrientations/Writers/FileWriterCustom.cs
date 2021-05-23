using System;
using System.IO;

namespace GraphOrientations.Writers
{
    internal class FileWriterCustom : IWriter
    {
        private readonly string _fileName;

        public FileWriterCustom(string FileName)
        {
            _fileName = FileName ?? throw new ArgumentNullException(nameof(FileName));

            if (File.Exists(_fileName))
            {
                File.WriteAllText(_fileName, "");
            }
            else
            {
                using (var fs = File.Create(_fileName)) { };
            }
        }

        public void Write(string s) => File.AppendAllText(_fileName, s);
        public void Write(int value) => File.AppendAllText(_fileName, value.ToString());
        public void WriteLine(string s) => File.AppendAllText(_fileName, s + Environment.NewLine);
        public void WriteLine() => File.AppendAllText(_fileName, Environment.NewLine);
    }
}
