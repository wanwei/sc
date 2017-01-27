using com.wer.sc.data.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.reader
{
    [TestClass]
    public class TestKLineDataReader
    {
        [TestMethod]
        public void TestKLineDataReader_GetData_20130101_20160101()
        {
            IHistoryDataReader_KLine klineDataReader = GetKLineDataReader();
            IKLineData klineData = klineDataReader.GetData("m05", 20130101, 20160101, KLinePeriod.KLinePeriod_1Minute);
            string[] lines = Resources.KLineData_M05_20130101_20151231_1Minute.Split('\r');
            Assert.AreEqual(lines.Length, klineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Assert.AreEqual(lines[i].Trim(), klineData.ToString());
                //Console.WriteLine(klineData);
            }
        }

        [TestMethod]
        public void TestKLineDataReaderGetAll()
        {
            IHistoryDataReader_KLine reader = GetKLineDataReader();
            IKLineData data_m01 = reader.GetAllData("m01", KLinePeriod.KLinePeriod_1Minute);

            IKLineData data_m03 = reader.GetAllData("m03", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m03);
            IKLineData data_m05 = reader.GetAllData("m05", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m05);
            IKLineData data_m07 = reader.GetAllData("m07", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m07);
            IKLineData data_m08 = reader.GetAllData("m08", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m08);
            IKLineData data_m09 = reader.GetAllData("m09", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m09);
            IKLineData data_m11 = reader.GetAllData("m11", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m11);
            IKLineData data_m12 = reader.GetAllData("m12", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m12);
            IKLineData data_m13 = reader.GetAllData("m13", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_m13);
            IKLineData data_mmi = reader.GetAllData("mmi", KLinePeriod.KLinePeriod_1Minute);
            AssertKLineTime(data_m01, data_mmi);
        }

        private void AssertKLineTime(IKLineData srcKLineData, IKLineData targetKLineData)
        {
            //Assert.AreEqual(srcKLineData.Length, targetKLineData.Length);
            for (int i = 0; i < srcKLineData.Length; i++)
                Assert.AreEqual(srcKLineData.Arr_Time[i], targetKLineData.Arr_Time[i]);
        }

        //[TestMethod]
        //public void TestGetData()
        //{
        //    IKLineDataReader klineDataReader = GetKLineDataReader();
        //    //IKLineData klineData = klineDataReader.GetData("m03", 20151215, 20151230, KLinePeriod.KLinePeriod_1Minute);            
        //    IKLineData klineData = klineDataReader.GetAllData("m03", KLinePeriod.KLinePeriod_1Minute);
        //    for (int i = 0; i < klineData.Length; i++)
        //    {
        //        klineData.BarPos = i;
        //        Console.WriteLine(klineData);
        //    }
        //}

        public void TestKLineDataReader_GetData_Hour()
        {

        }

        [TestMethod]
        public void TestKLineDataReader_GetData()
        {
            AssertGetData("m05", 20141229, 20141229, KLinePeriod.KLinePeriod_1Minute, Resources.KLineData_M05_20141229_20141229_1Minute);
            AssertGetData("m05", 20130101, 20151231, KLinePeriod.KLinePeriod_1Minute, Resources.KLineData_M05_20130101_20151231_1Minute);
            AssertGetData("m05", 20141215, 20150116, KLinePeriod.KLinePeriod_15Minute, Resources.KLineData_M05_20141215_20150116_15Minute);
            AssertGetData("m05", 20141215, 20150116, KLinePeriod.KLinePeriod_1Minute, Resources.KLineData_M05_20141215_20150116_1Minute);
            AssertGetData("m05", 20141215, 20150116, KLinePeriod.KLinePeriod_1Day, Resources.KLineData_M05_20141215_20150116_Day);
            AssertGetData("m05", 20141215, 20150127, KLinePeriod.KLinePeriod_15Minute, Resources.KLineData_M05_20141215_20150127_15Minute);
            AssertGetData("m05", 20141215, 20150127, KLinePeriod.KLinePeriod_1Minute, Resources.KLineData_M05_20141215_20150127_1Minute);
            AssertGetData("m05", 20141215, 20150127, KLinePeriod.KLinePeriod_1Day, Resources.KLineData_M05_20141215_20150127_Day);
            AssertGetData("m09", 20141215, 20150127, KLinePeriod.KLinePeriod_1Minute, Resources.KLineData_M09_20141215_20150127_1Minute);
        }

        private void AssertGetData(string code, int start, int end, KLinePeriod period, string result)
        {
            IHistoryDataReader_KLine klineDataReader = GetKLineDataReader();
            string[] lines = result.Split('\r');
            IKLineData klineData = klineDataReader.GetData(code, start, end, period);
            Assert.AreEqual(lines.Length, klineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Assert.AreEqual(lines[i].Trim(), klineData.ToString());
            }
        }


        private static IHistoryDataReader_KLine GetKLineDataReader()
        {
            return ResourceLoader.GetDefaultDataReaderFactory().KLineDataReader;
        }
    }
}
