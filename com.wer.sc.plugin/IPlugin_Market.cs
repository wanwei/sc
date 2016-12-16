﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public interface IPlugin_Market
    {
        IPlugin_MarketData MarketData { get; }

        IPlugin_MarketTrader MarketTrader { get; }
    }
}