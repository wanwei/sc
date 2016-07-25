using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public class PluginMgr
    {
        private static PluginMgr pluginMgr = new PluginMgr();

        public static PluginMgr Instance
        {
            get
            {
                return pluginMgr;
            }
        }

        private bool loadOK = false;
        private List<PluginInfo> plugins = new List<PluginInfo>();
        public bool LoadOK
        {
            get
            {
                return loadOK;
            }
        }

        public List<PluginInfo> Plugins
        {
            get
            {
                return plugins;
            }
        }

        PluginMgr()
        {
        }

        public List<PluginInfo> Load()
        {
            if (loadOK)
                return plugins;
            lock (this)
            {
                if (loadOK)
                    return plugins;
                PluginScan scan = new PluginScan();
                String path = System.Environment.CurrentDirectory;
                Plugins.AddRange(scan.Scan(path + "\\dataprovider\\"));
                Plugins.AddRange(scan.Scan(path + "\\model\\"));
                loadOK = true;
                return plugins;
            }
        }
    }
}
