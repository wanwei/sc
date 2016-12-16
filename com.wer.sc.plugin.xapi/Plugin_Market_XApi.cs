using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.xapi
{
    [Plugin("CnFutures", "中国期货市场交易", "中国期货市场交易")]
    public class Plugin_Market_XApi : IPlugin_Market
    {
        private Plugin_MarketData_XApi plugin_MarketData_XApi = new Plugin_MarketData_XApi();

        private Plugin_MarketTrader_XApi plugin_MarketTrader_XApi = new Plugin_MarketTrader_XApi();

        public IPlugin_MarketData MarketData
        {
            get
            {
                return plugin_MarketData_XApi;
            }
        }

        public IPlugin_MarketTrader MarketTrader
        {
            get
            {
                return plugin_MarketTrader_XApi;
            }
        }
    }
}
