using com.wer.sc.data.reader.realtime.utils;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    /// <summary>
    /// 实时数据导航
    /// 实现功能：
    /// 1.历史数据分析
    ///     --基于K线的分析，基于K线分析可以不需要tick数据，所以尽量不装载tick数据
    ///     --基于tick的分析，每一秒都可以进行分析
    /// 2.画图
    ///     --
    /// </summary>
    public class RealTimeDataNavigater : IRealTimeDataNavigater
    {
        private DataReaderFactory fac;

        private string code;

        private double time;

        private int currentDate;

        private Dictionary<KLinePeriod, IKLineData> dicKLineData = new Dictionary<KLinePeriod, IKLineData>();

        private ITimeLineData timeLineData;

        private ITickData tickData;

        private RealTimeDataReader realTimeDataReader;

        public RealTimeDataNavigater(DataReaderFactory fac, string code, double time)
        {
            this.fac = fac;
            this.code = code;
            this.time = time;
            this.realTimeDataReader = new RealTimeDataReader(fac, code, time);
        }

        public RealTimeDataNavigater(DataReaderFactory fac, string code, double time, int startDate, int endDate)
        {
            this.fac = fac;
            this.code = code;
            this.realTimeDataReader = new RealTimeDataReader(fac, code, time, startDate, endDate);
        }

        public void NavigateTo(double time)
        {
            double prevTime = this.time;
            this.realTimeDataReader.SetTime(time);
            OnDataNavigateHandler?.Invoke(this, new RealTimeDataNavigateEventArgs(prevTime, time));
        }

        public void NavigateForward_Time(KLinePeriod period, int len)
        {
            
        }

        public void NavigateForward_Period(KLinePeriod period, int len)
        {

        }

        public void NavigateForward_Tick(int len)
        {
            if (this.tickData == null)
            {

            }
        }

        private ITimeLineData GetCurrentTimeLine()
        {
            return null;
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public event DataNavigateEventHandler OnDataNavigateHandler;

        public IKLineData GetKLineData(KLinePeriod period)
        {
            throw new NotImplementedException();
        }

        public ITimeLineData GetTimeLineData()
        {
            if (timeLineData != null)
                return timeLineData;
            this.timeLineData = fac.TimeLineDataReader.GetData(code, currentDate);

            return timeLineData;
        }

        public ITickData GetTickData()
        {
            if (tickData != null)
            {
                return tickData;
            }
            this.tickData = fac.TickDataReader.GetTickData(code, currentDate);
            int barPos = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
            this.tickData.BarPos = barPos;
            return tickData;
        }

        public void NavigateToEnd()
        {
            throw new NotImplementedException();
        }
    }


}
