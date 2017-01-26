using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.plugin.mockmodel
{
    [Plugin("MOCKMODEL_MA","MA指标", "MA指标")]
    public class MockPlugin_Ma : IPlugin_Strategy
    {
        public StrategyPeriods GetStrategyPeriods()
        {
            return new StrategyPeriods();
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
