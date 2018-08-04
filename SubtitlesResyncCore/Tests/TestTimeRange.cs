using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubtitlesResyncCore;

namespace Tests
{
    [TestClass]
    public class TestTimeRange
    {
        [TestMethod]
        public void TR_Test_CTR_FromString()
        {
            string input = "01:50:56,072 --> 01:50:58,675";
            TimeRange tr = new TimeRange(input);
            Assert.AreEqual(tr.ToString(), input);
        }

        [TestMethod]
        public void TR_Test_CTR_FromTwoTimes()
        {
            Time input1 = new Time("01:50:46,464");
            Time input2 = new Time("01:50:47,564");

            TimeRange tr1 = new TimeRange(input1,input2);
            Assert.AreEqual(tr1.ToString(), "01:50:46,464 --> 01:50:47,564");
        }
    }
}
