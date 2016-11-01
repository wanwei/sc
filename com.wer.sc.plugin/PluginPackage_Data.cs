using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 数据插件包，包括历史数据和交易插件
    /// </summary>
    public class PluginPackage_Data
    {
        private IPlugin_HistoryData plugin_HistoryData;

        private IPlugin_Trader plugin_Trader;

        public IPlugin_HistoryData Plugin_HistoryData
        {
            get
            {
                return plugin_HistoryData;
            }

            set
            {
                plugin_HistoryData = value;
            }
        }

        public IPlugin_Trader Plugin_Trader
        {
            get
            {
                return plugin_Trader;
            }

            set
            {
                plugin_Trader = value;
            }
        }
    }
}
