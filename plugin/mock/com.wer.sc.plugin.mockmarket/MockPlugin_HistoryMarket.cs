using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.mockmarket
{
    [Plugin("MOCKHISTORYMARKET", "模拟交易", "模拟交易")]
    public class MockPlugin_HistoryMarket : IPlugin_Market
    {
        private MockPlugin_MarketData marketData = new MockPlugin_MarketData();

        private MockPlugin_MarketTrade marketTrade = new MockPlugin_MarketTrade();


        public IPlugin_MarketData MarketData
        {
            get
            {
                return marketData;
            }
        }

        public IPlugin_MarketTrader MarketTrader
        {
            get
            {
                return marketTrade;
            }
        }
    }
}
