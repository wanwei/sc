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
    public class TestMainFuturesScan
    {
        //[TestMethod]
        //public void TestScanMainFutures()
        //{
        //    DataProvider_CnFutures2 provider = TestDataProvider_CnFutures.GetProvider();
        //    MainFuturesScan scan = new MainFuturesScan(provider);

        //    List<DataGenerater_MainFutures> mainFutures = new List<DataGenerater_MainFutures>();
        //    //List<String> varieties = provider.CodeInfoReader.GetVarieties();
        //    //for (int i = 0; i < varieties.Count; i++)
        //    //{
        //    //    List<MainFutures> main = scan.Scan(varieties[i], provider.GetOpenDates());
        //    //    write(main);
        //    //}
        //}

        //private void write(List<DataGenerater_MainFutures> mainFutures)
        //{
        //    String path = @"D:\mainfutures.csv";
        //    String[] contents = new string[mainFutures.Count];
        //    for (int i = 0; i < mainFutures.Count; i++)            
        //        contents[i] = mainFutures[i].ToString();            
        //    File.AppendAllLines(path, contents);
        //}
    }
}
