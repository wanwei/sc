using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data.test.Properties;
using System.IO;

namespace com.wer.sc.data.store.test
{
    [TestClass]
    public class TestKLineDataStore
    {
        [TestMethod]
        public void TestAppend()
        {
            String path = System.Environment.CurrentDirectory + "test.kline";
            KLineData data = ResourceLoader.LoadKLineData();

            IKLineData d1 = data.GetRange(0, 100);
            IKLineData d2 = data.GetRange(101, data.Length - 1);

            KLineDataStore store = new KLineDataStore(path);
            store.Save(d1);
            store.Append(d2);

            KLineData data2 = store.Load();
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
            File.Delete(path);
        }        

        [TestMethod]
        public void TestSaveLoad()
        {
            String path = System.Environment.CurrentDirectory + "test.kline";
            KLineData data = ResourceLoader.LoadKLineData();
            KLineDataStore store = new KLineDataStore(path);
            store.Save(data);
            KLineData data2 = store.Load();
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
        public void TestLoadByDate()
        {
            String path = System.Environment.CurrentDirectory + "test.kline";
            KLineData data = ResourceLoader.LoadKLineData();
            KLineDataStore store = new KLineDataStore(path);
            store.Save(data);
            KLineData data2 = store.Load();
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
        public void TestFromBytes()
        {
            KLineData data = ResourceLoader.LoadKLineData();
            KLineDataStore store = new KLineDataStore("");
            byte[] bs = store.GetBytes(data);
            KLineData data2 = store.FromBytes(bs, 0, bs.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
                //Console.WriteLine(data2);
            }
        }

        [TestMethod]
        public void TestLoadCsv()
        {
            KLineData data = ResourceLoader.LoadKLineData();
            Assert.AreEqual("20000717,2160,2160,1996,1996,372,270,0", data.ToString());
            data.BarPos = 1;
            Assert.AreEqual("20000718,2013,2146,2013,2038,1084,692,0", data.ToString());
            data.BarPos = 2;
            Assert.AreEqual("20000719,2040,2040,2011,2012,136,742,0", data.ToString());
            data.BarPos = 3;
            Assert.AreEqual("20000720,2021,2026,2006,2016,60,742,0", data.ToString());
            data.BarPos = 4;
            Assert.AreEqual("20000721,1990,1998,1970,1970,204,754,0", data.ToString());
        }
    }
}
