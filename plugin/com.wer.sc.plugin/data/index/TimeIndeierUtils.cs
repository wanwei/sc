﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.index
{
    public class TimeIndeierUtils
    {
        private static TimeIndeier timeIndeier = new TimeIndeier();

        public static int IndexOfTime_KLine(IKLineData klineData, double time)
        {
            return timeIndeier.IndexOf(new KLineTimeGetter(klineData), time);
        }

        public static int IndexOfTime_KLine(IKLineData klineData, double time, bool findBackward)
        {
            return timeIndeier.IndexOf(new KLineTimeGetter(klineData), time, findBackward);
        }

        public static int IndexOfTime_Tick(ITickData tickData, double time)
        {
            return timeIndeier.IndexOf(new TickTimeGetter(tickData), time);
        }

        public static int IndexOfTime_Tick(ITickData tickData, double time, bool findBackward)
        {
            return timeIndeier.IndexOf(new TickTimeGetter(tickData), time, findBackward);
        }

        public static int IndexOfTime_TimeLine(ITimeLineData timeLineData, double time)
        {
            return timeIndeier.IndexOf(new TimeLineTimeGetter(timeLineData), time);
        }

        public static int IndexOfTime_TimeLine(ITimeLineData timeLineData, double time, bool findBackward)
        {
            return timeIndeier.IndexOf(new TimeLineTimeGetter(timeLineData), time, findBackward);
        }
    }
    public class KLineTimeGetter : TimeGetter
    {
        private IKLineData klineData;
        public KLineTimeGetter(IKLineData klineData)
        {
            this.klineData = klineData;
        }

        public int Count
        {
            get
            {
                return klineData.Length;
            }
        }

        public double GetTime(int index)
        {
            return klineData.Arr_Time[index];
        }
    }

    public class TickTimeGetter : TimeGetter
    {
        private ITickData tickData;

        public TickTimeGetter(ITickData tickData)
        {
            this.tickData = tickData;
        }

        public int Count
        {
            get
            {
                return tickData.Length;
            }
        }

        public double GetTime(int index)
        {
            return tickData.Arr_Time[index];
        }
    }

    public class TimeLineTimeGetter : TimeGetter
    {
        private ITimeLineData timeLineData;

        public TimeLineTimeGetter(ITimeLineData timeLineData)
        {
            this.timeLineData = timeLineData;
        }

        public int Count
        {
            get
            {
                return timeLineData.Length;
            }
        }

        public double GetTime(int index)
        {
            return timeLineData.Arr_Time[index];
        }
    }
}
