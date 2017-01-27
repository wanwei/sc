using com.wer.sc.data.store;
using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
using com.wer.sc.data.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test
{
    [TestClass]
    public class TestTickDataStore
    {
        [TestMethod]
        public void TestFromBytes()
        {
            TickData data = GetTickData();
            byte[] bs = TickDataStore.GetBytes(data);
            TickData data2 = TickDataStore.FromBytes(bs, 0, bs.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
        }

        [TestMethod]
        public void TestSaveLoad()
        {
            TickData data = GetTickData();

            String path = ResourceLoader.GetTestOutputPath("tickstoretest.tick");
            TickDataStore store = new TickDataStore(path);
            store.Save(data);

            TickDataStore store2 = new TickDataStore(path);
            TickData data2 = store2.Load();
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
            File.Delete(path);
        }

        [TestMethod]
        public void TestAppend()
        {
            TickData data = GetTickData();

            String path = ResourceLoader.GetTestOutputPath("tickstoretest.tick");
            TickData d1 = data.SubData(0, 100);
            TickData d2 = data.SubData(101, data.Length - 1);

            TickDataStore store = new TickDataStore(path);
            store.Save(d1);

            TickDataStore store2 = new TickDataStore(path);
            store2.Append(d2);

            TickDataStore store3 = new TickDataStore(path);
            TickData data2 = store3.Load();

            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
            File.Delete(path);
        }

        private TickData GetTickData()
        {
            string[] lines = Resources.Store_TickData.Split('\r');
            return (TickData)CsvUtils_TickData.LoadByLines(lines);
        }
    }
}
