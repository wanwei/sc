﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;

namespace com.wer.sc.plugin.mock.strategy
{
    [Strategy("MOCK.STRATEGY.TURNINGPOINT","转折点查找", "转折点查找，测试专用")]
    public class MockStrategy_TurningPoint : IStrategy
    {
        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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