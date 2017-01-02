using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.opentime
{
    [TestClass]
    public class TestDayStartTimeCache
    {
        [TestMethod]
        public void TestGetOpenDateByTime()
        {
            List<DayOpenTime> dayStartTimes = MockData_DayStartTime.GetDayStartTime("m05");
            DayStartTimeCache cache = new DayStartTimeCache(dayStartTimes);

            int date = cache.GetOpenDate(20140106.10);
            Assert.AreEqual(20140106, date);

            date = cache.GetOpenDate(20150105.22);
            Assert.AreEqual(20150106, date);

            date = cache.GetOpenDate(20150105.01);
            Assert.AreEqual(20141231, date);
        }
    }
}