using com.wer.sc.plugin.Properties;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market
{
    [TestClass]
    public class TestConnectionInfo
    {
        [TestMethod]
        public void TestLoadConnectionInfo()
        {
            string connectInfoStr = Resources.ConnectionInfo;
            ConnectionInfo connectInfo = ConnectionInfo.LoadJson(connectInfoStr);
            Assert.AreEqual("SIMNOW1", connectInfo.Id);
            Assert.AreEqual("SimuNow模拟CTP1", connectInfo.Name);
            Assert.AreEqual("SimuNow模拟CTP，一号线", connectInfo.Description);           
        }
    }
}