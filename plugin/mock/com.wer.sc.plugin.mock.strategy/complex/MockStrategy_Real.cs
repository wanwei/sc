﻿using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.plugin.mock.strategy.complex
{
    [Strategy("MOCK.STRATEGY.COMPLEX.REAL", "复杂策略", "复杂策略，测试专用")]
    public class MockStrategy_Real : IStrategy
    {

        public StrategyPeriods GetStrategyPeriods()
        {
            throw new NotImplementedException();
        }

        public void ModelEnd()
        {
            throw new NotImplementedException();
        }

        public void ModelStart()
        {
            throw new NotImplementedException();
        }

        public void OnBar(IRealTimeDataReader currentData)
        {
            throw new NotImplementedException();
        }

        public void OnTick(IRealTimeDataReader currentData)
        {
            throw new NotImplementedException();
        }
    }
}
