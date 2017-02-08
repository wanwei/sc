using com.wer.sc.data.index;
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

        private TimeGetter GetTickTimeGetter()
        {
            ITickData tickData = ResourceLoader.GetDefaultDataReaderFactory().TickDataReader.GetTickData("m05", 20100104);
            return new TickTimeGetter(tickData);
        }

        [TestMethod]
        public void TestTimeIndeier_Normal()
        {
            TimeGetter tickData = GetTickTimeGetter();
            TimeIndeier indeier = new TimeIndeier();
            indeier.IndexOf(tickData, 20100104.09);
        }

        [TestMethod]
        public void TestTimeIndeier_NotExist()
        {

        }

        [TestMethod]
        public void TestTimeIndeier_Repest()
        {

        }
    }
}
