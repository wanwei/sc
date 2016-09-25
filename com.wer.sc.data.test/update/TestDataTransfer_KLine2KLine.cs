using com.wer.sc.data.test;
using com.wer.sc.data.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestDataTransfer_KLine2KLine
    {
        [TestMethod]
        public void TestTransferKLine_Minute()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data_1min = DataTestUtils.GetKLineData("m05", 20131216, 20131231, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);

            //转换成5分钟线
            IKLineData data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 5));
            DataTestUtils.AssertKLineDataResult(data, Resources.Kline2Kline_M05_20131216_20131231_5Minute);

            //转换成15分钟
            data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15));
            DataTestUtils.AssertKLineDataResult(data, Resources.Kline2Kline_M05_20131216_20131231_15Minute);

            //转换成1小时
            data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLinePeriod.TYPE_HOUR, 1));
            DataTestUtils.AssertKLineDataResult(data, Resources.Kline2Kline_M05_20131216_20131231_1Hour);
        }

        [TestMethod]
        public void TestTransferKLine_Day()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data_1min = DataTestUtils.GetKLineData("m05", 20131216, 20131231, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);

            IKLineData data = DataTransfer_KLine2KLine.Transfer_Day(data_1min, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
            DataTestUtils.AssertKLineDataResult(data, Resources.Kline2kline_M05_20131216_20131231_Day);
        }

        [TestMethod]
        public void TestTransferKLine_DayOverNight()
        {
            IKLineData klineData = GetMaKLineData(20141215, 20150116);
            IKLineData data = DataTransfer_KLine2KLine.Transfer_Day(klineData, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
            AssertResult(data, Resources.Kline2Kline_M05_20141215_20150116_Day);
            //for (int i = 0; i < data.Length; i++)
            //{
            //    data.BarPos = i;
            //    Console.WriteLine(data);
            //}
        }

        private static IKLineData GetMaKLineData(int startDate, int endDate)
        {
            List<IKLineData> klineDataList = new List<IKLineData>();
            IList<int> openDates = ResourceLoader.GetDefaultDataReaderFactory().OpenDateReader.GetOpenDates(20141215, 20150116);
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                List<double[]> openTime = MockDataProvider_Abstract.GetOpenTime_M(openDates[i]);
                IKLineData data_1min = DataTestUtils.GetKLineData("m05", date, date, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
                klineDataList.Add(data_1min);
            }
            return KLineData.Merge(klineDataList);
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
