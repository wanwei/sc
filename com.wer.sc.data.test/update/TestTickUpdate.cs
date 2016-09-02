using com.wer.sc.data.reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestTickUpdate
    {
        [TestMethod]
        public void TestTickDataUpdate()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            DataUpdate_Code codeupdate = new DataUpdate_Code(dataProvider);
            codeupdate.Update();
            DataUpdate_Tick update = new DataUpdate_Tick(dataProvider);
            update.Update();

            DataReaderFactory fac = new DataReaderFactory(dataProvider.GetDataPath());
            TickDataReader tickReader = fac.TickDataReader;
            TickData data = tickReader.GetTickData("m01", 20131202);
            Assert.AreEqual("20131202.0859,3730,960,960,720254,3729,13,3730,77,1", data.ToString());
            Assert.AreEqual(10, tickReader.GetTickDates("m01").Count);
            Assert.AreEqual(0, tickReader.GetTickDates("m05").Count);

            dataProvider.Append = true;
            update.Update();
            data = tickReader.GetTickData("m01", 20131231);
            Assert.AreEqual("20131231.0859,3711,192,192,28132,3711,8,3721,1,0", data.ToString());
            data.BarPos = 1;
            Assert.AreEqual("20131231.090006,3718,8,200,-8,3715,1,3718,6,1", data.ToString());
            data.BarPos = 2;
            Assert.AreEqual("20131231.090013,3718,18,218,-14,3718,2,3720,2,1", data.ToString());
            Assert.AreEqual(22, tickReader.GetTickDates("m01").Count);
            Assert.AreEqual(12, tickReader.GetTickDates("m05").Count);
            Directory.Delete(dataProvider.GetDataPath(), true);
        }
    }
}
