using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;

namespace com.wer.sc.plugin.mockmarket
{

    public class MockPlugin_MarketData : IPlugin_MarketData
    {
        public List<double[]> GetMarketOpenTime(string code, int date)
        {
            return null;
        }

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

        public DelegateOnReturnMarketData OnReturnMarketData
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

        public void Subscribe(string[] codes)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribe(string[] codes)
        {
            throw new NotImplementedException();
        }
    }
}
