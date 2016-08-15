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
            IKLineData d1 = data.GetRange(0, 99);
            IKLineData d2 = data.GetRange(100, 199);
            IKLineData d3 = data.GetRange(200, 299);
            IKLineData d4 = data.GetRange(300, data.Length - 1);

            List<IKLineData> dataList = new List<IKLineData>();
            dataList.Add(d1);
            dataList.Add(d2);
            dataList.Add(d3);
            dataList.Add(d4);
            IKLineData dataResult = KLineData.Merge(dataList);
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