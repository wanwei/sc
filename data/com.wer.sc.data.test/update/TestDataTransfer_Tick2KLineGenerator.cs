using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
using com.wer.sc.data.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.update
{
    [TestClass]
    public class TestDataTransfer_Tick2KLineGenerator
    {
        [TestMethod]
        public void TestTick2Charts_Normal()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            TickData tickData = fac.TickDataReader.GetTickData("m01", 20131202);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(tickData, openTime, new KLinePeriod(KLineTimeType.MINUTE, 1), -1);
            AssertTick2Charts(charts, Resources.Tick2Charts_M01_20131202);
        }

        [TestMethod]
        public void TestTick2Charts_Night()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            TickData tickData = fac.TickDataReader.GetTickData("m05", 20141230);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(tickData, openTime, new KLinePeriod(KLineTimeType.MINUTE, 1), -1);
            AssertTick2Charts(charts, Resources.Tick2Charts_M05_20141230);
        }

        [TestMethod]
        public void TestTick2Charts_Night2()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();

            TickData tickData = fac.TickDataReader.GetTickData("m05", 20150624);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(tickData, openTime, new KLinePeriod(KLineTimeType.MINUTE, 1), -1);
            AssertTick2Charts(charts, Resources.Tick2Charts_M05_20150624);
        }

        [TestMethod]
        public void TestTick2Charts_WeekendNight()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            TickData tickData = fac.TickDataReader.GetTickData("m05", 20141229);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(tickData, openTime, new KLinePeriod(KLineTimeType.MINUTE, 1), -1);
            //for (int i = 0; i < charts.Count; i++)
            //    Console.WriteLine(charts[i]);
            AssertTick2Charts(charts, Resources.Tick2Charts_M05_20141229);

            //List<double> periods = DataTransfer_Tick2KLineGenerator.GetTodayTimePeriods(tickData, openTime);
            //AssertTimePeriods(periods, Resources.TimePeriod_M05_20141229);
        }

        [TestMethod]
        public void TestTick2Charts_Limit()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            TickData tickData = fac.TickDataReader.GetTickData("m05", 20150504);

            List<double[]> openTime = new List<double[]>();
            //openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(tickData, openTime, new KLinePeriod(KLineTimeType.MINUTE, 1), -1);
            //for (int i = 0; i < charts.Count; i++)
            //    Console.WriteLine(charts[i]);
            AssertTick2Charts(charts, Resources.Tick2Charts_M05_20150504);
        }

        [TestMethod]
        public void TestTick2Charts_Limit_Night()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            TickData tickData = fac.TickDataReader.GetTickData("m05", 20150506);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(tickData, openTime, new KLinePeriod(KLineTimeType.MINUTE, 1), -1);
            //for (int i = 0; i < charts.Count; i++)
            //    Console.WriteLine(charts[i]);
            AssertTick2Charts(charts, Resources.Tick2Charts_M05_20150506);
        }

        [TestMethod]
        public void TestTick2Charts_Limit_Early()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            TickData tickData = dataProvider.GetTickData("m05", 20040630);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            List<double> timePeriods = TimeUtils.GetKLineTimes(openTime, new KLinePeriod(KLineTimeType.MINUTE, 1));

            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(tickData, openTime, new KLinePeriod(KLineTimeType.MINUTE, 1), 2262);
            AssertTick2Charts(charts, Resources.Tick2Charts_M05_20040630);
        }

        private void AssertTick2Charts(List<KLineBar> charts, string txt)
        {
            string[] periodArr = txt.Split('\r');
            for (int i = 0; i < charts.Count; i++)
            {
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, charts[i].ToString());
            }
        }
    }
}