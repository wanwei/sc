using com.wer.sc.data.present;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    public class KLineStrategyRunner_Present
    {
        private RealTimeDataReceiver receiver;

        private List<string> codes;
               
        private IPlugin_Strategy strategy;
        public KLineStrategyRunner_Present(RealTimeDataReceiver receiver, List<string> codes, IPlugin_Strategy strategy)
        {
            this.receiver = receiver;
            this.codes = codes;
            this.strategy = strategy;

        }

        public void Run()
        {

        }

        public void OnDataReceive()
        {
            //this.strategy.OnTick(this.receiver_Code);
        }
    }


}
