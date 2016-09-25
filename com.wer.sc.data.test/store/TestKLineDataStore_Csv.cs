using com.wer.sc.data.test.Properties;
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
            IKLineData klineData = KLineDataStore_Csv.LoadKLineData(lines);
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
            IKLineData klineData = KLineDataStore_Csv.LoadKLineData(lines);

            KLineDataStore_Csv store = new KLineDataStore_Csv(ResourceLoader.GetTestOutputPath("m05_20000717_20131225.csv"));
            store.Save(klineData);

            KLineDataStore_Csv newstore = new KLineDataStore_Csv(ResourceLoader.GetTestOutputPath("m05_20000717_20131225.csv"));
            IKLineData newklineData = newstore.Load();
            Assert.AreEqual(klineData.Length, newklineData.Length);
            for(int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                newklineData.BarPos = i;
                Assert.AreEqual(klineData.ToString(), newklineData.ToString());
            }
        }
    }
}
