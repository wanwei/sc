using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public class PluginScan
    {
        public List<PluginInfo> Scan(String path)
        {
            if (!Directory.Exists(path))
                return new List<PluginInfo>();
            String[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            List<PluginInfo> plugins = new List<PluginInfo>();
            for (int i = 0; i < files.Length; i++)
            {
                PluginInfo plugin = LoadAssembly(files[i]);
                if (plugin != null)
                    plugins.Add(plugin);
            }
            return plugins;
        }

        private PluginInfo LoadAssembly(String path)
        {
            try
            {
                PluginInfo plugin = new PluginInfo();
                Assembly ass = Assembly.LoadFrom(path);
                plugin.FullPath = path;
                plugin.AssemblyName = ass.GetName().Name;
                Type[] types = ass.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    Type t = types[i];
                    if (t.IsSubclassOf(typeof(Plugin_DataProvider)))
                        plugin.DataProviders.Add(t);
                    if (t.IsSubclassOf(typeof(Plugin_KLineModel)))
                        plugin.KLineModels.Add(t);
                }
                if (plugin.DataProviders.Count == 0 && plugin.KLineModels.Count == 0)
                    return null;
                return plugin;
            }
            catch (Exception e)
            {
                return null;
                //throw new ApplicationException("装载插件出错", e);
            }            
        }
    }
}
