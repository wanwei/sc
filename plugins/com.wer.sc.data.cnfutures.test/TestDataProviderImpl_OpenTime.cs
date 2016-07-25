using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.test
{
    [TestClass]
    public class TestDataProviderImpl_OpenTime
    {
        [TestMethod]
        public void TestLoadOpenTime()
        {
            DataProviderImpl_OpenTime config = new DataProviderImpl_OpenTime();
            List<OpenTimeMarket> markets = config.Markets;
            Console.WriteLine(config);
        }

        [TestMethod]
        public void TestGetOpenTime()
        {
            DataProviderImpl_OpenTime config = new DataProviderImpl_OpenTime();
            List<double[]> openTime = config.GetOpenTime("DL", "M", 20100101);
            Console.WriteLine(OpenTimeToString(openTime));
            openTime = config.GetOpenTime("DL", "M", 20150101);
            Console.WriteLine(OpenTimeToString(openTime));
            openTime = config.GetOpenTime("SQ", "AU", 20150101);
            Console.WriteLine(OpenTimeToString(openTime));
            openTime = config.GetOpenTime("ZZ", "SR", 20111207);
            Console.WriteLine(OpenTimeToString(openTime));            
        }

        public static String OpenTimeToString(List<double[]> openTime)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < openTime.Count; i++)
            {
                sb.Append(openTime[i][0]).Append("-");
                sb.Append(openTime[i][1]).Append(";");
            }
            return sb.ToString();
        }
    }
}
