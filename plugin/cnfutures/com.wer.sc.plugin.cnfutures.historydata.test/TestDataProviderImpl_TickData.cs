using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.test
{
    [TestClass]
    public class TestTickLoader
    {
        //[TestMethod]
        //public void TestPrintGetTickData()
        //{
        //    DataProvider_CnFutures2 provider_cn = TestDataProvider_CnFutures.GetProvider();
        //    DataProviderImpl_TickData provider = provider_cn.Provider_TickData;
        //    List<int> openDates = provider_cn.Provider_OpenDate.GetOpenDates();
        //    for (int i = 300; i < 900; i++)
        //    {
        //        TickData data = provider.GetTickData("m05", openDates[i]);
        //        save(data);
        //    }
        //    //AssertTickData(provider.GetTickData("m05", 20061229), Resources.adjust_m05_20061229);
        //    //AssertTickData(provider.GetTickData("m09", 20070531), Resources.adjust_m09_20070531);
        //    //AssertTickData(provider.GetTickData("m05", 20071017), Resources.adjust_m05_20071017);
        //}

        //private void save(TickData tickData)
        //{
        //    if (tickData == null)
        //        return;
        //    String path = @"D:\sctest\m05\" + tickData.Date + ".csv";
        //    //File f = new File()
        //    String[] contents = new String[tickData.Length];
        //    for (int i = 0; i < tickData.Length; i++)
        //    {
        //        tickData.BarPos = i;
        //        contents[i] = tickData.ToString();
        //    }
        //    File.WriteAllLines(path, contents);
        //}

        ///// <summary>
        ///// 用例
        ///// 1.20061229 m05 第一时段，只调整auction；第二时段，开始时间spreadforward；第三时段，整体后移，开始时段spreadbackword
        ///// 2.
        ///// 3.20071017 m05
        ///// </summary>
        //[TestMethod]
        //public void TestGetTickData()
        //{
        //    DataProvider_CnFutures2 provider_cn = TestDataProvider_CnFutures.GetProvider();
        //    DataProviderImpl_TickData provider = provider_cn.Provider_TickData;
        //    AssertTickData(provider.GetTickData("m05", 20061229), Resources.adjust_m05_20061229);
        //    AssertTickData(provider.GetTickData("m09", 20070531), Resources.adjust_m09_20070531);
        //    AssertTickData(provider.GetTickData("m05", 20071017), Resources.adjust_m05_20071017);
        //    AssertTickData(provider.GetTickData("m08", 20050329), Resources.adjust_m08_20050329);
        //    //printTickData(provider.GetTickData("m08", 20050413));
        //}

        //private void AssertTickData(TickData tickData, String result)
        //{
        //    String[] arr = result.Split("\n".ToCharArray());
        //    Assert.AreEqual(tickData.Length, arr.Length);
        //    for (int i = 0; i < tickData.Length; i++)
        //    {
        //        tickData.BarPos = i;
        //        Assert.AreEqual(tickData.ToString(), arr[i].Trim());
        //    }
        //}

        //private void printTickData(TickData tickData)
        //{
        //    for (int i = 0; i < tickData.Length; i++)
        //    {
        //        tickData.BarPos = i;
        //        Console.WriteLine(tickData);
        //    }
        //}


        //[TestMethod]
        //public void TestGetOriginalTick()
        //{
        //    DataProvider_CnFutures2 provider_cn = TestDataProvider_CnFutures.GetProvider();
        //    DataProviderImpl_TickData provider = provider_cn.Provider_TickData;
        //    TickData tickData = provider.GetOrignalTickData("cf01", 20160429);

        //    Assert.AreEqual(18960, tickData.Length);
        //    Assert.AreEqual("20160428.2059,12420,554,554,240386,0,0,0,0,1", tickData.ToString());
        //    tickData.BarPos = 4;
        //    Assert.AreEqual("20160428.21,12415,10,640,-2,12410,4,12420,206,1", tickData.ToString());
        //    tickData.BarPos = 18959;
        //    Assert.AreEqual("20160429.145959,12600,10,238228,-4,12600,82,12605,9,0", tickData.ToString());
        //}

        //[TestMethod]
        //public void TestTickAnalysis()
        //{
        //    DataProvider_CnFutures2 provider = TestDataProvider_CnFutures.GetProvider();
        //    DataProviderImpl_TickData loader = new DataProviderImpl_TickData(provider);

        //    TickData data = loader.GetOrignalTickData("m05", 20100104);
        //    List<double[]> openTime = provider.GetOpenTime("m05", 20100104);
        //    List<TickInfo_Period> periods = TickDataAnalysis.Analysis(data, openTime);
        //    Assert.AreEqual("0,0,2735,False,False,0,0,41,False,False,0,0,42", periods[0].ToString());
        //    Assert.AreEqual("1,2736,4315,False,False,0,0,42,False,False,0,0,40", periods[1].ToString());
        //    Assert.AreEqual("2,4316,7491,False,False,0,0,43,False,False,0,0,41", periods[2].ToString());

        //    data = loader.GetOrignalTickData("m05", 20061229);
        //    openTime = provider.GetOpenTime("m05", 20061229);
        //    periods = TickDataAnalysis.Analysis(data, openTime);
        //    Assert.AreEqual("0,0,1478,False,False,0,0,341,False,False,0,0,-176", periods[0].ToString());
        //    Assert.AreEqual("1,1479,2260,True,True,1480,57,-179,False,False,0,0,-10", periods[1].ToString());
        //    Assert.AreEqual("2,2261,3718,True,True,2262,46,-181,False,False,0,0,-182", periods[2].ToString());
        //    //for (int i = 0; i < periods.Count; i++)
        //    //{
        //    //    Console.WriteLine(periods[i]);
        //    //}
        //}
    }
}