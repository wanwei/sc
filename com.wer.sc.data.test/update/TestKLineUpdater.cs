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
    public class TestKLineUpdater
    {

        [TestMethod]
        public void TestKLineUpdate()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            try
            {
                DataUpdate_Code codeupdate = new DataUpdate_Code(dataProvider);
                codeupdate.Update();
                DataUpdate_Tick tickupdate = new DataUpdate_Tick(dataProvider);
                tickupdate.Update();
                DataUpdate_KLine update = new DataUpdate_KLine(dataProvider);
                update.Update();

                DataReaderFactory fac = new DataReaderFactory(dataProvider.GetDataPath());
                KLineDataReader klineReader = fac.KLineDataReader;

                KLineData data = klineReader.GetData("m01", 20131202, 20131213, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
                String[] dataResult = ResourceLoader.GetKLineData_1Min_Result();
                for (int i = 0; i < data.Length; i++)
                {
                    data.BarPos = i;
                    Assert.AreEqual(dataResult[i], data.ToString());
                }

                data = klineReader.GetData("m01", 20131202, 20131213, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15));
                dataResult = ResourceLoader.GetKLineData_15Minute_Result();
                for (int i = 0; i < data.Length; i++)
                {
                    data.BarPos = i;
                    Assert.AreEqual(dataResult[i], data.ToString());
                }

                data = klineReader.GetData("m01", 20131202, 20131213, new KLinePeriod(KLinePeriod.TYPE_HOUR, 1));
                dataResult = ResourceLoader.GetKLineData_1Hour_Result();
                for (int i = 0; i < data.Length; i++)
                {
                    data.BarPos = i;
                    Assert.AreEqual(dataResult[i], data.ToString());
                }

                data = klineReader.GetData("m01", 20131202, 20131213, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
                for (int i = 0; i < data.Length; i++)
                {
                    data.BarPos = i;
                    //Console.WriteLine(data);
                    //Assert.AreEqual(dataResult[i], data.ToString());
                }

                dataProvider.Append = true;
                codeupdate.Update();
                tickupdate.Update();
                update.Update();

                data = klineReader.GetData("m01", 20131202, 20131231, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15));
                dataResult = ResourceLoader.GetKLineData_15Minute_Append_Result();
                for (int i = 0; i < data.Length; i++)
                {
                    data.BarPos = i;
                    Assert.AreEqual(dataResult[i], data.ToString());
                }
            }
            finally
            {
                Directory.Delete(dataProvider.GetDataPath(), true);
            }
        }
    }
}
