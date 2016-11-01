using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data.test.Properties;
using System.IO;
using com.wer.sc.data.utils;

namespace com.wer.sc.data.store.test
{
    [TestClass]
    public class TestKLineDataStore
    {
        [TestMethod]
        public void TestKLineDataStore_GetBytes_FromBytes()
        {
            IKLineData data = LoadKLineData();
            byte[] bs = KLineDataStore.GetBytes(data);
            KLineData data2 = KLineDataStore.FromBytes(bs, 0, bs.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
        }

        [TestMethod]
        public void TestKLineDataStore_SaveLoad()
        {
            String path = ResourceLoader.GetTestOutputPath("m05_20000717_20131225.kline");

            IKLineData data = LoadKLineData();
            KLineDataStore store = new KLineDataStore(path);
            store.Save(data);

            KLineDataStore store2 = new KLineDataStore(path);
            KLineData data2 = store.LoadAll();
            Assert.AreEqual(data.Length, data2.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }

            Assert.AreEqual(20000717, store.GetFirstTime());
            Assert.AreEqual(20131225, store.GetLastTime());
            File.Delete(path);
        }

        [TestMethod]
        public void TestKLineDataStore_Append()
        {
            String path = ResourceLoader.GetTestOutputPath("m05_20000717_20131225.kline");
            IKLineData data = LoadKLineData();

            IKLineData d1 = data.GetRange(0, 100);
            IKLineData d2 = data.GetRange(101, data.Length - 1);

            KLineDataStore store = new KLineDataStore(path);
            store.Save(d1);

            KLineDataStore store2 = new KLineDataStore(path);
            store2.Append(d2);

            KLineData data2 = store.LoadAll();
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
            File.Delete(path);
        }

        [TestMethod]
        public void TestKLineDataStore_LoadByIndex()
        {
            String path = ResourceLoader.GetTestOutputPath("m05_20000717_20131225.kline");
            IKLineData data = LoadKLineData();

            KLineDataStore store = new KLineDataStore(path);
            store.Save(data);

            KLineDataStore store2 = new KLineDataStore(path);
            IKLineData data2 = store2.LoadByIndex(50, 100);

            for (int i = 50; i <= 100; i++)
            {
                data.BarPos = i;
                data2.BarPos = i - 50;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }

            File.Delete(path);
        }

        [TestMethod]
        public void TestKLineDataStore_LoadByDate()
        {
            String path = ResourceLoader.GetTestOutputPath("m05_20000717_20131225.kline");
            IKLineData data = LoadKLineData();

            KLineDataStore store = new KLineDataStore(path);
            store.Save(data);

            KLineDataStore store2 = new KLineDataStore(path);
            IKLineData data2 = store2.Load(20100101, 20120101);

            for (int i = 2244; i <= 2722; i++)
            {
                data.BarPos = i;
                data2.BarPos = i - 2244;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }

            data2 = store2.Load(-1, int.MaxValue);
            Assert.AreEqual(data.Length, data2.Length);
            for(int i = 0; i < data2.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }

            File.Delete(path);
        }

        private IKLineData LoadKLineData()
        {
            string[] lines = Resources.KLineData_M05_20000717_20131225.Split('\r');
            return CsvUtils_KLineData.LoadByLines(lines);
        }
    }
}
