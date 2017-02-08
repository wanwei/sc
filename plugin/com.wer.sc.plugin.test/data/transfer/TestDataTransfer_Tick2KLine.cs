using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.mockdata;
using com.wer.sc.data;
using com.wer.sc.plugin.Properties;
using System.Collections.Generic;
using com.wer.sc.data.opentime;

namespace com.wer.sc.data.transfer
{
    [TestClass]
    public class TestDataTransfer_Tick2KLine
    {
        private static IKLineTimeListGetter klineTimeListGetter = GetTimeListGetter();

        private static IKLineTimeListGetter GetTimeListGetter()
        {
            List<int> openDates = MockData_OpenDate.GetAllOpenDates();
            IOpenDateReader opendatecache = new OpenDateCache(openDates);
            IOpenTimeReader opentimereader = new MockOpenTimeReader();
            KLineTimeListGetter getter = new KLineTimeListGetter(opendatecache, opentimereader);
            return getter;
        }

        [TestMethod]
        public void TestTransfer_M01_20131202()
        {
            int date = 20131202;
            ITickData tickData = MockData_Tick.GetTickData("m01", date);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(date, tickData, KLinePeriod.KLinePeriod_1Minute, klineTimeListGetter, -1, -1);
            AssertUtils.AssertKLineDataResult(data, Resources.Tick2KLine_M01_20131202);
        }

        [TestMethod]
        public void TestTransfer_M05_20040129()
        {
            int date = 20040129;
            ITickData tickData = MockData_Tick.GetTickData("m05", date);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(date, tickData, KLinePeriod.KLinePeriod_1Minute, klineTimeListGetter, -1, -1);
            AssertUtils.AssertKLineDataResult(data, Resources.Tick2KLine_M05_20040129);
        }

        //[TestMethod]
        //public void TestTransferNight2()
        //{
        //    List<double[]> openTime = new List<double[]>();
        //    openTime.Add(new double[] { .210000, .023300 });
        //    openTime.Add(new double[] { .090000, .101500 });
        //    openTime.Add(new double[] { .103000, .113000 });
        //    openTime.Add(new double[] { .133000, .150000 });
        //    TickData data = ResourceLoader.LoadTickData_M05_20150106();
        //    List<TickData> dataList = new List<TickData>();
        //    dataList.Add(data);
        //    IKLineData klinedata = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLineTimeType.MINUTE, 1), openTime);
        //    AssertResult(klinedata, Resources.M05_20150106_Result);
        //}

        [TestMethod]
        public void TestTransfer_M01_20131202_20131213()
        {
            IList<int> openDates = MockData_OpenDate.GetOpenDates(20131202, 20131213);
            List<ITickData> dataList = new List<ITickData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                ITickData tickData = MockData_Tick.GetTickData("m01", date);
                dataList.Add(tickData);
            }
            IKLineData data = DataTransfer_Tick2KLine.Transfer(openDates, dataList, KLinePeriod.KLinePeriod_1Minute, klineTimeListGetter, -1, -1);
            AssertUtils.AssertKLineDataResult(data, Resources.Tick2Kline_M01_20131202_20131213);
        }

        [TestMethod]
        public void TestTransfer_M05_20131202_20131231()
        {
            IList<int> openDates = MockData_OpenDate.GetOpenDates(20131202, 20131231);
            List<ITickData> dataList = new List<ITickData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                ITickData tickData = MockData_Tick.GetTickData("m05", date);
                dataList.Add(tickData);
            }
            IKLineData data = DataTransfer_Tick2KLine.Transfer(openDates, dataList, KLinePeriod.KLinePeriod_1Minute, klineTimeListGetter, -1, -1);
            AssertUtils.AssertKLineDataResult(data, Resources.Tick2Kline_M05_20131202_20131231);
        }

        [TestMethod]
        public void TestTransfer_M01_20131202_20131213_15Second()
        {
            IList<int> openDates = MockData_OpenDate.GetOpenDates(20131202, 20131213);
            List<ITickData> dataList = new List<ITickData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                ITickData tickData = MockData_Tick.GetTickData("m01", date);
                dataList.Add(tickData);
            }
            IKLineData data = DataTransfer_Tick2KLine.Transfer(openDates, dataList, new KLinePeriod(KLineTimeType.SECOND, 15), GetTimeListGetter(), -1, -1);
            AssertUtils.AssertKLineDataResult(data, Resources.Tick2Kline_M01_20131202_20131213_15Second);
        }

        /// <summary>
        /// 处理
        /// </summary>
        [TestMethod]
        public void TestTransfer_M01_20040102_20040301()
        {
            IList<int> openDates = MockData_OpenDate.GetOpenDates(20040102, 20040301);
            List<ITickData> dataList = new List<ITickData>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                ITickData tickData = MockData_Tick.GetTickData("m01", date);
                dataList.Add(tickData);
            }
            KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
            IKLineData data = DataTransfer_Tick2KLine.Transfer(openDates, dataList,period , GetTimeListGetter(), -1, -1);
            //for(int i = 0; i < data.Length; i++)
            //{
            //    data.BarPos = i;
            //    Console.WriteLine(data);
            //}
            AssertUtils.AssertKLineDataResult(data, Resources.Tick2Kline_M01_20040102_20040301);
        }
    }

    class MockOpenTimeReader : IOpenTimeReader
    {
        public List<double[]> GetOpenTime(string code, int date)
        {
            return MockData_OpenTime.GetOpenTime(code, date);
        }
    }
}
