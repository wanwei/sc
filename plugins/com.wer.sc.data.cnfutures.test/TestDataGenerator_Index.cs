using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.test
{
    [TestClass]
    public class TestDataGenerator_Index
    {
        [TestMethod]
        public void TestGeneratorIndex()
        {
            DataGenerator_Index generator = new DataGenerator_Index();

            DataProvider_CnFutures2 provider_cn = TestDataProvider_CnFutures.GetProvider();
            DataProviderImpl_TickData provider = provider_cn.Provider_TickData;

            //provider.GetTickData("")
        }
    }
}
