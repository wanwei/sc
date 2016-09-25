using com.wer.sc.data;
using com.wer.sc.data.provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace com.wer.sc.plugin.test.provider
{
    [TestClass]
    public class TestProvider_CodeInfo
    {
        [TestMethod]
        public void TestGetAllCodes()
        {
            DataProvider_CodeInfo provider = new DataProvider_CodeInfo(ResourceLoader.GetTestOutputPath());
            List<CodeInfo> codes = provider.GetAllCodes();

            string codePath = ResourceLoader.GetTestOutputPath("codes.csv");
            string[] lines = File.ReadAllLines(codePath);
            Assert.AreEqual(lines.Length, codes.Count);
            for (int i = 0; i < lines.Length; i++)
            {
                Assert.AreEqual(lines[i].Trim(), codes[i].ToString());
            }
        }
    }
}
