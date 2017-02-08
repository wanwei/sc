using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 插件信息类，该类描述了一个插件信息
    /// </summary>
    public class PluginInfo
    {
        //private PluginAssembly pluginAssembly;

        private Type pluginClassType;

        private Type pluginType;

        private string pluginID;

        private string pluginName;

        private string pluginDesc;

        private MarketType marketType;

        public PluginInfo(Type pluginClassType, Type pluginType, string pluginID, string pluginName, string pluginDesc, MarketType marketType)
        {
            this.pluginClassType = pluginClassType;
            this.pluginType = pluginType;
            this.pluginID = pluginID;
            this.pluginName = pluginName;
            this.pluginDesc = pluginDesc;
            this.marketType = marketType;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type PluginClassType
        {
            get { return pluginClassType; }
        }

        public Type PluginType
        {
            get { return pluginType; }
        }

        /// <summary>
        /// 插件ID
        /// </summary>
        public string PluginID
        {
            get
            {
                return pluginID;
            }
        }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName
        {
            get
            {
                return pluginName;
            }
        }

        public string PluginDesc
        {
            get
            {
                return pluginDesc;
            }
        }

        public MarketType MarketType
        {
            get
            {
                return marketType;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(pluginType.FullName).Append(",");
            sb.Append(pluginClassType.FullName).Append(",");
            sb.Append(PluginID).Append(",");
            sb.Append(PluginName).Append(",");
            sb.Append(PluginDesc);
            return sb.ToString();
        }
    }
}
