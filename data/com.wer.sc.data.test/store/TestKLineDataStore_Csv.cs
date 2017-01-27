using com.wer.sc.data.test.Properties;
using com.wer.sc.data.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.wer.sc.data.store
{
    [TestClass]
    public class TestKLineDataStore_Csv
    {
        [TestMethod]
        public void TestKLineDataCsvLoad()
        {
            string[] lines = Resources.Store_KlineData.Split('\r');
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
            string[] lines = Resources.Store_KlineData.Split('\r');
            IKLineData klineData = CsvUtils_KLineData.LoadByLines(lines);

            string path = ResourceLoader.GetTestOutputPath("m05_20000717_20131225.csv");
            CsvUtils_KLineData.Save(path, klineData);
            IKLineData newklineData = CsvUtils_KLineData.Load(path);
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