using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.reader
{
    [TestClass]
    public class TestKLineDataReader
    {
        [TestMethod]
        public void TestKLineDataReader_()
        {
            IKLineData klineData = ResourceLoader.GetDefaultDataReaderFactory().KLineDataReader.GetData("m05", 20130101, 20160101, KLinePeriod.KLinePeriod_1Minute);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Console.WriteLine(klineData);
            }
        }
    }
}
