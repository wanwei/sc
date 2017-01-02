using com.wer.sc.data.reader.realtime.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    [TestClass]
    public class TestTimeIndeier
    {
        [TestMethod]
        public void TestTimeIndeier_Tick()
        {
            ITickData tickData = ResourceLoader.LoadTickData_M01_20131231();
            // TimeIndeier_Tick indeier = new TimeIndeier_Tick(tickData);
            int index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20131231.140455);
            Console.WriteLine(index);
            Assert.AreEqual(879, index);
            index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20131231.140456);
            Console.WriteLine(index);
            Assert.AreEqual(880, index);
            index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20131231.140458, false);
            Assert.AreEqual(882, index);
        }
    }
}
