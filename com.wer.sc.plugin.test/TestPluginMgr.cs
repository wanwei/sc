using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.test
{
    [TestClass]
    public class TestPluginMgr
    {
        [TestMethod]
        public void TestScan()
        {
            string path = Environment.CurrentDirectory + "\\plugin\\";
            IPluginMgr mgr = PluginMgrFactory.CreatePluginMgr(path);

            List<PluginInfo> plugins = mgr.GetAllPlugins();
            PringPlugins(plugins);

            IPlugin_HistoryData plugin_HistoryData = mgr.CreatePluginObject<IPlugin_HistoryData>(plugins[0]);
            Console.WriteLine(plugin_HistoryData.GetDataPath());

            Console.WriteLine();
            plugins = mgr.GetPlugins(typeof(IPlugin_MarketTrader));
            PringPlugins(plugins);

            Console.WriteLine();
            plugins = mgr.GetPlugins(typeof(IPlugin_Model));
            PringPlugins(plugins);
        }

        private static void PringPlugins(List<PluginInfo> plugins)
        {
            for (int i = 0; i < plugins.Count; i++)
            {
                Console.WriteLine(plugins[i]);
            }
        }

        [TestMethod]
        public void TestCreatePlugin()
        {
            string path = Environment.CurrentDirectory + "\\plugin\\";
            IPluginMgr mgr = PluginMgrFactory.CreatePluginMgr(path);

            List<PluginInfo> plugins = mgr.GetPlugins(typeof(IPlugin_HistoryData));
            IPlugin_HistoryData plugin_HistoryData = mgr.CreatePluginObject<IPlugin_HistoryData>(plugins[0]);
            Assert.AreEqual(@"D:\SCTEST\MOCKDATA\", plugin_HistoryData.GetDataPath());
            Assert.AreEqual("MockData", plugin_HistoryData.GetName());
            Assert.AreEqual("MOCK出来的数据", plugin_HistoryData.GetDescription());            
        }
    }
}
