using System;
using SubtitlesResyncCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestTime
    {
        [TestMethod]
        public void T_TestToString()
        {
            Time t = new Time(0,14,49,555);
            Assert.AreEqual(t.ToString(), "00:14:49,555");
        }
        [TestMethod]
        public void T_TestDuration()
        {
            Time t1 = new Time(0, 14, 49, 555);
            Time t2 = new Time(0, 15, 52, 585);
            Time t3 = new Time(0, 1, 3, 30);

            Assert.AreEqual(Time.DurationBetween(t1, t2).ToString(), t3.ToString());
        }

        [TestMethod]
        public void T_TestEqual()
        {
            Time t1 = new Time(0, 01, 55, 555);
            Time t2 = new Time(0, 1, 55, 555);

            Assert.AreEqual(t1, t2);


            Time t3 = new Time(0, 2, 11, 234);
            Time t4 = new Time(0, 2, 11, 234);

            Assert.AreEqual(t3, t4);
        }

        [TestMethod]
        public void T_TestFromString()
        {
            string input1 = "00:01:58,073";
            string input2 = "01:51:48,923";

            Time t1 = new Time(input1);
            Assert.AreEqual(t1.ToString(), input1);
            Time t2 = new Time(input2);
            Assert.AreEqual(t2.ToString(), input2);
        }

        [TestMethod]
        public void T_TestToMilliseconds()
        {
            string input1 = "00:01:58,073";
            Time t1 = new Time(input1);
            Assert.AreEqual(118073, t1.TotalMilliseconds);
        }

        [TestMethod]
        public void T_TestFromMilliseconds()
        {
            string input1 = "00:01:58,073";
            Time t1 = new Time(input1);
            int input2 = 118073;
            Time t2 = new Time(input2);
            Assert.AreEqual(t1.ToString(), t2.ToString());
        }


    }
}
