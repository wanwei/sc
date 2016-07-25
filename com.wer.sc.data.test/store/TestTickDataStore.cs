using com.wer.sc.data.store;
using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
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
        public void TestAppend()
        {
            String path = System.Environment.CurrentDirectory + "test.tick";
            TickData data = ResourceLoader.LoadTickData();

            TickData d1 = data.SubData(0, 100);
            TickData d2 = data.SubData(101, data.Length - 1);

            TickDataStore store = new TickDataStore(path);
            store.save(d1);
            store.Append(d2);

            TickData data2 = store.load();
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
            String path = System.Environment.CurrentDirectory + "test.tick";
            TickData data = ResourceLoader.LoadTickData();
            TickDataStore store = new TickDataStore(path);
            store.save(data);
            TickData data2 = store.load();
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
            File.Delete(path);
        }

        [TestMethod]
        public void TestFromBytes()
        {
            TickData data = ResourceLoader.LoadTickData();
            TickDataStore store = new TickDataStore("");
            byte[] bs = store.GetBytes(data);
            TickData data2 = store.FromBytes(bs, 0, bs.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                data2.BarPos = i;
                Assert.AreEqual(data.ToString(), data2.ToString());
            }
        }

        [TestMethod]
        public void TestLoadCsv()
        {
            TickData data = ResourceLoader.LoadTickData();
            Assert.AreEqual("20131231.0859,3711,192,192,28132,3711,8,3721,1,0", data.ToString());
            data.BarPos = 1;
            Assert.AreEqual("20131231.090006,3718,8,200,-8,3715,1,3718,6,1", data.ToString());
            data.BarPos = 2;
            Assert.AreEqual("20131231.090013,3718,18,218,-14,3718,2,3720,2,1", data.ToString());
            //data.BarPos = 3;
            //Assert.AreEqual("20000720,2021,2026,2006,2016,60,742,0", data.ToString());
            //data.BarPos = 4;
            //Assert.AreEqual("20000721,1990,1998,1970,1970,204,754,0", data.ToString());
        }

    }
}
