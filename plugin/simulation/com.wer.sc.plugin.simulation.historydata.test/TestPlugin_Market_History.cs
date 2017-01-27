using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using com.wer.sc.data;

namespace com.wer.sc.plugin.market.history.test
{
    [TestClass]
    public class TestPlugin_Market_History
    {
        [TestMethod]
        public void TestMarket_History_Data_Frequency500()
        {
            IPluginMgr pluginMgr = PluginMgrFactory.DefaultPluginMgr;
            PluginInfo pluginInfo = pluginMgr.GetPlugin("MARKET.HISTORY");
            IPlugin_Market market = pluginMgr.CreatePluginObject<IPlugin_Market>(pluginInfo);

            ConnectionInfo connectInfo = new ConnectionInfo();
            connectInfo.Data.Add("DataPath", @"D:\SCDATA\CNFUTURES");
            connectInfo.Data.Add("StartDate", "20100105");
            connectInfo.Data.Add("EndDate", "20100105");
            connectInfo.Data.Add("Frequency", "500");

            market.MarketData.Subscribe(new string[] { "m05", "m09" });
            market.MarketData.OnReturnMarketData = OnReturnMarketData;
            market.MarketData.Connect(connectInfo);
        }

        void OnReturnMarketData(object sender, ref ITickBar marketData)
        {
            Console.WriteLine(marketData.Code + "," + marketData);
        }
    }
}
