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
        private Dictionary<string, object> dic_Id_PluginObject = new Dictionary<string, object>();

        //按插件ID索引，key是插件ID，value是实现了该接口的插件
        private Dictionary<string, PluginInfo> dic_Id_Plugin = new Dictionary<string, PluginInfo>();

        //所有的插件包
        private List<PluginAssembly> pluginAssemblies = new List<PluginAssembly>();

        //所有的插件
        private List<PluginInfo> pluginInfos = new List<PluginInfo>();

        //按插件类型索引，key是插件类型，value是实现了该接口的插件
        private Dictionary<string, List<PluginInfo>> dic_Type_Plugins = new Dictionary<string, List<PluginInfo>>();

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
            dic_Type_Plugins.TryGetValue(type.Name, out plugins);
            return plugins;
        }

        /// <summary>
        /// 得到插件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PluginInfo GetPlugin(string id)
        {
            PluginInfo pluginInfo;
            bool b = dic_Id_Plugin.TryGetValue(id, out pluginInfo);
            return b ? pluginInfo : null;
        }

        public T CreatePluginObject<T>(PluginInfo pluginInfo)
        {
            return (T)Activator.CreateInstance(pluginInfo.PluginClassType);
        }

        public T GetPluginObject<T>(PluginInfo pluginInfo)
        {
            if (dic_Id_PluginObject.ContainsKey(pluginInfo.PluginID))
            {
                return (T)dic_Id_PluginObject[pluginInfo.PluginID];
            }
            T t = CreatePluginObject<T>(pluginInfo);
            dic_Id_PluginObject.Add(pluginInfo.PluginID, t);
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
            AddDic_Id_Plugin(pluginInfo);
            AddDic_Type_Plugins(pluginInfo);
        }

        private void AddDic_Id_Plugin(PluginInfo pluginInfo)
        {
            string id = pluginInfo.PluginID;
            if (this.dic_Id_Plugin.ContainsKey(id))
            {
                //TODO 写入日志，重复插件ID
            }
            else
            {
                this.dic_Id_Plugin.Add(id, pluginInfo);
            }
        }

        private void AddDic_Type_Plugins(PluginInfo pluginInfo)
        {
            Type type = pluginInfo.PluginType;
            string typeName = type.Name;
            if (this.dic_Type_Plugins.ContainsKey(typeName))
            {
                this.dic_Type_Plugins[typeName].Add(pluginInfo);
            }
            else
            {
                List<PluginInfo> plugins = new List<PluginInfo>();
                plugins.Add(pluginInfo);
                this.dic_Type_Plugins.Add(typeName, plugins);
            }
        }
    }
}
