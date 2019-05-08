using System.IO;
using System.Collections.Generic;

namespace AlphaFX.WordCount
{
    public interface IFileReader
    {
        IEnumerable<string> FileReader(string filePath);
    }
}
