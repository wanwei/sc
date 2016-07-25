using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    [TestClass]
    public class TestKLineData
    {
        //[TestMethod]
        public void TestMem()
        {
            TickData data = new TickData(10000);
            KLineData data1 = new KLineData(225 * 1000);
            Thread.Sleep(1000 * 20);
        }

        [TestMethod]
        public void TestKLineMerge()
        {
            KLineData data = ResourceLoader.GetKLineData_1Min();
            KLineData d1 = data.SubData(0, 99);
            KLineData d2 = data.SubData(100, 199);
            KLineData d3 = data.SubData(200, 299);
            KLineData d4 = data.SubData(300, data.Length - 1);

            List<KLineData> dataList = new List<KLineData>();
            dataList.Add(d1);
            dataList.Add(d2);
            dataList.Add(d3);
            dataList.Add(d4);
            KLineData dataResult = KLineData.Merge(dataList);
            Assert.AreEqual(dataResult.Length, data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                dataResult.BarPos = i;
                Assert.AreEqual(dataResult.ToString(), data.ToString());
            }
        }
    }
}