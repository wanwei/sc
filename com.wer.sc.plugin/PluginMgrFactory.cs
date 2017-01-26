using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public class PluginMgrFactory
    {
        public static IPluginMgr CreatePluginMgr(string path)
        {
            PluginMgr mgr = new PluginMgr(path);
            mgr.Load();
            return mgr;
        }

        private static IPluginMgr CreateDefaultPluginMgr()
        {
            string dllPath = Assembly.GetExecutingAssembly().Location;
            FileInfo f = new FileInfo(dllPath);
            string path = f.DirectoryName + "\\plugin\\";
            //string path = Environment.CurrentDirectory + "\\plugin\\";
            return CreatePluginMgr(path);
        }

        private static IPluginMgr pluginMgr = CreateDefaultPluginMgr();

        public static IPluginMgr DefaultPluginMgr
        {
            get
            {
                return pluginMgr;
            }
        }
    }
}
