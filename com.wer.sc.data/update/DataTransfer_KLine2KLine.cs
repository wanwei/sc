using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataTransfer_KLine2KLine
    {
        /// <summary>
        /// 将源k线数据转换成目标k线数据
        /// 源只可能是1分钟、1小时、1日
        /// </summary>
        /// <param name="data"></param>
        /// <param name="targetPeriod"></param>
        /// <param name="openTimeList"></param>
        /// <returns></returns>
        public static KLineData Transfer(KLineData data, KLinePeriod targetPeriod)
        {
            if (targetPeriod.PeriodType == KLinePeriod.TYPE_DAY)
                return Transfer_Day(data, targetPeriod, 0.2);
            KLinePeriod sourcePeriod = data.Period;
            if (sourcePeriod.PeriodType == KLinePeriod.TYPE_MINUTE)
            {
                if (targetPeriod.PeriodType == KLinePeriod.TYPE_HOUR)
                    targetPeriod = new KLinePeriod(sourcePeriod.PeriodType, targetPeriod.Period * 60);
                return Transfer_SrcIs1Minute(data, targetPeriod);
            }
            return null;
        }

        /// <summary>
        /// 将k线转换成日线
        /// </summary>
        /// <param name="data"></param>
        /// <param name="targetPeriod"></param>
        /// <param name="timeSplit"></param>
        /// <returns></returns>
        public static KLineData Transfer_Day(KLineData data, KLinePeriod targetPeriod, double timeSplit)
        {
            List<KLineChart> charts = new List<KLineChart>();
            int period = targetPeriod.Period;

            int startIndex = 0;
            int endIndex = 0;
            for (int i = 1; i < data.Length; i++)
            {
                double lastfulltime = data.arr_time[i - 1];
                double lastdate = (int)lastfulltime;
                double lasttime = lastfulltime - lastdate;

                double fulltime = data.arr_time[i];
                double date = (int)fulltime;
                double time = fulltime - date;

                if (date != lastdate || (lasttime < timeSplit && time > timeSplit))
                {
                    endIndex = i - 1;
                    charts.Add(GetChart_Day(data, startIndex, endIndex));
                    startIndex = endIndex + 1;
                }
                if (i == data.Length - 1)
                {
                    endIndex = i;
                    charts.Add(GetChart_Day(data, startIndex, endIndex));
                }
            }

            return GetKLineData(charts);
        }

        private static KLineChart GetChart_Day(KLineData data, int startIndex, int endIndex)
        {
            KLineChart chart = GetChart(data, startIndex, endIndex);           
            chart.time = (int)data.arr_time[endIndex];
            return chart;
        }

        private static KLineData Transfer_SrcIs1Minute(KLineData data, KLinePeriod targetPeriod)
        {
            KLinePeriod sourcePeriod = data.Period;
            if (sourcePeriod.PeriodType != targetPeriod.PeriodType)
                return Transfer_DifferentPeriod(data, targetPeriod);

            List<KLineChart> charts = new List<KLineChart>();
            int period = targetPeriod.Period;

            int startIndex = 0;
            int endIndex = startIndex + period - 1;

            while (endIndex < data.Length)
            {
                charts.Add(GetChart(data, startIndex, endIndex));
                startIndex = endIndex + 1;
                endIndex = startIndex + period - 1;

                endIndex = FindRealLastIndex_1Minute(data, startIndex, endIndex);
            }

            return GetKLineData(charts);
        }

        private static int FindRealLastIndex_1Minute(KLineData data, int startIndex, int endIndex)
        {
            if (startIndex >= data.Length)
                return endIndex;
            if (endIndex >= data.Length)
                return data.Length - 1;
            double between = data.arr_time[endIndex] - data.arr_time[startIndex];
            if (between < 0.04)
                return endIndex;

            while (between > 0.04)
            {
                endIndex--;
                between = data.arr_time[endIndex] - data.arr_time[startIndex];
            }
            return endIndex;
        }

        private static KLineChart GetChart(KLineData data, int startIndex, int endIndex)
        {
            KLineChart chart = new KLineChart();
            chart.time = data.arr_time[startIndex];
            chart.start = data.arr_start[startIndex];
            chart.end = data.arr_end[endIndex];
            chart.hold = data.arr_hold[endIndex];

            float high = float.MinValue;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                float chigh = data.arr_high[i];
                float clow = data.arr_low[i];
                high = high < chigh ? chigh : high;
                low = low > clow ? clow : low;
                mount += data.arr_mount[i];
                money += data.arr_money[i];
            }
            chart.high = high;
            chart.low = low;
            chart.mount = mount;
            chart.money = money;
            return chart;
        }

        private static KLineData GetKLineData(List<KLineChart> charts)
        {
            KLineData data = new KLineData(charts.Count);
            for (int i = 0; i < charts.Count; i++)
            {
                KLineChart chart = charts[i];
                data.arr_time[i] = chart.time;
                data.arr_start[i] = chart.start;
                data.arr_high[i] = chart.high;
                data.arr_low[i] = chart.low;
                data.arr_end[i] = chart.end;
                data.arr_mount[i] = chart.mount;
                data.arr_money[i] = chart.money;
                data.arr_hold[i] = chart.hold;
            }
            return data;
        }

        private static KLineData Transfer_DifferentPeriod(KLineData data, KLinePeriod targetPeriod)
        {
            KLinePeriod srcPeriod = data.Period;
            if (targetPeriod.PeriodType == KLinePeriod.TYPE_HOUR && srcPeriod.PeriodType == KLinePeriod.TYPE_MINUTE)
            {
                KLinePeriod p = new KLinePeriod(srcPeriod.PeriodType, targetPeriod.Period * 60);
                return Transfer(data, p);
            }
            return null;
        }

        //private static KLineData Transfer_Minute(KLineData data, KLinePeriod targetPeriod, List<double[]> openTimeList)
        //{
        //    //TimeUtils.GetKLineTimes()
        //    return null;
        //}
    }
}
