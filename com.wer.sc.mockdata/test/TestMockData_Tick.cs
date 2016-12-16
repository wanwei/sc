using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data;
using com.wer.sc.mockdata.Properties;

namespace com.wer.sc.mockdata.test
{
    [TestClass]
    public class TestMockData_Tick
    {
        [TestMethod]
        public void TestGetTick()
        {
            ITickData tickdata = MockData_Tick.GetTickData("m05", 20140106);
            AssertUtils.AssertTickDataResult(tickdata, Resources.MockData_Tick);
        }
    }
}
