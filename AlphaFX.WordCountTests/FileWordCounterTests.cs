using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaFX.WordCount;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Unity;
using System.IO;

namespace AlphaFX.WordCount.Tests
{
    [TestClass()]
    public class FileWordCounterTests
    {
        IUnityContainer container;
        IFileReader fileReader;
        IFileWordCounter fileWordCounter;

        [TestInitialize]
        public void Setup()
        {
            // IOC design pattern using Unity framwork
            container = new UnityContainer();
            container.RegisterType<IFileReader, TextFileReader>();
            container.RegisterType<IFileWordCounter, FileWordCounter>();

            fileReader = container.Resolve<IFileReader>();
            fileWordCounter = container.Resolve<IFileWordCounter>();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "file not exists exception")]
        public void FileNotExistWordCountTest()
        {
            IList<string> filelines = null;
            var actualObject = fileWordCounter.FileWordsCount(filelines);
            var expectedObject = new ConcurrentDictionary<string, int>();
        }

        [TestMethod()]
        public void EmptyFileWordCountTest()
        {
            IList<string> filelines = new List<string>();

            var actualObject = fileWordCounter.FileWordsCount(filelines);
            var expectedObject = new ConcurrentDictionary<string, int>();
            Assert.IsTrue(Equals(expectedObject, actualObject));
        }


        [TestMethod()]
        public void FileWordCountTest()
        {
            IList<string> filelines = new List<string>
            {
                "ALPHAFX Test to show skills and technical capability for candidate",
                "ALPHAFX Test to show skills and technical",
                "ALPHAFX Test to show skills",
            };

            var actualObject = fileWordCounter.FileWordsCount(filelines);
            var expectedObject = new ConcurrentDictionary<string, int>();
            expectedObject.TryAdd("alphafx", 3);
            expectedObject.TryAdd("test", 3);
            expectedObject.TryAdd("to", 3);
            expectedObject.TryAdd("show", 3);
            expectedObject.TryAdd("skills", 3);
            expectedObject.TryAdd("and", 2);
            expectedObject.TryAdd("technical", 2);
            expectedObject.TryAdd("capability", 1);
            expectedObject.TryAdd("for", 1);
            expectedObject.TryAdd("candidate", 1);

            Assert.IsTrue(Equals(expectedObject, actualObject));
        }

        [TestMethod()]
        public void FileWordCountTest3()
        {
            IList<string> filelines = new List<string>
            {
                "ALPHAFX Test ALPHAFX Test ALPHAFX Test",
                "",
                "******",
                "&&&&&&"
            };

            var actualObject = fileWordCounter.FileWordsCount(filelines);
            var expectedObject = new ConcurrentDictionary<string, int>();
            expectedObject.TryAdd("alphafx", 3);
            expectedObject.TryAdd("test", 3);
            expectedObject.TryAdd("******", 1);
            expectedObject.TryAdd("&&&&&&", 1);
                     
            Assert.IsTrue(Equals(expectedObject, actualObject));
        }


        [TestMethod()]
        public void Top10WordByCountTest()
        {
            string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "TestFile", "InputFile1.txt");
            var actualObject = fileWordCounter.Top10WordByCount(filePath);
            var expectedObject = new ConcurrentDictionary<string, int>();
            expectedObject.TryAdd("seven", 7);
            expectedObject.TryAdd("six", 6);
            expectedObject.TryAdd("five", 5);
            expectedObject.TryAdd("four", 4);
            expectedObject.TryAdd("three", 3);
            expectedObject.TryAdd("two", 2);
            expectedObject.TryAdd("one", 1);

            Assert.IsTrue(Equals(expectedObject, actualObject));
        }

        [TestMethod()]
        public void Top10WordByCountTest2()
        {
             string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "TestFile", "InputFile.txt");
             var actualObject = fileWordCounter.Top10WordByCount(filePath);

            var expectedObject = new ConcurrentDictionary<string, int>();
            expectedObject.TryAdd("the", 11677);
            expectedObject.TryAdd("and", 7353);
            expectedObject.TryAdd("of", 5070);
            expectedObject.TryAdd("to", 3899);
            expectedObject.TryAdd("a", 3689);
            expectedObject.TryAdd("he", 2894);
            expectedObject.TryAdd("in", 2882);
            expectedObject.TryAdd("was", 2431);
            expectedObject.TryAdd("that", 2339);
            expectedObject.TryAdd("i", 2194);
            Assert.IsTrue(Equals(expectedObject, actualObject));
        }

        private bool Equals<TKey, TValue>(IDictionary<TKey, TValue> x, IDictionary<TKey, TValue> y)
        {
            // early-exit checks
            if (null == y)
                return null == x;
            if (null == x)
                return false;
            if (object.ReferenceEquals(x, y))
                return true;
            if (x.Count != y.Count)
                return false;

            // check keys are the same
            foreach (TKey k in x.Keys)
                if (!y.ContainsKey(k))
                    return false;

            // check values are the same
            foreach (TKey k in x.Keys)
                if (!x[k].Equals(y[k]))
                    return false;

            return true;
        }

        
    }
}