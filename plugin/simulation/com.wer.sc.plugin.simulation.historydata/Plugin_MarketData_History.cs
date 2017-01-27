using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;
using com.wer.sc.data;

namespace com.wer.sc.plugin.market.history
{
    public class Plugin_MarketData_History : Plugin_XApi_Base, IPlugin_MarketData
    {
        private List<String> subscribedCodes = new List<string>();

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
            //this.marketDataTimer = new System.Timers.Timer(500);
            //marketDataTimer.Elapsed += MarketDataTimer_Elapsed;
            //marketDataTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            //marketDataTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        private void MarketDataTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        public List<double[]> GetMarketOpenTime(string code, int date)
        {
            return null;
        }

        /// <summary>
        /// 历史数据的ConnectionInfo参数
        /// 1.DataPath 使用的数据中心的地址
        /// 2.StartTime 使用的开始日期
        /// 3.频率，
        /// </summary>
        /// <param name="connectionInfo"></param>
        public void Connect(ConnectionInfo connectionInfo)
        {
            string dataPath = connectionInfo.Data["DataPath"];
            //int startTime = int.Parse(connectionInfo.Data["StartTime"]);
            //double startTime = double.Parse(connectionInfo.Data["StartTime"]);
            int startDate = int.Parse(connectionInfo.Data["StartDate"]);
            int endDate = int.Parse(connectionInfo.Data["EndDate"]);
            int frequency = int.Parse(connectionInfo.Data["Frequency"]);
            //int period = int.Parse(connectionInfo.Data["Period"]);
            DataReaderFactory fac = new DataReaderFactory(dataPath);

            IList<int> dates = fac.OpenDateReader.GetOpenDates(startDate, endDate);

            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                Dictionary<string, ITickData> dic = PrepareTickDataDictionary(fac, date);
                //fac.OpenTimeReader.GetOpenTime()
                //M05_20141229
                //夜盘开始时间是20141229
                double startTime;
                if (date < 20141229)
                {
                    startTime = date + 0.09000;
                }
                else
                {
                    if (i > 0)
                        startTime = dates[i - 1] + 0.210000;
                    else
                        startTime = fac.OpenDateReader.GetPrevOpenDate(date) + 0.210000;
                }
                DoReturnMarketData(dic, Math.Round(((double)frequency) / 1000000, 8), startTime);
            }
        }

        private void DoReturnMarketData(Dictionary<String, ITickData> dic, double frequency, double startTime)
        {
            if (onReturnMarketData == null)
                return;
            HashSet<string> endCodes = new HashSet<string>();

            for (int i = 0; i < subscribedCodes.Count; i++)
            {
                string code = subscribedCodes[i];
                ITickData tickData = dic[code];
                DoReturnMarketData_First(tickData);
                if (tickData.BarPos == tickData.Length - 1)
                {
                   // Console.WriteLine(code + "结束了");
                    endCodes.Add(code);
                }
            }
            bool isFinished = endCodes.Count == dic.Count;
            while (!isFinished)
            {
               // Console.WriteLine(subscribedCodes.Count);
                for (int i = 0; i < subscribedCodes.Count; i++)
                {
                    string code = subscribedCodes[i];
                    if (endCodes.Contains(code))
                        continue;
                    ITickData tickData = dic[code];
                    DoReturnMarketData(tickData, startTime);
                    if (tickData.BarPos == tickData.Length - 1)
                    {
                        endCodes.Add(code);
                        Console.WriteLine(code + "结束了");
                    }                        
                }
                isFinished = endCodes.Count == dic.Count;
                startTime += frequency;
            }
        }

        private object lockObj = new object();
        private void DoReturnMarketData_First(ITickData tickData)
        {
            ITickBar tickBar = tickData.GetBar(0);
            onReturnMarketData(this, ref tickBar);
        }

        private void DoReturnMarketData(ITickData tickData, double time)
        {
            int nextBarPos = tickData.BarPos + 1;
            if (nextBarPos >= tickData.Length)
                return;
            double nextTickTime = tickData.Arr_Time[nextBarPos];
            if (time >= nextTickTime)
            {
                tickData.BarPos = nextBarPos;
                ITickBar tickBar = tickData.GetBar(nextBarPos);
                onReturnMarketData(this, ref tickBar);
            }
        }

        private Dictionary<string, ITickData> PrepareTickDataDictionary(DataReaderFactory fac, int date)
        {
            Dictionary<string, ITickData> dicTickData = new Dictionary<string, ITickData>();
            for (int i = 0; i < subscribedCodes.Count; i++)
            {
                string code = subscribedCodes[i];
                ITickData tickData = fac.TickDataReader.GetTickData(code, date);
                dicTickData.Add(code, tickData);
            }
            return dicTickData;
        }

        //private void DoReturnMarketData(DataReaderFactory fac,int startDate)
        //{
        //    //onReturnMarketData
        //}



        public void DisConnect()
        {

        }

        public void Subscribe(string[] codes)
        {
            if (codes == null)
                return;
            foreach (string code in codes)
            {
                subscribedCodes.Add(code);
            }
        }

        public void UnSubscribe(string[] codes)
        {
            if (codes == null)
                return;
            foreach (string code in codes)
            {
                subscribedCodes.Remove(code);
            }
        }
    }
}