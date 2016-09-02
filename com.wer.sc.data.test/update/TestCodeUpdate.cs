using com.wer.sc.data.reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestCodeUpdate
    {
        [TestMethod]
        public void TestCodeDataUpdate()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            DataUpdate_Code update = new DataUpdate_Code(dataProvider);
            update.Update();

            DataReaderFactory fac = new DataReaderFactory(dataProvider.GetDataPath());
            ICodeReader codeReader = fac.CodeReader;

            List<CodeInfo> codes = codeReader.GetAllCodes();
            Assert.AreEqual(62, codes.Count);
            Assert.AreEqual("aX01", codes[0].code);
            Assert.AreEqual(3, codeReader.GetCodesByCatelog("aX").Count);

            dataProvider.Append = true;
            update.Update();
            codeReader.Refresh();
            codes = codeReader.GetAllCodes();
            Assert.AreEqual(162, codes.Count);
            Assert.AreEqual("aX01", codes[0].code);
            Assert.AreEqual(3, codeReader.GetCodesByCatelog("aX").Count);

            Directory.Delete(dataProvider.GetDataPath(), true);
        }          
    }
}