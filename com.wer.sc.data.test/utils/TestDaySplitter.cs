using com.wer.sc.data.test;
using com.wer.sc.data.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestDaySplitter
    {
        [TestMethod]
        public void TestSplit_Normal()
        {
            List<SplitterResult> results = DaySplitter.Split(new MockTimeGetter("m05", 20131201, 20131231, DataTestUtils.GetOpenTimeNormal()));
            AssertResult(results, Resources.DaySplit_M05_20131201_20131231);
        }

        [TestMethod]
        public void TestSplit_Night()
        {
            List<SplitterResult> results = DaySplitter.Split(new MockTimeGetter("m05", 20150625, 20150715, DataTestUtils.GetOpenTimeNight()));
            AssertResult(results, Resources.DaySplit_M05_20150625_20150715);
        }

        [TestMethod]
        public void TestSplit_OverNightWeekend()
        {
            List<SplitterResult> results = DaySplitter.Split(new MockTimeGetter("m05", 20141229, 20150115, DataTestUtils.GetOpenTimeOverNight()));
            for (int i = 0; i < results.Count; i++)
                Console.WriteLine(results[i]);
        }

        [TestMethod]
        public void TestSplit_EndInNight()
        {
            IKLineData klineData = ResourceLoader.GetDefaultDataReaderFactory().KLineDataReader.GetData("m05", 20150105, 20150106, KLinePeriod.KLinePeriod_1Minute);
            IKLineData subData = klineData.Sub(0, 570);
            List<SplitterResult> results = DaySplitter.Split(new MockTimeGetter(subData));
            //for (int i = 0; i < results.Count; i++)
            //    Console.WriteLine(results[i]);
            Assert.AreEqual("20150105,0", results[0].ToString());
            Assert.AreEqual("20150106,225", results[1].ToString());
        }

        //[TestMethod]
        //public void TestSplit_Mix()
        //{
        //    List<SplitterResult> results = DaySpliter.Split(new MockTimeGetter("m05", 20141229, 20150115, DataTestUtils.GetOpenTimeOverNight()));
        //    for (int i = 0; i < results.Count; i++)
        //        Console.WriteLine(results[i]);
        //}

        private void AssertResult(List<SplitterResult> results, string txt)
        {
            string[] periodArr = txt.Split('\r');
            Assert.AreEqual(periodArr.Length, results.Count);
            for (int i = 0; i < results.Count; i++)
            {
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, results[i].ToString());
            }
        }
    }

    class MockTimeGetter : TimeGetter
    {
        private IKLineData klineData;
        public MockTimeGetter(string code, int start, int end, List<double[]> openTime)
        {
            this.klineData = DataTestUtils.GetKLineData(code, start, end, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
        }

        public MockTimeGetter(IKLineData klineData)
        {
            this.klineData = klineData;
        }

        public int Count
        {
            get
            {
                return klineData.Length;
            }
        }

        public double GetTime(int index)
        {
            return klineData.Arr_Time[index];
        }
    }
}
