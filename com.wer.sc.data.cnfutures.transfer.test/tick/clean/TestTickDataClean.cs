using com.wer.sc.data;
using com.wer.sc.data.cnfutures.generator.Properties;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.tick.clean
{
    [TestClass]
    public class TestTickDataClean
    {

        [TestMethod]
        public void TestTickDataClean_M05_20040106()
        {
            ITickData tickData = GetAdjustTickData("m05", 20040106);
            AssertUtils.AssertTickDataResult(tickData, Resources.TickDataClean_M05_20040106);
        }

        [TestMethod]
        public void TestTickDataClean_M05_20140106()
        {
            ITickData tickData = GetAdjustTickData("m05", 20140106);
            AssertUtils.AssertTickDataResult(tickData, Resources.TickDataClean_M05_20140106);
        }

        private ITickData GetAdjustTickData(string code, int date)
        {
            TickData tickData = MockDataLoader.LoadTickData(code, date);
            List<double[]> openTime = MockDataLoader.LoadOpenTime(code, date);
            TickDataClean clean = new TickDataClean();
            clean.Adjust(tickData, openTime);
            return tickData;
        }

        private void PrintTickData(ITickData tickData)
        {
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                Console.WriteLine(tickData);
            }
        }
    }
}
