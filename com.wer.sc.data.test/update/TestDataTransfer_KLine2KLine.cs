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
            for(int i=0;i< data_1min.Length;i++)
            {
                data_1min.BarPos = i;
                Console.WriteLine(data_1min);
            }
            //Assert.AreEqual("20131216,3341,3364,3335,3348,938428,0,1659396", data.ToString());
            //data.BarPos = 1;
            //Assert.AreEqual("20131217,3365,3395,3362,3386,1231522,0,1625218", data.ToString());
            //data.BarPos = 2;
            //Assert.AreEqual("20131218,3400,3406,3383,3393,989908,0,1617380", data.ToString());
            //data.BarPos = 3;
            //Assert.AreEqual("20131219,3371,3383,3367,3381,765966,0,1616592", data.ToString());
            //data.BarPos = 11;
            //Assert.AreEqual("20131231,3352,3373,3350,3368,1049534,0,1556822", data.ToString());
        }

        public static IKLineData GetKLineData_1Minute()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            dataProvider.Append = true;
            List<int> dates = dataProvider.GetOpenDates();
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                TickData tickData = dataProvider.GetTickData("m05", date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
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
