using com.wer.sc.data.store;
using com.wer.sc.data.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.store
{
    [TestClass]
    public class TestOpenDateStore
    {
        [TestMethod]
        public void TestOpenDateSaveLoad()
        {
            String path = ResourceLoader.GetTestOutputPath("opendate");
            string[] lines = Resources.Store_OpenDate.Split('\r');
            List<int> openDates = OpenDateStore.LoadOpenDates(lines);

            Assert.AreEqual(lines.Length, openDates.Count);
            for (int i = 0; i < openDates.Count; i++)
                Assert.AreEqual(lines[i].Trim(), openDates[i].ToString());

            OpenDateStore store = new OpenDateStore(path);
            store.Save(openDates);

            OpenDateStore newstore = new OpenDateStore(path);
            List<int> newcodes = store.Load();
            Assert.AreEqual(openDates.Count, newcodes.Count);
            for (int i = 0; i < openDates.Count; i++)
            {
                Assert.AreEqual(openDates[i], newcodes[i]);
            }
            File.Delete(path);
        }
    }
}
