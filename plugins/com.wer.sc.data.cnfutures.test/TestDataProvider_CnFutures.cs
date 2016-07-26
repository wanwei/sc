using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace com.wer.sc.data.cnfutures.test
{
    [TestClass]
    public class TestDataProvider_CnFutures
    {
        public static DataProvider_CnFutures2 GetProvider()
        {
            DataProvider_CnFutures2 provider = new DataProvider_CnFutures2();
            provider.Helper.ConfigPath = System.Environment.CurrentDirectory + "\\com.wer.sc.data.cnfutures\\";
            return provider;
        }

        [TestMethod]
        public void TestGetCodes()
        {
            DataProvider_CnFutures2 provider = GetProvider();
            List<CodeInfo> codes = provider.GetCodes();
            Assert.AreEqual(635, codes.Count);
            Assert.AreEqual("AMI,豆一连续,A", codes[0].ToString());
            Assert.AreEqual("WT11,硬麦1111,WT", codes[633].ToString());
        }

        [TestMethod]
        public void TestOpenDates()
        {
            DataProvider_CnFutures2 provider = GetProvider();
            List<int> openDates = provider.GetOpenDates();
            Assert.IsTrue(openDates.Count >= 2994);
            Assert.AreEqual(20040102, openDates[0]);
            Assert.AreEqual(20160429, openDates[2993]);
        }

        [TestMethod]
        public void TestGetOpenTime()
        {
            DataProvider_CnFutures2 provider = GetProvider();
            List<double[]> openTime = provider.GetOpenTime("m05", 20160429);
            Assert.AreEqual("0.09-0.1015;0.103-0.113;0.133-0.15;0.21-0.233;", TestDataProviderImpl_OpenTime.OpenTimeToString(openTime));
            //Console.WriteLine(TestOpenTimeConfig.OpenTimeToString(openTime));
            openTime = provider.GetOpenTime("m05", 20141226);
            Assert.AreEqual("0.09-0.1015;0.103-0.113;0.133-0.15;", TestDataProviderImpl_OpenTime.OpenTimeToString(openTime));
            openTime = provider.GetOpenTime("m05", 20141229);
            Assert.AreEqual("0.09-0.1015;0.103-0.113;0.133-0.15;0.21-0.233;", TestDataProviderImpl_OpenTime.OpenTimeToString(openTime));

            openTime = provider.GetOpenTime("p05", 20140815);
            Assert.AreEqual("0.09-0.1015;0.103-0.113;0.133-0.15;0.21-0.233;", TestDataProviderImpl_OpenTime.OpenTimeToString(openTime));
            openTime = provider.GetOpenTime("p05", 20140812);
            Assert.AreEqual("0.09-0.1015;0.103-0.113;0.133-0.15;", TestDataProviderImpl_OpenTime.OpenTimeToString(openTime));

            openTime = provider.GetOpenTime("au03", 20140815);
            Assert.AreEqual("0.09-0.1015;0.103-0.113;0.133-0.15;0.21-0.023;", TestDataProviderImpl_OpenTime.OpenTimeToString(openTime));
            openTime = provider.GetOpenTime("au03", 20140812);
            Assert.AreEqual("0.09-0.1015;0.103-0.113;0.133-0.15;", TestDataProviderImpl_OpenTime.OpenTimeToString(openTime));

        }

        [TestMethod]
        public void TestGetTick()
        {
            DataProvider_CnFutures2 provider = GetProvider();
            //TickData tickData = provider.GetTickData("m05", 20160429);
            TickData tickData = provider.GetTickData("cf01", 20160429);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                Console.WriteLine(tickData);
            }
        }
    }
}
