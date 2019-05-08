using System;
using System.IO;
using System.Collections.Generic;

namespace AlphaFX.WordCount
{
    public class TextFileReader : IFileReader
    {
        public IEnumerable<string> FileReader(string filePath)
        {
            return File.ReadLines(filePath);
            
        }
    }
}
