﻿using com.wer.sc.plugin.market.history;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historymarket
{
    [Plugin("MARKET.SIMULATION", "仿真市场", "仿真市场", MarketType.CnFutures)]
    public class Plugin_Market_Simulation : IPlugin_Market
    {
        private IPlugin_MarketData plugin_MarketData = new Plugin_MarketData_Simulation();
        private IPlugin_MarketTrader plugin_MarketTrader = new Plugin_MarketTrader_Simulation();

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
