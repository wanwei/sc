using com.wer.sc.data.provider;
using com.wer.sc.plugin.test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.wer.sc.plugin.provider
{
    [TestClass]
    public class TestProvider_OpenTime
    {
        [TestMethod]
        public void TestGetOpenTime()
        {
            DataProvider_OpenTime provider = new DataProvider_OpenTime(ResourceLoader.GetTestOutputPath(""));
            List<double[]> opentime = provider.GetOpenTime("m05", 20150105);
            PrintOpenTime(opentime);

            opentime = provider.GetOpenTime("m05", 20150115);
            PrintOpenTime(opentime);
        }

        private void PrintOpenTime(List<double[]> openTime)
        {
            for(int i = 0; i < openTime.Count; i++)
            {
                double[] openPeriod = openTime[i];
                Console.WriteLine(openPeriod[0] + "-" + openPeriod[1]);
            }
        }
    }
}