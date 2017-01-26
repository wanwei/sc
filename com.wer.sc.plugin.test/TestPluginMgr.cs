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
        public void TestPluginScan()
        {
            string path = Environment.CurrentDirectory + "\\plugin\\";
            IPluginMgr mgr = PluginMgrFactory.CreatePluginMgr(path);

            List<PluginInfo> plugins = mgr.GetAllPlugins();
            //PringPlugins(plugins);
            Assert.AreEqual(5, plugins.Count);

            IPlugin_HistoryData plugin_HistoryData = mgr.CreatePluginObject<IPlugin_HistoryData>(plugins[0]);
            //Console.WriteLine(plugin_HistoryData.GetDataPath());
            Assert.AreEqual(@"D:\SCTEST\MOCKDATA\", plugin_HistoryData.GetDataPath());

            Console.WriteLine();
            plugins = mgr.GetPlugins(typeof(IPlugin_Market));
            //PringPlugins(plugins);
            Assert.AreEqual(1, plugins.Count);

            Console.WriteLine();
            plugins = mgr.GetPlugins(typeof(IPlugin_Strategy));
            //PringPlugins(plugins);
            Assert.AreEqual(3, plugins.Count);

            PluginInfo pluginInfo = mgr.GetPlugin("MOCKHISTORYDATA");
            //Console.WriteLine(pluginInfo);
            Assert.AreEqual("com.wer.sc.plugin.mockmarket.MockPlugin_HistoryData", pluginInfo.PluginClassType.FullName);
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
