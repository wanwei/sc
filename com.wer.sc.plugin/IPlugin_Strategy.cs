using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public interface IPlugin_Strategy
    {
        void ModelStart();

        void ModelEnd();

        void OnTick(IRealTimeDataReader currentData);

        void OnBar(IRealTimeDataReader currentData);

        //StrategyPeriods GetStrategyPeriods();
    }

    public class StrategyPeriods
    {
        public bool UseTickData = false;

        public List<KLinePeriod> UsedKLinePeriods = new List<KLinePeriod>();
    }
}
