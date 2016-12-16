using com.wer.sc.data;
using com.wer.sc.mockdata.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata.test
{
    [TestClass]
    public class TestMockData_DayStartTime
    {
        [TestMethod]
        public void TestMock_DayStartTime()
        {
            List<DayStartTime> dayStartTime = MockData_DayStartTime.GetDayStartTime("m05");
            AssertUtils.AssertList<DayStartTime>(dayStartTime, Resources.MockData_DayStartTime);
        }
    }
}
