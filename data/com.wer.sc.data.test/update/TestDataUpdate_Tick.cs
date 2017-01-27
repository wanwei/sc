using com.wer.sc.data.reader;
using com.wer.sc.data.test;
using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
using com.wer.sc.plugin;
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
    public class TestTickUpdate
    {
        //[TestMethod]
        //public void TestTickDataUpdate()
        //{
        //    //第一次更新
        //    MockDataProvider dataProvider = new MockDataProvider();
        //    dataProvider.DataPathDir = "tickupdate\\";
        //    try
        //    {
        //        DoTickUpdate(dataProvider);

        //        DataReaderFactory fac = new DataReaderFactory(dataProvider.GetDataPath());
        //        TickDataReader tickReader = fac.TickDataReader;

        //        TickData data = tickReader.GetTickData("m01", 20141223);
        //        DataTestUtils.AssertTickDataResult(data, Resources.TickData_M01_20141223);

        //        data = tickReader.GetTickData("M05", 20150121);
        //        Assert.IsNull(data);
        //        data = tickReader.GetTickData("M09", 20141223);
        //        Assert.IsNull(data);
        //        List<int> openDates = tickReader.GetTickDates("m05");
        //        DataTestUtils.AssertDates(openDates, Resources.MockData_OpenDate);

        //        //第二次更新
        //        MockDataProvider2 dataProvider2 = new MockDataProvider2();
        //        dataProvider2.DataPathDir = "tickupdate\\";
        //        DoTickUpdate(dataProvider2);

        //        DataReaderFactory fac2 = new DataReaderFactory(dataProvider.GetDataPath());
        //        TickDataReader tickReader2 = fac2.TickDataReader;

        //        data = tickReader.GetTickData("m01", 20141223);
        //        DataTestUtils.AssertTickDataResult(data, Resources.TickData_M01_20141223);
        //        data = tickReader.GetTickData("M05", 20150121);
        //        DataTestUtils.AssertTickDataResult(data, Resources.TickData_M05_20150121);
        //        data = tickReader.GetTickData("M09", 20141223);
        //        DataTestUtils.AssertTickDataResult(data, Resources.TickData_M09_20141223);

        //        openDates = tickReader.GetTickDates("m05");
        //        DataTestUtils.AssertDates(openDates, Resources.MockData_OpenDate2);
        //        openDates = tickReader.GetTickDates("m09");
        //        DataTestUtils.AssertDates(openDates, Resources.MockData_OpenDate2);
        //    }
        //    finally
        //    {
        //        Directory.Delete(dataProvider.GetDataPath(), true);
        //    }
        //}

        //private static void DoTickUpdate(Plugin_DataProvider dataProvider)
        //{
        //    DataUpdate_Code codeupdate = new DataUpdate_Code(dataProvider);
        //    codeupdate.Update();
        //    DataUpdate_Tick update = new DataUpdate_Tick(dataProvider);
        //    update.Update();
        //}
    }
}
