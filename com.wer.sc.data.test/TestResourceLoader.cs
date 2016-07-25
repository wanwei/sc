using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    [TestClass]
    public class TestResourceLoader
    {
        [TestMethod]
        public void TestGetKLineData_1Min()
        {
            KLineData data = ResourceLoader.GetKLineData_1Min();
            String[] dataResult = ResourceLoader.GetKLineData_1Min_Result();
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                Assert.AreEqual(dataResult[i], data.ToString());
            }
        }
    }
}
