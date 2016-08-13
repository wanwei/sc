using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public class PluginInfo
    {
        /// <summary>
        /// 程序集的名称
        /// </summary>
        public String AssemblyName;

        /// <summary>
        /// 程序集完整路径
        /// </summary>
        public String FullPath;

        /// <summary>
        /// 得到插件里的数据提供者
        /// </summary>
        public List<Type> DataProviders = new List<Type>();

        public List<Type> KLineModels = new List<Type>();

        public Plugin_DataProvider CreateDataProvider(Type type)
        {
            PluginHelper helper = new PluginHelper();
            helper.ConfigPath = FullPath.Substring(0, FullPath.Length - 4) + "\\";
            Plugin_DataProvider dataProvider = (Plugin_DataProvider)Activator.CreateInstance(type, new Object[] { helper });
            return dataProvider;
        }

        private List<Plugin_DataProvider> providers;

        public List<Plugin_DataProvider> GetProviders()
        {
            if (providers != null)
                return providers;
            providers = new List<Plugin_DataProvider>();
            for (int i = 0; i < DataProviders.Count; i++)
            {
                providers.Add(CreateDataProvider(DataProviders[i]));
            }
            return providers;
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AssemblyName).Append(",");
            sb.Append(FullPath).Append(",");
            for (int i = 0; i < DataProviders.Count; i++)
            {
                sb.Append(DataProviders[i]);
                if (i == DataProviders.Count - 1)
                    sb.Append(",");
                else
                    sb.Append(";");
            }
            for (int i = 0; i < KLineModels.Count; i++)
            {
                sb.Append(KLineModels[i]);
                if (i != KLineModels.Count - 1)
                    sb.Append(";");
            }
            return sb.ToString();
        }
    }
}
