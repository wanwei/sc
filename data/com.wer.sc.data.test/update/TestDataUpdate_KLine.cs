using com.wer.sc.data.test;
using com.wer.sc.data.test.Properties;
using com.wer.sc.data.test.update;
using com.wer.sc.plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.wer.sc.data.update
{
    [TestClass]
    public class TestDataUpdater_KLine
    {

        [TestMethod]
        public void TestKLineUpdate()
        {
            KLinePeriod period_Minute = KLinePeriod.KLinePeriod_1Minute;
            KLinePeriod period_15Minute = KLinePeriod.KLinePeriod_15Minute;
            KLinePeriod period_Day = KLinePeriod.KLinePeriod_1Day;

            MockDataProvider dataProvider = new MockDataProvider();
            dataProvider.DataPathDir = "klineupdate\\";
            try
            {
                //第一次更新
                DoKLineUpdate(dataProvider);

                DataReaderFactory fac = new DataReaderFactory(dataProvider.GetDataPath());
                IHistoryDataReader_KLine klineReader = fac.KLineDataReader;

                IKLineData data = klineReader.GetAllData("m05", period_Minute);
                DataTestUtils.AssertKLineDataResult(data, Resources.KLineData_M05_20141215_20150116_1Minute);
                data = klineReader.GetAllData("m05", period_15Minute);
                DataTestUtils.AssertKLineDataResult(data, Resources.KLineData_M05_20141215_20150116_15Minute);
                data = klineReader.GetAllData("m05", period_Day);
                DataTestUtils.AssertKLineDataResult(data, Resources.KLineData_M05_20141215_20150116_Day);

                data = klineReader.GetAllData("m09", period_Minute);
                Assert.IsNull(data);
                data = klineReader.GetAllData("m09", period_15Minute);
                Assert.IsNull(data);
                data = klineReader.GetAllData("m09", period_Day);
                Assert.IsNull(data);

                //第二次更新
                MockDataProvider2 dataProvider2 = new MockDataProvider2();
                dataProvider2.DataPathDir = "klineupdate\\";
                DoKLineUpdate(dataProvider2);

                data = klineReader.GetAllData("m05", new KLinePeriod(KLineTimeType.MINUTE, 1));                
                DataTestUtils.AssertKLineDataResult(data, Resources.KLineData_M05_20141215_20150127_1Minute);

                data = klineReader.GetAllData("m05", new KLinePeriod(KLineTimeType.MINUTE, 15));
                DataTestUtils.AssertKLineDataResult(data, Resources.KLineData_M05_20141215_20150127_15Minute);

                data = klineReader.GetAllData("m05", new KLinePeriod(KLineTimeType.DAY, 1));
                DataTestUtils.AssertKLineDataResult(data, Resources.KLineData_M05_20141215_20150127_Day);

                data = klineReader.GetAllData("m09", new KLinePeriod(KLineTimeType.MINUTE, 1));
                DataTestUtils.AssertKLineDataResult(data, Resources.KLineData_M09_20141215_20150127_1Minute);            
            }
            finally
            {
                Directory.Delete(dataProvider.GetDataPath(), true);
            }
        }

        private static void DoKLineUpdate(Plugin_DataProvider dataProvider)
        {
            DataUpdate_Code codeupdate = new DataUpdate_Code(dataProvider);
            codeupdate.Update();
            DataUpdate_OpenDate opendateupdate = new DataUpdate_OpenDate(dataProvider);
            opendateupdate.Update();
            DataUpdate_Tick tickupdate = new DataUpdate_Tick(dataProvider);
            tickupdate.Update();
            DataUpdate_KLine klineupdate = new DataUpdate_KLine(dataProvider);
            klineupdate.Update();
        }

        [TestMethod]
        public void TestGetKLineDataByTick()
        {
            DataProvider_MockCnFuturesM provider = new DataProvider_MockCnFuturesM(null);
            DataUpdate_KLine updater = new DataUpdate_KLine(provider);

            DataReaderFactory reader = ResourceLoader.GetDefaultDataReaderFactory();
            IList<int> openDates = reader.OpenDateReader.GetOpenDates(20040101, 20040201);
            IKLineData klineData = updater.GetKLineDataByTick("m01", reader, KLinePeriod.KLinePeriod_1Minute, openDates);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Console.WriteLine(klineData);
            }
        }
    }
}
