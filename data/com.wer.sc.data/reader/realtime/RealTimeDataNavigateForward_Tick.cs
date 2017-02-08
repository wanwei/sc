using com.wer.sc.data.index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    /// <summary>
    /// 基于tick的实时数据导航
    /// </summary>
    public class RealTimeDataNavigateForward_Tick : IRealTimeDataNavigateForward
    {
        private DataReaderFactory dataReaderFactory;
        private string code;
        private int startDate;
        private int endDate;
        private double time;

        private int todayDate;
        private ITickData todayTickData;

        private TimeLineData_RealTime timeLineData;

        private Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData;

        private Dictionary<KLinePeriod, KLineBar> dicKLineBar = new Dictionary<KLinePeriod, KLineBar>();

        public RealTimeDataNavigateForward_Tick(DataReaderFactory dataReaderFactory, string code, int startDate, int endDate, Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData)
        {
            Init(dataReaderFactory, code, startDate, endDate, dicKLineData);
            InitTimeByDate(dataReaderFactory, code, startDate);
        }

        public RealTimeDataNavigateForward_Tick(DataReaderFactory dataReaderFactory, string code, int startDate, int endDate, Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData, double time)
        {
            Init(dataReaderFactory, code, startDate, endDate, dicKLineData);
            InitTime(time);
        }

        private void Init(DataReaderFactory dataReaderFactory, string code, int startDate, int endDate, Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData)
        {
            this.dataReaderFactory = dataReaderFactory;
            this.code = code;
            if (dataReaderFactory.OpenDateReader.IsOpen(startDate))
                this.startDate = startDate;
            else
                this.startDate = dataReaderFactory.OpenDateReader.GetNextOpenDate(startDate);
            if (dataReaderFactory.OpenDateReader.IsOpen(endDate))
                this.endDate = endDate;
            else
                this.endDate = dataReaderFactory.OpenDateReader.GetPrevOpenDate(endDate);

            this.dicKLineData = dicKLineData;
            KLinePeriod[] periods = dicKLineData.Keys.ToArray();
            for (int i = 0; i < periods.Length; i++)
            {
                dicKLineBar.Add(periods[i], new KLineBar());
            }
        }

        private void InitTime(double time)
        {
            int date = dataReaderFactory.OpenTimeReader.GetOpenDate(code, time);
            if (date < 0)
            {
                InitTimeByDate(dataReaderFactory, code, dataReaderFactory.OpenDateReader.GetNextOpenDate(date));
            }
            else
            {
                this.time = time;
                this.todayTickData = dataReaderFactory.TickDataReader.GetTickData(code, todayDate);
                this.timeLineData = new TimeLineData_RealTime(dataReaderFactory.TimeLineDataReader.GetData(code, todayDate));
            }
        }

        private void InitTimeByDate(DataReaderFactory dataReaderFactory, string code, int startDate)
        {
            bool isStartOpen = dataReaderFactory.OpenDateReader.IsOpen(startDate);
            if (isStartOpen)
                this.todayDate = startDate;
            else
                this.todayDate = dataReaderFactory.OpenDateReader.GetNextOpenDate(startDate);
            this.time = dataReaderFactory.OpenTimeReader.GetOpenTime(code, todayDate).Start;
            this.todayTickData = dataReaderFactory.TickDataReader.GetTickData(code, todayDate);
            InitKLineData(time, todayTickData);
        }

        private void InitKLineData(double time, ITickData tickData)
        {
            foreach (KLineData_RealTime klineData in dicKLineData.Values)
            {
                InitKLineData(klineData, time, tickData);
            }
        }

        private void InitKLineData(KLineData_RealTime klineData, double time, ITickData tickData)
        {
            int klineIndex = TimeIndeierUtils.IndexOfTime_KLine(klineData, time);
            double klineTime = klineData.Arr_Time[klineIndex];
            int startIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime);
            int endIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
            KLineBar bar = new KLineBar();
            RealTimeDataNavigateUtils.AggreKLineBar(bar, tickData, startIndex, endIndex);
            klineData.SetRealTimeData(bar, klineIndex);
        }

        public bool NavigateForward(int len)
        {
            int forwardedTickIndex = todayTickData.BarPos + len;
            if (forwardedTickIndex >= todayTickData.Length)
            {
                int nextDate = dataReaderFactory.OpenDateReader.GetNextOpenDate(todayDate);
                if (nextDate > this.endDate)
                    return false;
                ForwardKLineData_NextDay(nextDate);
            }
            else
            {
                todayTickData.BarPos = forwardedTickIndex;
                ForwardKLineData(len);
            }
            return true;
        }

        private void ForwardKLineData_NextDay(int nextDate)
        {
            foreach (KLinePeriod period in dicKLineData.Keys)
            {
                ForwardKLineData_NextDay(nextDate, period, dicKLineData[period]);
            }
            this.todayDate = nextDate;
        }

        private void ForwardKLineData_NextDay(int nextDate, KLinePeriod period, KLineData_RealTime klineData)
        {
            KLineBar tmpBar = dicKLineBar[period];
            TickData nextTickData = dataReaderFactory.TickDataReader.GetTickData(code, nextDate);
            while (nextTickData == null)
            {
                nextDate = dataReaderFactory.OpenDateReader.GetNextOpenDate(nextDate);
                if (nextDate < 0)
                    return;
                nextTickData = dataReaderFactory.TickDataReader.GetTickData(code, nextDate);
            }
            if (nextTickData == null)
                return;
            this.todayTickData = nextTickData;
            RealTimeDataNavigateUtils.ForwardKLineDataToNextDayOpenTime(klineData, nextDate, todayTickData, dataReaderFactory, tmpBar);
        }

        private void ForwardKLineData(int len)
        {
            foreach (KLinePeriod period in dicKLineData.Keys)
            {
                KLineBar tmpBar = dicKLineBar[period];
                RealTimeDataNavigateUtils.ForwardKLineDataByForwardedTick(dicKLineData[period], todayTickData, todayTickData.BarPos - len, todayTickData.BarPos, tmpBar);
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            KLineData_RealTime klineData;
            bool b = dicKLineData.TryGetValue(period, out klineData);
            return b ? klineData : null;
        }

        public ITimeLineData GetTimeLineData()
        {
            return timeLineData;
        }

        public ITickData GetTickData()
        {
            return todayTickData;
        }

        public Dictionary<KLinePeriod, KLineData_RealTime> DicKLineData
        {
            get
            {
                return dicKLineData;
            }
        }
    }
}