using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;

namespace com.wer.sc.plugin.mock.strategy
{
    [Strategy("MOCK.STRATEGY.MA", "MA指标", "MA指标")]
    public class MockStrategy_Ma : IStrategy
    {
        private StrategyPeriods strategyPeriods;

        public MockStrategy_Ma()
        {
            strategyPeriods = new StrategyPeriods();
            strategyPeriods.UseTickData = true;
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            strategyPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
        }

        public StrategyPeriods GetStrategyPeriods()
        {
            return strategyPeriods;
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
