using com.wer.sc.data.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestDataTransfer_Tick2KLine
    {
        [TestMethod]
        public void TestTransferNight()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            TickData data = ResourceLoader.LoadTickData_AG05_20141230();
            List<TickData> dataList = new List<TickData>();
            dataList.Add(data);
            IKLineData klinedata = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            AssertResult(klinedata, Resources.AG05_20141230_Result);
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

        [TestMethod]
        public void TestTransferNight2()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023300 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            TickData data = ResourceLoader.LoadTickData_M05_20150106();
            List<TickData> dataList = new List<TickData>();
            dataList.Add(data);
            IKLineData klinedata = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            AssertResult(klinedata, Resources.M05_20150106_Result);
        }

        [TestMethod]
        public void TestTransfer_M01_20131202_20131213()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            ITickDataReader tickReader = fac.TickDataReader;
            IList<int> openDates = fac.OpenDateReader.GetOpenDates(20131202, 20131213);
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                TickData tickData = tickReader.GetTickData("m01", date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            AssertResult(data, Resources.Tick2Kline_M01_20131202_20131213);
        }

        [TestMethod]
        public void TestTransfer_M05_20131202_20131231()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            ITickDataReader tickReader = fac.TickDataReader;
            IList<int> openDates = fac.OpenDateReader.GetOpenDates(20131202, 20131231);
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                TickData tickData = tickReader.GetTickData("m05", date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            AssertResult(data, Resources.Tick2Kline_M05_20131202_20131231);
        }

        [TestMethod]
        public void TestTransfer_M01_20131202_20131213_15Second()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            ITickDataReader tickReader = fac.TickDataReader;
            IList<int> openDates = fac.OpenDateReader.GetOpenDates(20131202, 20131213);
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                TickData tickData = tickReader.GetTickData("m01", date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_SECOND, 15), openTime);
            AssertResult(data, Resources.Tick2Kline_M01_20131202_20131213_15Second);
            //String[] dataResults = ResourceLoader.GetKLineData_15Second_Result();
            ////Assert.AreEqual(data.Length, dataResults.Length);
            //for (int i = 0; i < data.Length; i++)
            //{
            //    data.BarPos = i;
            //    //Assert.AreEqual(dataResults[i], data.ToString());
            //    Console.WriteLine(data);
            //}
        }
    }
}
