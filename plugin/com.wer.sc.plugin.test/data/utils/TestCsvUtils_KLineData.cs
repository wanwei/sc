using com.wer.sc.plugin.test;
using com.wer.sc.plugin.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestCsvUtils_KLineData
    {
        [TestMethod]
        public void TestKLineDataCsvLoad()
        {
            string[] lines = Resources.CsvUtils_KLineData.Split('\r');
            IKLineData klineData = CsvUtils_KLineData.LoadByLines(lines);
            Assert.AreEqual(lines.Length, klineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Assert.AreEqual(lines[i].Trim(), klineData.ToString());
            }
        }

        [TestMethod]
        public void TestSaveLoad()
        {
            String filename = "m05_20000717_20131225.csv";
            string[] lines = Resources.CsvUtils_KLineData.Split('\r');
            IKLineData klineData = CsvUtils_KLineData.LoadByLines(lines);

            CsvUtils_KLineData.Save(ResourceLoader.GetTestOutputPath(filename), klineData);            
            IKLineData newklineData = CsvUtils_KLineData.Load(ResourceLoader.GetTestOutputPath(filename));
            Assert.AreEqual(klineData.Length, newklineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                newklineData.BarPos = i;
                Assert.AreEqual(klineData.ToString(), newklineData.ToString());
            }
        }
    }
}
