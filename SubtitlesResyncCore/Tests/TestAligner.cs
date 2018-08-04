using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubtitlesResyncCore;

namespace Tests
{
    [TestClass]
    public class TestAligner
    {
        [TestMethod]
        public void A_TestGradeAlignment()
        {
            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterEng.txt");
            string[] text = File.ReadAllLines(path);

            string path2 = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterHeb.txt");
            string[] text2 = File.ReadAllLines(path2);

            SubtitlesFile sf1 = new SubtitlesFile(text);
            SubtitlesFile sf2 = new SubtitlesFile(text2);

            SubsAligner pa = new SubsAligner(sf1, sf2);
            int err_score = pa.GetScore();
            Assert.AreNotEqual(0, err_score);
        }

        [TestMethod]
        public void A_TestApplyFix()
        {

            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterEng.txt");
            string[] text = File.ReadAllLines(path);

            string path2 = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterHeb.txt");
            string[] text2 = File.ReadAllLines(path2);

            SubtitlesFile sf1 = new SubtitlesFile(text);
            SubtitlesFile sf2 = new SubtitlesFile(text2);

            SubsAligner pa = new SubsAligner(sf1, sf2);

            int before_err_score = pa.GetScore();
            pa.ApplyChange(2000, 1);
            int after_err_score = pa.GetScore();
            Assert.IsTrue(before_err_score > after_err_score);
        }

        [TestMethod]
        public void A_TestAlign()
        {
            string path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterEng.txt");
            string[] text = File.ReadAllLines(path);

            string path2 = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "ShapeOfWaterHeb.txt");
            string[] text2 = File.ReadAllLines(path2);

            SubtitlesFile sf1 = new SubtitlesFile(text);
            SubtitlesFile sf2 = new SubtitlesFile(text2);

            SubsAligner pa = new SubsAligner(sf1, sf2);

            int currScore = pa.GetScore();
            int first_score = currScore;
            int prev_score = currScore;
            while (true)
            {
                pa.ApplyChange(100, 1);
                currScore = pa.GetScore();
                if (currScore > prev_score)
                {
                    pa.ApplyChange(-100, 1);
                    break;
                }
                else
                {
                    prev_score = currScore;
                }
            }

            int final_score = pa.GetScore();
            Assert.IsTrue(first_score > final_score);
        }
    }
}
