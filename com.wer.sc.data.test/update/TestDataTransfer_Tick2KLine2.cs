using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.update
{
    /// <summary>
    /// 测试用例：
    /// 目标是
    /// 1.
    /// </summary>
    [TestClass]
    public class TestDataTransfer_Tick2KLine2
    {
        [TestMethod]
        public void TestTransfer_M01_20131202()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            IHistoryDataReader_Tick tickReader = fac.TickDataReader;
            int date = 20131202;
            TickData tickData = tickReader.GetTickData("m01", date);
            ICommonDataReader_OpenDate openDateReader = fac.OpenDateReader;

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data = DataTransfer_Tick2KLine2.Transfer(20131202, tickData, openDateReader, openTime, KLinePeriod.KLinePeriod_1Minute, null);
            AssertResult(data, Resources.Tick2KLine_M01_20131202);
        }

        [TestMethod]
        public void TestTransfer_M05_20040129()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            IHistoryDataReader_Tick tickReader = fac.TickDataReader;
            int date = 20040129;
            TickData tickData = tickReader.GetTickData("m05", date);
            ICommonDataReader_OpenDate openDateReader = fac.OpenDateReader;

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data = DataTransfer_Tick2KLine2.Transfer(date, tickData, openDateReader, openTime, KLinePeriod.KLinePeriod_1Minute, null);
            AssertResult(data, Resources.Tick2KLine_M05_20040129);
        }

        private void AssertResult(IKLineData klineData, String txt)
        {
            string[] periodArr = txt.Split('\r');
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, klineData.ToString());
            }
        }
    }
}