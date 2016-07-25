using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test
{
    [TestClass]
    public class TestTime
    {

        [TestMethod]
        public void TestConvert()
        {
            double d = 20140101;
            DateTime dt = TimeConvert.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeConvert.ConvertToDoubleTime(dt));

            d = 20140101.1;
            dt = TimeConvert.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeConvert.ConvertToDoubleTime(dt));

            d = 20140101.09;
            dt = TimeConvert.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeConvert.ConvertToDoubleTime(dt));

            d = 20140101.092;
            dt = TimeConvert.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeConvert.ConvertToDoubleTime(dt));

            d = 20140101.0925;
            dt = TimeConvert.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeConvert.ConvertToDoubleTime(dt));

            d = 20140101.09251;
            dt = TimeConvert.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeConvert.ConvertToDoubleTime(dt));

            d = 20140101.092501;
            dt = TimeConvert.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeConvert.ConvertToDoubleTime(dt));

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
        public void TestAdd()
        {
            Assert.AreEqual(20140912.10, TimeUtils.AddSeconds(20140912.095955, 5));
        }

        [TestMethod]
        public void TestGetKLineTimes()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            KLinePeriod period = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1);
            List<double> klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(225, klineTimes.Count);
            Assert.AreEqual(0.09, klineTimes[0]);
            Assert.AreEqual(0.103, klineTimes[75]);
            Assert.AreEqual(0.133, klineTimes[135]);

            period = new KLinePeriod(KLinePeriod.TYPE_SECOND, 5);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(2700, klineTimes.Count);

            period = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 5);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(45, klineTimes.Count);
            //for (int i = 0; i < klineTimes.Count; i++)
            //    Console.WriteLine(klineTimes[i]);

            period = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15);
            klineTimes = TimeUtils.GetKLineTimes(openTime, period);
            Assert.AreEqual(15, klineTimes.Count);
            //for (int i = 0; i < klineTimes.Count; i++)
            //    Console.WriteLine(klineTimes[i]);

            period = new KLinePeriod(KLinePeriod.TYPE_HOUR, 1);
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
            period = new KLinePeriod(KLinePeriod.TYPE_HOUR, 1);
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
            period = new KLinePeriod(KLinePeriod.TYPE_HOUR, 1);
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
            period = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1);
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