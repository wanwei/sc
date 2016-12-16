using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    public class PluginMgr : IPluginMgr
    {
        private Dictionary<string, object> dic = new Dictionary<string, object>();

        private List<PluginAssembly> pluginAssemblies = new List<PluginAssembly>();

        private List<PluginInfo> pluginInfos = new List<PluginInfo>();

        private Dictionary<string, List<PluginInfo>> dicPlugins = new Dictionary<string, List<PluginInfo>>();

        private string path;

        public List<PluginAssembly> PluginAssemblys
        {
            get
            {
                return pluginAssemblies;
            }
        }

        internal PluginMgr(string path)
        {
            this.path = path;
        }

        public List<PluginInfo> GetAllPlugins()
        {
            return this.pluginInfos;
        }

        public List<PluginInfo> GetPlugins(Type type)
        {
            List<PluginInfo> plugins;
            dicPlugins.TryGetValue(type.Name, out plugins);
            return plugins;
        }

        public T CreatePluginObject<T>(PluginInfo pluginInfo)
        {
            return (T)Activator.CreateInstance(pluginInfo.PluginClassType);
        }

        public T GetPluginObject<T>(PluginInfo pluginInfo)
        {
            if (dic.ContainsKey(pluginInfo.PluginName))
            {
                return (T)dic[pluginInfo.PluginName];
            }
            T t = CreatePluginObject<T>(pluginInfo);
            dic.Add(pluginInfo.PluginName, t);
            return t;
        }

        public void Load()
        {
            PluginAssemblyScan scan = new PluginAssemblyScan();
            this.pluginAssemblies = scan.Scan(this.path);
            if (pluginAssemblies == null)
                return;
            for (int i = 0; i < pluginAssemblies.Count; i++)
            {
                AddPluginAssembly(pluginAssemblies[i]);
            }
        }

        private void AddPluginAssembly(PluginAssembly assembly)
        {
            List<PluginInfo> plugins = assembly.Plugins;
            if (plugins == null)
                return;
            this.pluginInfos.AddRange(plugins);
            for (int i = 0; i < plugins.Count; i++)
            {
                PluginInfo plugin = plugins[i];
                AddPlugin2Dic(plugin);
            }
        }

        private void AddPlugin2Dic(PluginInfo pluginInfo)
        {
            Type type = pluginInfo.PluginType;
            string typeName = type.Name;
            if (this.dicPlugins.ContainsKey(typeName))
            {
                this.dicPlugins[typeName].Add(pluginInfo);
            }
            else
            {
                List<PluginInfo> plugins = new List<PluginInfo>();
                plugins.Add(pluginInfo);
                this.dicPlugins.Add(typeName, plugins);
            }
        }
    }
}
