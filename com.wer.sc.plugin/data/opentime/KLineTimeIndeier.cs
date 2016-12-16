using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.opentime
{
    /// <summary>
    /// K线的时间索引器，用于快速找到时间在KLineData里的Index
    /// 比如一段从20100101-20110101的1分钟K线，可以快速定位20100510100920的K线Bar的Index
    /// </summary>
    public class KLineTimeIndeier
    {
        private IKLineData klineData;

        private Dictionary<double, int> indeies = new Dictionary<double, int>();

        private KLinePeriod klinePeriod;

        public KLineTimeIndeier(IKLineData klineData)
        {
            this.klineData = klineData;
            this.klinePeriod = klineData.Period;
        }

        private void DoIndex()
        {
            double lastRoundTime = GetRoundTime(klineData.Arr_Time[0]);
            indeies.Add(lastRoundTime, 0);
            for (int i = 1; i < klineData.Length; i++)
            {
                double time = klineData.Arr_Time[i];
                double roundTime = GetRoundTime(time);

                if (lastRoundTime != roundTime)
                {
                    indeies.Add(roundTime, i);
                }
            }
        }

        private bool IsAddIndex(double lastTime, double time)
        {
            double lastTimeRound = GetRoundTime(lastTime);
            double timeRound = GetRoundTime(time);
            return lastTimeRound != timeRound;
        }

        private double GetRoundTime(double time)
        {
            /*
             * 秒钟线用分钟做索引
             * 分钟K线用小时做索引
             * 小时及以上K线用日线做索引
             */
            if (klinePeriod.PeriodType == KLinePeriod.TYPE_SECOND)
            {
                return Math.Round(time, 4);
            }
            else if (klinePeriod.PeriodType == KLinePeriod.TYPE_MINUTE)
            {
                return Math.Round(time, 2);
            }
            else
            {
                return Math.Round(time);
            }
        }

        public int IndexOfTime(double time)
        {
            double t = GetRoundTime(time);
            if (!indeies.ContainsKey(t))
                return -1;
            return indeies[time];
        }
    }
}