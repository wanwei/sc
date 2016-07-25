using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 1.找到period，及其始末index
    /// 2.获得该period的所有time
    /// 3.把这些index的数据全部转换成KLine
    /// </summary>
    public class DataTransfer_Tick2KLine2
    {
        public static KLineData Transfer(TickData data, KLinePeriod targetPeriod, List<double[]> opentime)
        {
            return Transfer(data, targetPeriod, opentime, -1);
        }

        public static KLineData Transfer(TickData data, KLinePeriod targetPeriod, List<double[]> opentime, float yesterdayEndPrice)
        {
            DataTransfer_Tick2KLine2 transfer = new DataTransfer_Tick2KLine2(data, targetPeriod, opentime, yesterdayEndPrice);
            return transfer.GetKLineData();
        }

        private TickData tick;
        private KLinePeriod targetPeriod;
        private List<double> timePeriods;
        private float yesterdayEndPrice = -1;

        public DataTransfer_Tick2KLine2(TickData tick, KLinePeriod targetPeriod, List<double[]> opentime, float yesterdayEndPrice)
        {
            this.tick = tick;
            this.targetPeriod = targetPeriod;
            this.timePeriods = TimeUtils.GetKLineTimes(opentime, targetPeriod);
            this.yesterdayEndPrice = yesterdayEndPrice;
        }

        public KLineData GetKLineData()
        {

            // KLineChartGen.GenerateCharts(tick, timePeriods, yesterdayEndPrice);

            //List<KLineChart> charts = new List<KLineChart>(ticks.Count * timePeriods.Count);
            //for (int i = 0; i < ticks.Count; i++)
            //    charts.AddRange(KLineChartGen.GenerateCharts(ticks[i], timePeriods, yesterdayEndPrice));

            //KLineData data = new KLineData(charts.Count);
            //for (int i = 0; i < charts.Count; i++)
            //{
            //    KLineChart chart = charts[i];
            //    data.arr_time[i] = chart.time;
            //    data.arr_start[i] = chart.start;
            //    data.arr_high[i] = chart.high;
            //    data.arr_low[i] = chart.low;
            //    data.arr_end[i] = chart.end;
            //    data.arr_mount[i] = chart.mount;
            //    data.arr_money[i] = chart.money;
            //    data.arr_hold[i] = chart.hold;
            //}
            //return data;
            return null;
        }
    }

    public class KLineChartGen_Period
    {
        private List<double> timeKLinePeriods = new List<double>();
        private TickData tickData;
        private KLinePeriod targetPeriod;

        private int startIndex;
        private int endIndex;

        public KLineChartGen_Period(TickData tickData, KLinePeriod targetPeriod, double[] period, double lastEndPrice)
        {
            this.tickData = tickData;
            this.targetPeriod = targetPeriod;
            //this.timeKLinePeriods
        }

        public List<KLineChart> GetCharts()
        {
            return null;
        }

        private int GetDate(int index)
        {
            return (int)tickData.arr_time[index];
        }

        private List<double> GetTodayTimePeriods(TickData data, List<double> timePeriods)
        {
            int startDate = GetDate(0);
            int endDate = GetDate(data.Length - 1);

            List<double> periods = new List<double>(timePeriods.Count);
            if (startDate == endDate)
            {
                for (int i = 0; i < timePeriods.Count; i++)
                    periods.Add(Math.Round(startDate + timePeriods[i], 6));
            }
            else
            {
                bool isEndDate = false;
                for (int i = 0; i < timePeriods.Count; i++)
                {
                    if (isEndDate)
                        periods.Add(Math.Round(endDate + timePeriods[i], 6));
                    else
                    {
                        int date = startDate;
                        if (i != 0)
                        {
                            if (timePeriods[i] < timePeriods[i - 1])
                            {
                                isEndDate = true;
                                date = endDate;
                            }
                        }
                        periods.Add(Math.Round(date + timePeriods[i], 6));
                    }
                }
            }
            return periods;
        }

        private int GetEndTickIndex(int endIndex)
        {
            return -1;
        }


        public static List<KLineChart> GenerateCharts(TickData tickData, int startIndex, int endIndex, float lastEndPrice)
        {
            return null;
        }
    }
}
