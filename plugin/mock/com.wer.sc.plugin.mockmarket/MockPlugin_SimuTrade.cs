using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;

namespace com.wer.sc.plugin.mockmarket
{
    public class MockPlugin_SimuTrade : IPlugin_MarketTrader
    {
        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Connect(ConnectionInfo connectionInfo)
        {
            throw new NotImplementedException();
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
        }

        public List<ConnectionInfo> GetAllConnections()
        {
            throw new NotImplementedException();
        }

        public void QueryInstruments(string[] instruments)
        {
            throw new NotImplementedException();
        }
    }
}
