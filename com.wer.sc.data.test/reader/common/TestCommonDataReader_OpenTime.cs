using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.reader.common
{
    [TestClass]
    public class TestCommonDataReader_OpenTime
    {
        [TestMethod]
        public void TestOpenTime_GetOpenDate()
        {
            ICommonDataReader_OpenTime reader = ResourceLoader.GetDefaultDataReaderFactory().OpenTimeReader;
            int date = reader.GetOpenDate("m05", 20140505.092000);
            Assert.AreEqual(20140505, date);

            date = reader.GetOpenDate("m05", 20140505.082000);
            Assert.AreEqual(-1, date);

            date = reader.GetRecentOpenDate("m05", 20140505.082000);
            Assert.AreEqual(20140505, date);

            date = reader.GetRecentOpenDate("m05", 20140505.192000);
            Assert.AreEqual(20140506, date);

            date = reader.GetOpenDate("m05", 20150717.220000);
            Assert.AreEqual(20150720, date);

            date = reader.GetOpenDate("m05", 20150718.220000);
            Assert.AreEqual(20150720, date);

            date = reader.GetRecentOpenDate("m05", 30150718.220000);
            Assert.AreEqual(-1, date);

            date = reader.GetRecentOpenDate("m05", 10150718.220000);
            Assert.AreEqual(20040102, date);
        }
    }
}
