using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestDataUpdate_Code
    {
        //[TestMethod]
        //public void TestCodeDataUpdate()
        //{
        //    //第一次更新
        //    MockDataProvider dataProvider = new MockDataProvider();
        //    dataProvider.DataPathDir = "codeupdate\\";
        //    try
        //    {
        //        DataUpdate_Code update = new DataUpdate_Code(dataProvider);
        //        update.Update();

        //        DataReaderFactory fac = new DataReaderFactory(dataProvider.GetDataPath());
        //        ICodeReader codeReader = fac.CodeReader;

        //        List<CodeInfo> codes = codeReader.GetAllCodes();
        //        Assert.AreEqual(4, codes.Count);
        //        Assert.AreEqual("MMI", codes[0].Code);
        //        Assert.AreEqual(4, codeReader.GetCodesByCatelog("M").Count);

        //        //第二次更新
        //        MockDataProvider2 dataProvider2 = new MockDataProvider2();
        //        dataProvider2.DataPathDir = "codeupdate\\";
        //        DataUpdate_Code update2 = new DataUpdate_Code(dataProvider2);
        //        update2.Update();
        //        DataReaderFactory fac2 = new DataReaderFactory(dataProvider.GetDataPath());
        //        ICodeReader codeReader2 = fac2.CodeReader;

        //        codes = codeReader2.GetAllCodes();
        //        Assert.AreEqual(11, codes.Count);
        //        Assert.AreEqual("MMI", codes[0].Code);
        //        Assert.AreEqual(11, codeReader2.GetCodesByCatelog("M").Count);
        //    }
        //    finally
        //    {
        //        Directory.Delete(dataProvider.GetDataPath(), true);
        //    }
        //}
    }
}