using com.wer.sc.data.reader;
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
    public class TestOpenDateCache
    {
        private static OpenDateCache openDateCache;

        private static OpenDateCache GetOpenDateCache()
        {
            if (openDateCache != null)
                return openDateCache;

            String[] lines = Resources.Cache_OpenDate.Split('\r');
            List<int> openDates = new List<int>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                openDates.Add(int.Parse(lines[i].Trim()));
            }
            openDateCache = new OpenDateCache(openDates);
            return openDateCache;
        }

        [TestMethod]
        public void TestOpenDateCache_IsOpen()
        {
            OpenDateCache cache = GetOpenDateCache();
            Assert.IsFalse(cache.IsOpen(19900101));
            Assert.IsTrue(cache.IsOpen(20100201));
            Assert.IsFalse(cache.IsOpen(20100206));
            Assert.IsFalse(cache.IsOpen(20170101));
        }

        [TestMethod]
        public void TestOpenDateCache_GetAllOpenDates()
        {
            OpenDateCache cache = GetOpenDateCache();

            String[] lines = Resources.Cache_OpenDate.Split('\r');
            List<int> openDates = cache.GetAllOpenDates();
            for (int i = 0; i < lines.Length; i++)
                Assert.AreEqual(int.Parse(lines[i]), openDates[i]);
            Assert.AreEqual(lines.Length, openDates.Count);
        }

        [TestMethod]
        public void TestOpenDateCache_FirstLastOpenDate()
        {
            OpenDateCache cache = GetOpenDateCache();
            Assert.AreEqual(20040102, cache.FirstOpenDate);
            Assert.AreEqual(20160429, cache.LastOpenDate);
        }

        [TestMethod]
        public void TestOpenDateCache_GetOpenDates()
        {
            OpenDateCache cache = GetOpenDateCache();
            List<int> allOpenDates = cache.GetAllOpenDates();
            IList<int> openDates = cache.GetOpenDates(20110101, 20110205);
            for (int i = 0; i < openDates.Count; i++)
            {
                Assert.AreEqual(allOpenDates[1701 + i], openDates[i]);
            }

            Assert.AreEqual(0, cache.GetOpenDates(20110101, 20101209).Count);

            Assert.AreEqual(cache.GetAllOpenDates().Count, cache.GetOpenDates(-1, 20170101).Count);
        }

        [TestMethod]
        public void TestOpenDateCache_GetOpenDateCount()
        {
            OpenDateCache cache = GetOpenDateCache();
            int cnt = cache.GetOpenDateCount(20100101, 20100201);
            Assert.AreEqual(21, cnt);
        }

        [TestMethod]
        public void TestOpenDateCache_GetOpenDate_GetOpenDateIndex()
        {
            OpenDateCache cache = GetOpenDateCache();
            Assert.AreEqual(-1, cache.GetOpenDateIndex(20100101));
            Assert.AreEqual(1460, cache.GetOpenDateIndex(20100105));

            Assert.AreEqual(20040609, cache.GetOpenDate(100));

            Assert.AreEqual(-1, cache.GetOpenDate(20000101));
            Assert.AreEqual(-1, cache.GetOpenDate(20170101));
        }

        [TestMethod]
        public void TestOpenDateCache_GetNextOpenDate_GetPrevOpenDate()
        {
            OpenDateCache cache = GetOpenDateCache();
            Assert.AreEqual(20100202, cache.GetNextOpenDate(20100201));
            Assert.AreEqual(20100208, cache.GetNextOpenDate(20100205));
            Assert.AreEqual(20100208, cache.GetNextOpenDate(20100206));
            Assert.AreEqual(20100210, cache.GetNextOpenDate(20100206, 3));
            Assert.AreEqual(20040102, cache.GetNextOpenDate(20000101));
            Assert.AreEqual(-1, cache.GetNextOpenDate(30000101, 3));

            Assert.AreEqual(20100204, cache.GetPrevOpenDate(20100205));
            Assert.AreEqual(20100202, cache.GetPrevOpenDate(20100205, 3));
            Assert.AreEqual(20100205, cache.GetPrevOpenDate(20100207));
            Assert.AreEqual(20100203, cache.GetPrevOpenDate(20100207, 3));
            Assert.AreEqual(-1, cache.GetPrevOpenDate(20000101));
            Assert.AreEqual(20160429, cache.GetPrevOpenDate(30000101));
        }
    }
}