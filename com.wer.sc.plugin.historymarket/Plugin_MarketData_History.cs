using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;
using com.wer.sc.data;

namespace com.wer.sc.plugin.historymarket
{
    public class Plugin_MarketData_History : IPlugin_MarketData
    {
        private System.Timers.Timer marketDataTimer;

        private DelegateOnConnectionStatus onConnectionStatus;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                this.onConnectionStatus = value;
            }
        }

        private DelegateOnReturnMarketData onReturnMarketData;

        public DelegateOnReturnMarketData OnReturnMarketData
        {
            get
            {
                return this.onReturnMarketData;
            }

            set
            {
                this.onReturnMarketData = value;
            }
        }

        public Plugin_MarketData_History()
        {
            //一秒钟返回两次数据
            this.marketDataTimer = new System.Timers.Timer(500);
            marketDataTimer.Elapsed += MarketDataTimer_Elapsed;
            marketDataTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            marketDataTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        private void MarketDataTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        public void Connect(ConnectionInfo connectionInfo)
        {
            string dataPath = connectionInfo.Data["DataPath"];
            //double time =Double.Parse( connectionInfo.Data["Time"];

            DataReaderFactory fac = new DataReaderFactory(dataPath);
            //fac.TickDataReader.GetTickData()

        }

        public void DisConnect()
        {

        }

        public List<ConnectionInfo> GetAllConnections()
        {
            return null;
        }

        public void Subscribe(string[] codes)
        {

        }

        public void UnSubscribe(string[] codes)
        {

        }
    }
}
