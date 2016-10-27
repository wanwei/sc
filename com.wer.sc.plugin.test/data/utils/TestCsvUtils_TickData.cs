using com.wer.sc.data.utils;
using com.wer.sc.plugin.test;
using com.wer.sc.plugin.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.store
{
    [TestClass]
    public class TestTickDataStore_Csv
    {
        [TestMethod]
        public void TestLoadTick()
        {
            string[] lines = Resources.CsvUtils_TickData.Split('\r');
            ITickData tickData = CsvUtils_TickData.LoadByLines(lines);
            Assert.AreEqual(lines.Length, tickData.Length);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                Assert.AreEqual(lines[i].Trim(), tickData.ToString());
            }
        }

        [TestMethod]
        public void TestLoadSaveTick()
        {
            String path = ResourceLoader.GetTestOutputPath("m01_20131231.csv");
            string[] lines = Resources.CsvUtils_TickData.Split('\r');
            ITickData tickData = CsvUtils_TickData.LoadByLines(lines);

            CsvUtils_TickData.Save(path, tickData);

            ITickData newtickData = CsvUtils_TickData.Load(path);
            Assert.AreEqual(tickData.Length, newtickData.Length);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                newtickData.BarPos = i;
                Assert.AreEqual(tickData.ToString(), newtickData.ToString());
            }
        }
    }
}