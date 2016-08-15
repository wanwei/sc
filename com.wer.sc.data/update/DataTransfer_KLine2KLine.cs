using com.wer.sc.data.store;
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
        public static IKLineData Transfer(IKLineData data, KLinePeriod targetPeriod)
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
        public static IKLineData Transfer_Day(IKLineData data, KLinePeriod targetPeriod, double timeSplit)
        {
            List<KLineChart2> charts = new List<KLineChart2>();
            int period = targetPeriod.Period;

            int startIndex = 0;
            bool hasNight = false;
            for (int i = 1; i < data.Length; i++)
            {
                double lastfulltime = data.Arr_Time[i - 1];
                double fulltime = data.Arr_Time[i];

                double lastdate = (int)lastfulltime;
                double date = (int)fulltime;

                if (KLineDataIndex.IsNightStart(fulltime, lastfulltime))
                {
                    charts.Add(GetChart_Day(data, startIndex, i - 1));
                    startIndex = i;
                    hasNight = true;
                }
                else if (hasNight)
                {
                    if (date != lastdate)
                        hasNight = false;
                }
                else if (date != lastdate)
                {
                    charts.Add(GetChart_Day(data, startIndex, i - 1));
                    startIndex = i;
                }
            }

            return GetKLineData(charts);
        }

        private static KLineChart2 GetChart_Day(IKLineData data, int startIndex, int endIndex)
        {
            KLineChart2 chart = GetChart(data, startIndex, endIndex);
            chart.time = (int)data.Arr_Time[endIndex];
            return chart;
        }

        private static IKLineData Transfer_SrcIs1Minute(IKLineData data, KLinePeriod targetPeriod)
        {
            KLinePeriod sourcePeriod = data.Period;
            if (sourcePeriod.PeriodType != targetPeriod.PeriodType)
                return Transfer_DifferentPeriod(data, targetPeriod);

            List<KLineChart2> charts = new List<KLineChart2>();
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

        private static int FindRealLastIndex_1Minute(IKLineData data, int startIndex, int endIndex)
        {
            if (startIndex >= data.Length)
                return endIndex;
            if (endIndex >= data.Length)
                return data.Length - 1;
            double between = data.Arr_Time[endIndex] - data.Arr_Time[startIndex];
            if (between < 0.04)
                return endIndex;

            while (between > 0.04)
            {
                endIndex--;
                between = data.Arr_Time[endIndex] - data.Arr_Time[startIndex];
            }
            return endIndex;
        }

        private static KLineChart2 GetChart(IKLineData data, int startIndex, int endIndex)
        {
            KLineChart2 chart = new KLineChart2();
            chart.time = data.Arr_Time[startIndex];
            chart.start = data.Arr_Start[startIndex];
            chart.end = data.Arr_End[endIndex];
            chart.hold = data.Arr_Hold[endIndex];

            float high = float.MinValue;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                float chigh = data.Arr_High[i];
                float clow = data.Arr_Low[i];
                high = high < chigh ? chigh : high;
                low = low > clow ? clow : low;
                mount += data.Arr_Mount[i];
                money += data.Arr_Money[i];
            }
            chart.high = high;
            chart.low = low;
            chart.mount = mount;
            chart.money = money;
            return chart;
        }

        private static KLineData GetKLineData(List<KLineChart2> charts)
        {
            KLineData data = new KLineData(charts.Count);
            for (int i = 0; i < charts.Count; i++)
            {
                KLineChart2 chart = charts[i];
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

        private static IKLineData Transfer_DifferentPeriod(IKLineData data, KLinePeriod targetPeriod)
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
