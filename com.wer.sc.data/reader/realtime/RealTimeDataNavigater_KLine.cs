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
    /// 基于K线的导航器
    /// 该导航器用途是遍历一段时间的K线数据
    /// 可以以一分钟向前进，5分钟前进等各种周期前进方式
    /// 该类只用于K线数据回测，tick数据回测不能使用该类。
    /// </summary>
    public class RealTimeDataNavigater_KLine
    {
        private DataReaderFactory fac;

        private string code;

        private double time;

        private int startDate;

        private int endDate;

        private KLinePeriod defaultPeriod;

        private IKLineData klineData_Default;

        private KLineData_RealTime klineData_1Minute;

        private KLineData_RealTime klineData_5Second;

        private Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

        private Dictionary<KLinePeriod, KLineBar> dicKLineBar = new Dictionary<KLinePeriod, KLineBar>();

        /// <summary>
        /// 创建一个基于K线的导航器
        /// </summary>
        /// <param name="fac"></param>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="defaultPeriod">默认要分析的周期，也就是向前</param>
        /// <param name="referPeriods"></param>
        public RealTimeDataNavigater_KLine(DataReaderFactory fac, string code, int startDate, int endDate, KLinePeriod defaultPeriod, List<KLinePeriod> referPeriods)
        {
            this.fac = fac;
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;

            this.defaultPeriod = defaultPeriod;
            this.klineData_Default = fac.KLineDataReader.GetData(code, startDate, endDate, defaultPeriod);
            this.time = klineData_Default.Arr_Time[1];

            IKLineBar currentDefaultBar = this.klineData_Default.GetCurrentBar();
            for (int i = 0; i < referPeriods.Count; i++)
            {
                KLinePeriod period = referPeriods[i];
                IKLineData klineData = fac.KLineDataReader.GetData(code, startDate, endDate, period);
                KLineData_RealTime realtimeKLineData = new KLineData_RealTime(klineData);

                if (period.CompareTo(defaultPeriod) > 0)
                {
                    KLineBar bar = KLineBar.CopyFrom(currentDefaultBar);
                    bar.Time = this.time;
                    realtimeKLineData.SetRealTimeData(bar, 0);
                }
                else
                {

                }

                dicKLineData.Add(period, realtimeKLineData);

                //dicKLineBar.Add(period,)
                //realtimeKLineData.SetRealTimeData()
                //realtimeKLineData.SetRealTimeData()
                //GetKLineDataInternal(referPeriods[i]);
            }
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            if (period.Equals(defaultPeriod))
                return klineData_Default;
            return dicKLineData[period];
        }

        //private KLineData_RealTime GetKLineDataInternal(KLinePeriod klinePeriod)
        //{
        //    KLineData_RealTime realtimeKLineData;
        //    bool exist = dicKLineData.TryGetValue(klinePeriod, out realtimeKLineData);
        //    if (exist)
        //        return realtimeKLineData;

        //    IKLineData klineData = fac.KLineDataReader.GetData(code, startDate, endDate, klinePeriod);
        //    realtimeKLineData = new KLineData_RealTime(klineData);
        //    dicKLineData.Add(klinePeriod, realtimeKLineData);
        //    return realtimeKLineData;
        //}

        private void AdjustedKLineData(IKLineData klineData)
        {
            if (klineData.Time == time)
                return;
            if (klineData.Period.PeriodType == KLineTimeType.MINUTE)
            {

            }
            else if (klineData.Period.PeriodType == KLineTimeType.SECOND)
            {

            }
        }

        public void NavigateForward(KLinePeriod period, int len)
        {
            //KLineData_RealTime currentKLineData = GetKLineDataInternal(period);
            //if (currentKLineData.Time == time)
            //{
            //    int lastBarPos = currentKLineData.Length - 1;
            //    if (lastBarPos == currentKLineData.BarPos)
            //        return;
            //    int toBarPos = currentKLineData.BarPos + len;
            //    if (toBarPos > lastBarPos)
            //        toBarPos = lastBarPos;

            //    double lastTime = this.time;
            //    currentKLineData.BarPos = toBarPos;
            //    this.time = currentKLineData.Time;

            //    NavigateForwardOthers(currentKLineData, period, len, lastTime);
            //}
        }

        private void NavigateForwardOthers(KLineData_RealTime currentKLineData, KLinePeriod period, int len, double lastTime)
        {
            DataForwardOtherHelper forwardHelper = new DataForwardOtherHelper(currentKLineData, time, len);
            KLinePeriod[] keies = dicKLineData.Keys.ToArray();
            for (int i = 0; i < keies.Length; i++)
            {
                KLinePeriod currentperiod = keies[i];
                if (currentperiod == period)
                    continue;

                KLineData_RealTime klineData = dicKLineData[currentperiod];
                forwardHelper.Forward(klineData);
                //NavigateForwardKLineData(klineData, period, len, lastTime, time);
            }
        }

        private void NavigateForwardKLineData(IKLineData klineData, KLinePeriod period, int len, double lastTime, double currentTime)
        {
            if (klineData.Period.CompareTo(period) > 0)
            {
                int index = TimeIndeierUtils.IndexOfTime_KLine(klineData, currentTime);

            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public event DataNavigateEventHandler OnDataNavigateHandler;

    }

    /// <summary>
    /// 数据前进帮助类
    /// 例如：
    /// 数据前进1分钟，现有1分钟，5分钟数据
    /// 几种情况：
    /// 1.现在是092900，前进1分钟，1分钟K线进一格，5分钟K线进一格
    /// 2.现在是093000，前进1分钟，1分钟K线进一格，5分钟K线修改最新block
    /// 
    /// </summary>
    class DataForwardOtherHelper
    {
        private KLineData_RealTime klineData_SmallPeriod;

        private double currentTime;

        private int forwardLength;

        public DataForwardOtherHelper(KLineData_RealTime klineData_SmallPeriod, double currentTime, int forwardLength)
        {
            this.klineData_SmallPeriod = klineData_SmallPeriod;
            this.currentTime = currentTime;
            this.forwardLength = forwardLength;
        }

        public void Forward(KLineData_RealTime klineData_LargePeriod)
        {
            Forward_Large(klineData_LargePeriod);
        }

        private void Forward_Large(KLineData_RealTime klineData_LargePeriod)
        {
            IKLineBar forwardSmallKLineBar = klineData_SmallPeriod.GetAggrKLineBar(klineData_SmallPeriod.BarPos - forwardLength + 1, klineData_SmallPeriod.BarPos);
            int nextLargeBarPos = klineData_LargePeriod.BarPos + 1;
            double nextLargeTime = klineData_LargePeriod.Arr_Time[nextLargeBarPos];
            if (nextLargeTime == currentTime)
            {
                klineData_LargePeriod.SetRealTimeData(null);
                klineData_LargePeriod.SetRealTimeData(forwardSmallKLineBar, nextLargeBarPos);
            }
            else
            {
                KLineBar bar = KLineBarMerge.Merge(klineData_LargePeriod.GetCurrentBar(), forwardSmallKLineBar);
                klineData_LargePeriod.SetRealTimeData(bar, klineData_LargePeriod.BarPos);
            }
        }
    }
}