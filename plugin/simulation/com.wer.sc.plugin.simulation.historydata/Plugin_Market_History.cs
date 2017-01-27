using com.wer.sc.plugin.market.history;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historymarket
{
    [Plugin("MARKET.HISTORY", "历史市场", "历史市场")]
    public class Plugin_Market_History : IPlugin_Market
    {
        private IPlugin_MarketData plugin_MarketData = new Plugin_MarketData_History();
        private IPlugin_MarketTrader plugin_MarketTrader = new Plugin_MarketTrader_History();

        public IPlugin_MarketData MarketData
        {
            get
            {
                return plugin_MarketData;
            }
        }

        public IPlugin_MarketTrader MarketTrader
        {
            get
            {
                return plugin_MarketTrader;
            }
        }
    }
}
