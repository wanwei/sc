using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historymarket
{
    public class Plugin_Market_History : IPlugin_Market
    {
        public IPlugin_MarketData MarketData
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPlugin_MarketTrader MarketTrader
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
