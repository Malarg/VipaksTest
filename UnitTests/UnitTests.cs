using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VipaksPlusTestTask;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void FistTest()
        {
            var parser = new FilesParser();
            var directory = Directory.GetCurrentDirectory() + "\\Test";
            parser.CalculateFrequencies(directory);
            while(!parser.IsParsingFinished)
            {

            }
            Assert.IsNotNull(parser);
            Assert.IsNotNull(parser.WordsFrequency);
            Assert.IsTrue(Directory.GetFiles(directory).Length.Equals(4));
            Assert.IsTrue(parser.WordsFrequency.Count.Equals(10));
            Assert.IsTrue(parser.WordsFrequency["мама"].Frequency.Equals(3));
            Assert.IsTrue(parser.WordsFrequency["раму"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["крыла"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["крышу"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["Шила"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["пальтишко"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["Рыла"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["яму"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["Мама"].Frequency.Equals(1));
            Assert.IsTrue(parser.WordsFrequency["мыла"].Frequency.Equals(1));
        }
    }
}
