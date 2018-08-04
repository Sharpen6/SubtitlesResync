using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubtitlesResyncCore;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SF_Test_CTR_FromText()
        {
            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterEng.txt");
            string[] text = File.ReadAllLines(path);
            SubtitlesFile sf = new SubtitlesFile(text);
            Assert.AreEqual(1388, sf.Subtitles.Count);

            string path2 = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterHeb.txt");
            string[] text2 = File.ReadAllLines(path2);
            SubtitlesFile sf2 = new SubtitlesFile(text2);
            Assert.AreEqual(1267, sf2.Subtitles.Count);
        }

        [TestMethod]
        public void SF_Test_ExtractPauses()
        {
            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterEng.txt");
            string[] text = File.ReadAllLines(path);
            SubtitlesFile sf = new SubtitlesFile(text);
            List<Time> pauses = sf.TimeBetweenSubtitles();
            foreach (var pausa in pauses)
            {
                Console.WriteLine(pausa);
            }
        }

        [TestMethod]
        public void SF_Test_WriteFile()
        {
            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterEng.txt");
            string[] text = File.ReadAllLines(path);
            SubtitlesFile sf = new SubtitlesFile(text);
            string output = sf.ToString();
        }
    }
}
