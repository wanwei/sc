using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestDataTransfer_Tick2KLine
    {
        [TestMethod]
        public void TestKLineChartGen()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            TickData tickData = dataProvider.GetTickData("m01", 20131202);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            List<double> timePeriods = TimeUtils.GetKLineTimes(openTime, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));

            List<KLineChart2> charts = KLineChartGen.GenerateCharts(tickData, timePeriods, -1);
            Assert.AreEqual("20131202.09,3730,3739,3730,3736,8154,0,719974", charts[0].ToString());
            Assert.AreEqual("20131202.0901,3736,3736,3735,3735,1568,0,720216", charts[1].ToString());
            Assert.AreEqual("20131202.103,3746,3746,3744,3745,4042,0,712060", charts[75].ToString());
            Assert.AreEqual("20131202.133,3743,3744,3742,3744,2362,0,702416", charts[135].ToString());
            Assert.AreEqual("20131202.1459,3744,3745,3743,3744,2436,0,675816", charts[224].ToString());
        }

        [TestMethod]
        public void TestKLineGen_Err()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            TickData tickData = dataProvider.GetTickData("m05", 20040630);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            List<double> timePeriods = TimeUtils.GetKLineTimes(openTime, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));

            List<KLineChart2> charts = KLineChartGen.GenerateCharts(tickData, timePeriods, 2262);
            String[] result = ResourceLoader.GetResultData("m05_20040630_1minute.csv");
            for(int i = 0; i < charts.Count; i++)
            {
                //Console.WriteLine(charts[i]);
                Assert.AreEqual(result[i], charts[i].ToString());
            }
        }

        [TestMethod]
        public void TestTransferNight()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            TickData data = ResourceLoader.LoadTickData2();
            List<TickData> dataList = new List<TickData>();
            dataList.Add(data);
            IKLineData klinedata = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            for (int i = 0; i < klinedata.Length; i++)
            {
                klinedata.BarPos = i;
                Console.WriteLine(klinedata);
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
            TickData data = ResourceLoader.LoadTickData_Night();
            List<TickData> dataList = new List<TickData>();
            dataList.Add(data);
            IKLineData klinedata = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            for (int i = 0; i < klinedata.Length; i++)
            {
                klinedata.BarPos = i;
                Console.WriteLine(klinedata);
            }
        }

        [TestMethod]
        public void TestTransfer_M01()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            dataProvider.Append = true;
            List<int> dates = dataProvider.GetOpenDates();
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < 10; i++)
            {
                int date = dates[i];
                TickData tickData = dataProvider.GetTickData("m01", date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            String[] dataResults = ResourceLoader.GetKLineData_1Min_Result();
            Assert.AreEqual(data.Length, dataResults.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                Assert.AreEqual(dataResults[i], data.ToString());
            }
        }

        [TestMethod]
        public void TestTransfer()
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
            IKLineData data = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
            //for (int i = 0; i < data.Length; i++)
            //{
            //    data.BarPos = i;
            //    Console.WriteLine(data);
            //}
            Assert.AreEqual("20131216.09,3341,3341,3336,3340,24580,0,1654950", data.ToString());
            data.BarPos = data.Length - 1;
            Assert.AreEqual("20131231.1459,3366,3368,3366,3368,10578,0,1556822", data.ToString());
        }

        [TestMethod]
        public void TestTransfer_15Second()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            dataProvider.Append = true;
            List<int> dates = dataProvider.GetOpenDates();
            List<TickData> dataList = new List<TickData>();
            for (int i = 0; i < 10; i++)
            {
                int date = dates[i];
                TickData tickData = dataProvider.GetTickData("m01", date);
                if (tickData == null)
                    continue;
                dataList.Add(tickData);
            }

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            IKLineData data = DataTransfer_Tick2KLine.Transfer(dataList, new KLinePeriod(KLinePeriod.TYPE_SECOND, 15), openTime);
            String[] dataResults = ResourceLoader.GetKLineData_15Second_Result();
            Assert.AreEqual(data.Length, dataResults.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                Assert.AreEqual(dataResults[i], data.ToString());
                //Console.WriteLine(data);
            }
        }
    }
}
