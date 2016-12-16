using com.wer.sc.data.test.Properties;
using com.wer.sc.data.utils;
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
    public class TestCodeStore
    {
        [TestMethod]
        public void TestCodeSaveLoad()
        {
            String path = ResourceLoader.GetTestOutputPath("codes");
            string[] lines = Resources.Store_Code.Split('\r');
            List<CodeInfo> codes = CsvUtils_Code.LoadByLines(lines);

            Assert.AreEqual(lines.Length, codes.Count);
            for (int i = 0; i < codes.Count; i++)            
                Assert.AreEqual(lines[i].Trim(), codes[i].ToString());
            
            CodeStore store = new CodeStore(path);
            store.Save(codes);

            CodeStore newstore = new CodeStore(path);
            List<CodeInfo> newcodes = store.Load();
            Assert.AreEqual(codes.Count, newcodes.Count);
            for(int i = 0;  i < codes.Count; i++)
            {
                Assert.AreEqual(codes[i], newcodes[i]);
            }
            File.Delete(path);
        }
    }
}
