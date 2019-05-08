using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaFX.WordCount
{
    public interface IFileWordCounter
    {
        ConcurrentDictionary<string,int> FileWordsCount(IEnumerable<string> fileLines);
        IDictionary<string, int> Top10WordByCount(string filePath);
    }
}
