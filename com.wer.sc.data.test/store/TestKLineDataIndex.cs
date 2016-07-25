using com.wer.sc.data.update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    [TestClass]
    public class TestKLineDataIndex
    {
        [TestMethod]
        public void TestIndex()
        {
            KLineData data_ = ResourceLoader.GetKLineData_1Min();
            KLineData data = data_.SubData(0, 449);

            MockDataProvider provider = new MockDataProvider();
            String targetPath = provider.GetDataPath() + "\\testindex.kline";

            KLineDataStore store = new KLineDataStore(targetPath);
            store.Save(data);

            KLineDataIndex indexer = new KLineDataIndex(targetPath);
            indexer.DoIndex();

            KLineDataIndexResult result = indexer.GetIndex();
            Assert.AreEqual(2, result.DateList.Count);
            Assert.AreEqual(20131202, result.DateList[0]);
            Assert.AreEqual(20131203, result.DateList[1]);

            data = data_.SubData(450, data_.Length - 1);
            store.Append(data);
            indexer.DoIndex();
            result = indexer.GetIndex();
            Assert.AreEqual(10, result.DateList.Count);

            Directory.Delete(provider.GetDataPath(), true);
        }
    }
}
