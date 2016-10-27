using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.update
{
    [TestClass]
    public class TestDataUpdate
    {
        [TestMethod]
        public void TestDataPrepare()
        {
            MockDataProvider dataProvider = new MockDataProvider();
            try
            {
                dataProvider.DataPathDir = "dataupdate\\dataprepare\\";
                DataUpdate_Code codeupdate = new DataUpdate_Code(dataProvider);
                codeupdate.Update();
                DataUpdate_OpenDate opendateupdate = new DataUpdate_OpenDate(dataProvider);
                opendateupdate.Update();
                DataUpdate_Tick tickupdate = new DataUpdate_Tick(dataProvider);
                tickupdate.Update();
                DataUpdate_KLine klineupdate = new DataUpdate_KLine(dataProvider);
                klineupdate.Update();

                MockDataProvider2 dataProvider2 = new MockDataProvider2();
                dataProvider2.DataPathDir = "dataupdate\\dataprepare\\";
                DataUpdate_Code codeupdate2 = new DataUpdate_Code(dataProvider2);
                codeupdate2.Update();
                DataUpdate_OpenDate opendateupdate2 = new DataUpdate_OpenDate(dataProvider2);
                opendateupdate2.Update();

                DataProviderWrap providerWrap = new DataProviderWrap(dataProvider2);
                DataUpdate dataUpdate = new DataUpdate(providerWrap);
                //UpdateInfo_Code updateInfo_Code = dataUpdate.GetUpdateInfo_Code("m05");

                UpdateInfo updateInfo = dataUpdate.DataPrepare();

                Assert.AreEqual(Resources.DataUpdate_DataPrepare, updateInfo.ToString());
            }
            finally
            {
                Directory.Delete(dataProvider.GetDataPath(), true);
            }
            //UpdateInfo updateInfo = dataUpdate.DataPrepare();
            //updateInfo.
            //update_Code.Update();
            //update_OpenDate.Update();
            //UpdateInfo generates = DataPrepare();
        }
    }
}
