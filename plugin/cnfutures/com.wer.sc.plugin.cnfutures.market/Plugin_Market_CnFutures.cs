using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market.cnfutures
{
    [Plugin("MARKET.CNFUTURES", "中国期货市场", "中国期货市场", MarketType.CnFutures)]
    public class Plugin_Market_CnFutures : IPlugin_Market
    {
        private Plugin_MarketData_CnFutures plugin_MarketData_XApi = new Plugin_MarketData_CnFutures();

        private Plugin_MarketTrader_CnFutures plugin_MarketTrader_XApi = new Plugin_MarketTrader_CnFutures();

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
