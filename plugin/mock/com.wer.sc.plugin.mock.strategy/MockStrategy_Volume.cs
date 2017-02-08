﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;

namespace com.wer.sc.plugin.mock.strategy
{
    [Strategy("MOCK.STRATEGY.VOLUME","量能过滤", "量能过滤，测试专用")]
    public class MockStrategy_Volume : IStrategy
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

        public void OnBar(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        public void OnTick(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}