using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaFX.WordCount
{
    public class FileWordCounter : IFileWordCounter
    {
        IFileReader _fileReader;
        public FileWordCounter(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        /// <summary>
        /// function take IEnumerable list of fileline as parameter then parse line by space and count words
        /// while counting word its ignore the cases
        /// </summary>
        /// <param name="fileLines"></param>
        /// <returns></returns>
        public ConcurrentDictionary<string, int> FileWordsCount(IEnumerable<string> fileLines)
        {
            try
            {
                if (fileLines == null)
                    throw new Exception("file not exists exception");

                var wordCollection = new ConcurrentDictionary<string, int>();
                 Parallel.ForEach(fileLines, fileline =>
                {
                    var words = fileline.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach(var word in words)
                    {
                        wordCollection.AddOrUpdate(word.ToLower(), 1, (key, x) => x + 1);
                    }
                 });

                return wordCollection;
            }
            catch(Exception)
            {
                throw;
            }

        }
        /// <summary>
        ///  Function retun top 10 work by count
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IDictionary<string,int> Top10WordByCount(string filePath)
        {
            try
            {
                IEnumerable<string> fileLines = _fileReader.FileReader(filePath);
                ConcurrentDictionary<string, int> wordCollection = FileWordsCount(fileLines);
                return wordCollection.OrderByDescending(x => x.Value).Take(10).ToDictionary(x => x.Key, x => x.Value);
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
