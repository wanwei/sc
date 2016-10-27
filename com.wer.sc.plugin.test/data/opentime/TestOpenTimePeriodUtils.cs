using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using com.wer.sc.plugin.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.opentime
{
    /// <summary>
    /// 五个测试用例：
    /// 1.普通交易日
    /// 2.有夜盘无过夜交易日
    /// 3.有夜盘过夜交易日
    /// 4.有夜盘跨周无过夜交易日
    /// 5.有夜盘扩周有过夜交易日
    /// </summary>
    [TestClass]
    public class TestOpenTimePeriodUtils
    {
        [TestMethod]
        public void TestGetOpenTimePeriod_M01_20131202_1Minute()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20131202, OpenDateReader, OpenTime_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_M01_20131202);            
        }

        [TestMethod]
        public void TestGetOpenTimePeriod_M01_20141229_5Second()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20141229, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_5Second);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_M05_20141229_5Second);
        }

        [TestMethod]
        public void TestGetOpenTimePeriod_M05_20141229_1Minute()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20141229, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_M05_20141229);
        }

        [TestMethod]
        public void TestGetOpenTimePeriod_M05_20141230_1Minute()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20141230, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_M05_20141230);
        }

        [TestMethod]
        public void TestGetOpenTimePeriod_M05_20150624_1Minute()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20150624, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_M05_20150624);
        }

        [TestMethod]
        public void TestGetOpenTimePeriod_M05_20150629_1Minute()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20150629, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_M05_20150629);
        }

        [TestMethod]
        public void TestGetOpenTimePeriods_Normal()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20100105, OpenDateReader, OpenTime_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_Normal);
        }

        [TestMethod]
        public void TestGetOpenTimePeriods_Night_Normal()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20150701, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_Night_Normal);
        }

        [TestMethod]
        public void TestGetOpenTimePeriods_Night_OverNight()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20150106, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_OverNight);
        }

        [TestMethod]
        public void TestGetOpenTimePeriods_NightNormal_WeekStart()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20150727, OpenDateReader, OpenTime_Night_Normal, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_NightNormal_WeekStart);
            //for (int i = 0; i < klineTimes.Count; i++)
            //    Console.WriteLine(klineTimes[i]);
        }

        [TestMethod]
        public void TestGetOpenTimePeriods_NightOverNight_WeekStart()
        {
            List<double> klineTimes = OpenTimePeriodUtils.GetKLineTimeList(20150112, OpenDateReader, OpenTime_Night_OverNight, KLinePeriod.KLinePeriod_1Minute);
            AssertOpenTime(klineTimes, Resources.OpenTimePeriod_OverNight_WeekStart);
        }

        private void AssertOpenTime(List<double> klineTimes, String resource)
        {
            string[] lines = resource.Split('\r');
            Assert.AreEqual(lines.Length, klineTimes.Count);
            for (int i = 0; i < klineTimes.Count; i++)
            {
                Assert.AreEqual(double.Parse(lines[i]), klineTimes[i]);
            }
        }

        private List<double[]> OpenTime_Normal
        {
            get
            {
                List<double[]> openTime = new List<double[]>();
                openTime.Add(new double[] { .090000, .101500 });
                openTime.Add(new double[] { .103000, .113000 });
                openTime.Add(new double[] { .133000, .150000 });
                return openTime;
            }
        }

        private List<double[]> OpenTime_Night_Normal
        {
            get
            {
                List<double[]> openTime = new List<double[]>();
                openTime.Add(new double[] { .210000, .233000 });
                openTime.Add(new double[] { .090000, .101500 });
                openTime.Add(new double[] { .103000, .113000 });
                openTime.Add(new double[] { .133000, .150000 });
                return openTime;
            }
        }

        private List<double[]> OpenTime_Night_OverNight
        {
            get
            {
                List<double[]> openTime = new List<double[]>();
                openTime.Add(new double[] { .210000, .023000 });
                openTime.Add(new double[] { .090000, .101500 });
                openTime.Add(new double[] { .103000, .113000 });
                openTime.Add(new double[] { .133000, .150000 });
                return openTime;
            }
        }

        private OpenDateCache opendateCache = new OpenDateCache(MockData_OpenDate.GetAllOpenDates());

        private IOpenDateReader OpenDateReader
        {
            get
            {
                return opendateCache;
            }
        }
    }
}
