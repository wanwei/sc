using com.wer.sc.plugin.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestTimeUtils
    {

        [TestMethod]
        public void TestTimeUtils_Convert()
        {
            double d = 20140101;
            DateTime dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.1;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.09;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.092;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.0925;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.09251;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.092501;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

        }

        [TestMethod]
        public void TestDateTime()
        {
            DateTime dt = Convert.ToDateTime("2014-09-12 09:30:05");
            Console.WriteLine(string.Format("{0:yyyyMMddHHmmss}", dt));

            Double d = Double.Parse(string.Format("{0:yyyyMMdd.HHmmss}", dt));
            Console.WriteLine(d);
        }

        [TestMethod]
        public void TestTimeUtils_Add()
        {
            Assert.AreEqual(20140912.10, TimeUtils.AddSeconds(20140912.095955, 5));
        }

        [TestMethod]
        public void TestGetKLineTimes_DayOpenTime()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
            KLineOpenPeriods dayOpenTime = TimeUtils.GetKLineTimes_DayOpenTime(openTime, period);
            Assert.AreEqual(0, dayOpenTime.SplitIndeies[0]);
            Assert.AreEqual(75, dayOpenTime.SplitIndeies[1]);
            Assert.AreEqual(135, dayOpenTime.SplitIndeies[2]);
            Assert.AreEqual(-1, dayOpenTime.OverNightIndex);
            AssertOpenTime(dayOpenTime.KlineTimes, Resources.OpenTime_Normal_1Minute);
        }

        [TestMethod]
        public void TestGetKLineTimes_DayOpenTime_Night_Normal()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
            KLineOpenPeriods dayOpenTime = TimeUtils.GetKLineTimes_DayOpenTime(openTime, period);

            //AssertOpenTime(dayOpenTime.KlineTimes, Resources.OpenTime_OverNight_1Minute);
            Assert.AreEqual(150, dayOpenTime.OverNightIndex);

            Assert.AreEqual(0, dayOpenTime.SplitIndeies[0]);
            Assert.AreEqual(150, dayOpenTime.SplitIndeies[1]);
            Assert.AreEqual(225, dayOpenTime.SplitIndeies[2]);
            Assert.AreEqual(285, dayOpenTime.SplitIndeies[3]);
        }

        [TestMethod]
        public void TestGetKLineTimes_DayOpenTime_OverNight()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
            KLineOpenPeriods dayOpenTime = TimeUtils.GetKLineTimes_DayOpenTime(openTime, period);

            AssertOpenTime(dayOpenTime.KlineTimes, Resources.OpenTime_OverNight_1Minute);
            Assert.AreEqual(180, dayOpenTime.OverNightIndex);

            //for (int i = 0; i < dayOpenTime.SplitIndeies.Count; i++)
            //    Console.WriteLine(dayOpenTime.SplitIndeies[i]);

            Assert.AreEqual(0, dayOpenTime.SplitIndeies[0]);
            Assert.AreEqual(330, dayOpenTime.SplitIndeies[1]);
            Assert.AreEqual(405, dayOpenTime.SplitIndeies[2]);
            Assert.AreEqual(465, dayOpenTime.SplitIndeies[3]);
        }

        private void AssertOpenTime(List<double> openTime, String result)
        {
            string[] lines = result.Split('\r');
            Assert.AreEqual(lines.Length, openTime.Count);
            for (int i = 0; i < openTime.Count; i++)
            {
                Assert.AreEqual(double.Parse(lines[i]), openTime[i]);
            }
        }

        [TestMethod]
        public void TestGetKLineTimes()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            KLinePeriod period = new KLinePeriod(KLineTimeType.MINUTE, 1);
            List<double> klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(225, klineTimes.Count);
            Assert.AreEqual(0.09, klineTimes[0]);
            Assert.AreEqual(0.103, klineTimes[75]);
            Assert.AreEqual(0.133, klineTimes[135]);

            period = new KLinePeriod(KLineTimeType.SECOND, 5);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(2700, klineTimes.Count);

            period = new KLinePeriod(KLineTimeType.MINUTE, 5);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(45, klineTimes.Count);
            //for (int i = 0; i < klineTimes.Count; i++)
            //    Console.WriteLine(klineTimes[i]);

            period = new KLinePeriod(KLineTimeType.MINUTE, 15);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(15, klineTimes.Count);
            //for (int i = 0; i < klineTimes.Count; i++)
            //    Console.WriteLine(klineTimes[i]);

            period = new KLinePeriod(KLineTimeType.HOUR, 1);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(4, klineTimes.Count);
            Assert.AreEqual(0.09, klineTimes[0]);
            Assert.AreEqual(0.1, klineTimes[1]);
            Assert.AreEqual(0.1115, klineTimes[2]);
            Assert.AreEqual(0.1415, klineTimes[3]);

            openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            openTime.Add(new double[] { .210000, .233000 });
            period = new KLinePeriod(KLineTimeType.HOUR, 1);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(7, klineTimes.Count);
            Assert.AreEqual(0.09, klineTimes[0]);
            Assert.AreEqual(0.1, klineTimes[1]);
            Assert.AreEqual(0.1115, klineTimes[2]);
            Assert.AreEqual(0.1415, klineTimes[3]);
            Assert.AreEqual(0.21, klineTimes[4]);
            Assert.AreEqual(0.22, klineTimes[5]);
            Assert.AreEqual(0.23, klineTimes[6]);

            openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .023000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            period = new KLinePeriod(KLineTimeType.HOUR, 1);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            //for(int i = 0; i < klineTimes.Count; i++)            
            //    Console.WriteLine(klineTimes[i]);
            Assert.AreEqual(10, klineTimes.Count);
            Assert.AreEqual(0.21, klineTimes[0]);
            Assert.AreEqual(0.22, klineTimes[1]);
            Assert.AreEqual(0.23, klineTimes[2]);
            Assert.AreEqual(0, klineTimes[3]);
            Assert.AreEqual(0.01, klineTimes[4]);
            Assert.AreEqual(0.02, klineTimes[5]);
            Assert.AreEqual(0.09, klineTimes[6]);

            openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .230000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            period = new KLinePeriod(KLineTimeType.MINUTE, 1);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            for (int i = 0; i < klineTimes.Count; i++)
                Console.WriteLine(klineTimes[i]);
            Assert.AreEqual(345, klineTimes.Count);
            //Assert.AreEqual(0.21, klineTimes[0]);
            //Assert.AreEqual(0.22, klineTimes[1]);
            //Assert.AreEqual(0.23, klineTimes[2]);
            //Assert.AreEqual(0, klineTimes[3]);
            //Assert.AreEqual(0.01, klineTimes[4]);
            //Assert.AreEqual(0.02, klineTimes[5]);
            //Assert.AreEqual(0.09, klineTimes[6]);
        }
    }
}