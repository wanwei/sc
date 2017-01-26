using com.wer.sc.data.reader.realtime.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    /// <summary>
    /// 实时数据读取
    /// 指定一个时间后能够取到该时间的k线数据，分时线数据以及tick数据
    /// 分时线数据和tick数据获取当日的即可。
    ///     
    /// 
    /// 实现时需要考虑的情况：
    /// 1.当日没有tick
    /// 2.指定时间对应的kline没有数据，甚至当日之前的时间也没有数据
    /// 3.指定时间不是开盘时间
    /// </summary>
    public class RealTimeDataReader : IRealTimeDataReader
    {
        private DataReaderFactory fac;
        private string code;
        private int date;
        private double time;

        private int startDate = -1;
        private int endDate = -1;

        private ITickData tickData;

        private ITimeLineData timeLineData;

        private Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

        public double Time
        {
            get
            {
                return time;
            }
        }

        public RealTimeDataReader(DataReaderFactory fac, string code, double time)
        {
            this.fac = fac;
            this.code = code;
            SetTime(time);
        }

        public RealTimeDataReader(DataReaderFactory fac, string code, double time, int startDate, int endDate)
        {
            this.fac = fac;
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            SetTime(time);
        }

        public void SetTime(double time)
        {
            this.date = fac.OpenTimeReader.GetOpenDate(code, time);
            this.time = time;
            this.tickData = CalcTickData();
        }

        /// <summary>
        /// 获取Tick数据
        /// this.time对应日的所有tick数据
        /// </summary>
        /// <returns></returns>
        private ITickData CalcTickData()
        {
            ITickData tickData = fac.TickDataReader.GetTickData(code, date);
            if (tickData == null)
                return null;
            int index = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
            tickData.BarPos = index;
            return tickData;
        }

        /// <summary>
        /// 获取K线数据
        /// 
        /// 如果创建reader时规定了开始结束时间，直接获取该时间段内所有数据
        /// 如果没规定开始结束时间，k线数据至少获取1000条数据。
        /// 周线：获取所有数据
        /// 日线：获取之前的1000日，之后的500日
        /// 小时线：获取之前的400日，之后的200日
        /// 15分钟线：获取之前的100日，之后的50日
        /// 5-15分钟线：获取之前的50日，之后的25日
        /// 1-5分钟线：获取之前的20日，之后的10日
        /// 秒线：获取之前的1日，之后的1日
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public IKLineData GetKLineData(KLinePeriod period)
        {
            int[] startEnd = GetStartEndDates(date, period);
            IKLineData klineData = fac.KLineDataReader.GetData(code, startEnd[0], startEnd[1], period);

            int klineIndex = TimeIndeierUtils.IndexOfTime_KLine(klineData, time);
            double klineTime = klineData.Arr_Time[klineIndex];
            int currentHold = klineData.Arr_Hold[klineIndex - 1];
            float price = klineData.Arr_End[klineIndex - 1];

            IKLineBar chart;
            if (tickData == null)
            {
                chart = CalcKLineBar_EmptyChart(klineTime, currentHold, price);
            }
            else
            {
                int startIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime, false);
                int endIndex = tickData.BarPos;
                chart = CalcKLineBar(tickData, startIndex, endIndex, klineTime, klineData.Arr_Hold[klineIndex], price);
            }
            KLineData_RealTime realtimeKLineData = new KLineData_RealTime(klineData);
            realtimeKLineData.SetRealTimeData(chart, klineIndex);
            return realtimeKLineData;
        }

        private int[] GetStartEndDates(int date, KLinePeriod period)
        {
            if (startDate < 0 || endDate < 0)
            {
                /// 周线：获取所有数据
                /// 日线：获取之前的1000日，之后的500日
                /// 小时线：获取之前的400日，之后的200日
                /// 15分钟线：获取之前的100日，之后的50日
                /// 5-15分钟线：获取之前的50日，之后的25日
                /// 1-5分钟线：获取之前的20日，之后的10日
                /// 秒线：获取之前的1日，之后的1日
                if (period.PeriodType == KLineTimeType.SECOND)
                {
                    int startdate = fac.OpenDateReader.GetPrevOpenDate(date);
                    int enddate = fac.OpenDateReader.GetNextOpenDate(date);
                    return new int[] { startdate, enddate };
                }
                else if (period.PeriodType == KLineTimeType.MINUTE)
                {
                    if (period.Period <= 5)
                    {
                        int startdate = fac.OpenDateReader.GetPrevOpenDate(date, 20);
                        int enddate = fac.OpenDateReader.GetNextOpenDate(date, 10);
                        return new int[] { startdate, enddate };
                    }
                    else if (period.Period <= 15)
                    {
                        int startdate = fac.OpenDateReader.GetPrevOpenDate(date, 50);
                        int enddate = fac.OpenDateReader.GetNextOpenDate(date, 25);
                        return new int[] { startdate, enddate };
                    }
                    else
                    {
                        int startdate = fac.OpenDateReader.GetPrevOpenDate(date, 100);
                        int enddate = fac.OpenDateReader.GetNextOpenDate(date, 50);
                        return new int[] { startdate, enddate };
                    }
                }
                else if (period.PeriodType == KLineTimeType.HOUR)
                {
                    int startdate = fac.OpenDateReader.GetPrevOpenDate(date, 400);
                    int enddate = fac.OpenDateReader.GetNextOpenDate(date, 200);
                    return new int[] { startdate, enddate };
                }
                else if (period.PeriodType == KLineTimeType.DAY)
                {
                    int startdate = fac.OpenDateReader.GetPrevOpenDate(date, 1000);
                    int enddate = fac.OpenDateReader.GetNextOpenDate(date, 500);
                    return new int[] { startdate, enddate };
                }
                else if (period.PeriodType == KLineTimeType.WEEK)
                {
                    int startdate = fac.OpenDateReader.FirstOpenDate;
                    int enddate = fac.OpenDateReader.LastOpenDate;
                    return new int[] { startdate, enddate };
                }
                return null;
            }
            else
                return new int[] { startDate, endDate };
        }

        /// <summary>
        /// 获取分时线数据，当日的分时数据
        /// </summary>
        /// <returns></returns>
        public ITimeLineData GetTimeLineData()
        {
            ITimeLineData timeLineData = fac.TimeLineDataReader.GetData(code, date);
            int timeLineIndex = TimeIndeierUtils.IndexOfTime_TimeLine(timeLineData, time);

            double timeLineTime = timeLineData.Arr_Time[timeLineIndex];
            int currentHold = timeLineData.Arr_Hold[timeLineIndex - 1];
            float price = timeLineData.Arr_Price[timeLineIndex - 1];
            float yesterdayEnd = timeLineData.YesterdayEnd;

            ITimeLineBar timeLineBar;
            if (tickData == null)
            {
                timeLineBar = CalcTimeLineBar_Empty(timeLineTime, currentHold, price, yesterdayEnd);
            }
            else
            {
                int startIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, timeLineTime, false);
                int endIndex = tickData.BarPos;
                timeLineBar = CalcTimeLineBar(tickData, startIndex, endIndex, timeLineTime, currentHold, price, yesterdayEnd);
            }
            TimeLineData_RealTime realTimeTimeLineData = new TimeLineData_RealTime(timeLineData);
            realTimeTimeLineData.SetRealTimeData(timeLineBar, timeLineIndex);
            return realTimeTimeLineData;
        }

        public ITickData GetTickData()
        {
            return tickData;
        }

        private KLineBar CalcKLineBar(ITickData tickData, int startTickIndex, int endTickIndex, double time, int currentHold, float currentPrice)
        {
            if (tickData == null || startTickIndex > endTickIndex)
            {
                return CalcKLineBar_EmptyChart(time, currentHold, currentPrice);
            }
            KLineBar klineBar = new KLineBar();
            float high = 0;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startTickIndex; i <= endTickIndex; i++)
            {
                int currentMount = tickData.Arr_Mount[i];
                float price = tickData.Arr_Price[i];
                high = high < price ? price : high;
                low = low > price ? price : low;
                mount += currentMount;
                //money += currentMount * price;
                currentHold += tickData.Arr_Add[i];
            }
            klineBar.Code = tickData.Code;
            klineBar.Time = time;
            klineBar.Start = tickData.Arr_Price[startTickIndex];
            klineBar.High = high;
            klineBar.Low = low;
            klineBar.End = tickData.Arr_Price[endTickIndex];
            klineBar.Mount = mount;
            klineBar.Money = money;
            klineBar.Hold = currentHold;
            return klineBar;
        }

        private KLineBar CalcKLineBar_EmptyChart(double time, int currentHold, float price)
        {
            KLineBar klineBar = new KLineBar();
            klineBar.Time = time;
            klineBar.Start = price;
            klineBar.High = price;
            klineBar.Low = price;
            klineBar.End = price;
            klineBar.Mount = 0;
            klineBar.Money = 0;
            klineBar.Hold = currentHold;
            return klineBar;
        }

        private ITimeLineBar CalcTimeLineBar(ITickData tickData, int startTickIndex, int endTickIndex, double time, int currentHold, float currentPrice, float yesterdayEnd)
        {
            KLineBar bar = CalcKLineBar(tickData, startTickIndex, endTickIndex, time, currentHold, currentPrice);
            return CalcTimeLineBar(bar, yesterdayEnd);
        }

        private ITimeLineBar CalcTimeLineBar_Empty(double time, int currentHold, float price, float yesterdayEnd)
        {
            return CalcTimeLineBar(null, 0, -1, time, currentHold, price, yesterdayEnd);
        }

        private ITimeLineBar CalcTimeLineBar(KLineBar minuteKlineBar, double yesterdayEnd)
        {
            TimeLineBar timeLineBar = new TimeLineBar();
            timeLineBar.Code = minuteKlineBar.Code;
            timeLineBar.Hold = minuteKlineBar.Hold;
            timeLineBar.Mount = minuteKlineBar.Mount;
            timeLineBar.Price = minuteKlineBar.End;
            timeLineBar.Time = minuteKlineBar.Time;
            timeLineBar.UpPercent = (float)Math.Round((minuteKlineBar.End - yesterdayEnd) * 100 / yesterdayEnd, 2);
            timeLineBar.UpRange = (float)(minuteKlineBar.End - yesterdayEnd);
            return timeLineBar;
        }
    }
}
