using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;

namespace com.wer.sc.plugin.historymarket
{
    public class Plugin_MarketTrader_History : IPlugin_MarketTrader
    {
        private DelegateOnConnectionStatus onConnectionStatus;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                onConnectionStatus = value;
            }
        }

        private DelegateOnReturnInstrument onReturnInstruments;

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                return onReturnInstruments;
            }

            set
            {
                onReturnInstruments = value;
            }
        }

        public void Connect(ConnectionInfo connectionInfo)
        {
        }

        public void DisConnect()
        {
        }

        public List<ConnectionInfo> GetAllConnections()
        {
            return null;
        }

        public void QueryInstruments(string[] instruments = null)
        {
        }
    }
}
