using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataTransfer_Tick2KLine
    {
        public static IKLineData Transfer(List<TickData> data, KLinePeriod targetPeriod, List<double[]> opentime)
        {
            List<IKLineData> klineDataList = new List<IKLineData>();
            KLineData lastData = null;
            for (int i = 0; i < data.Count; i++)
            {
                float lastPrice = lastData == null ? -1 : lastData.arr_end[lastData.Length - 1];
                KLineData klinedata = Transfer(data[i], targetPeriod, opentime, lastPrice);
                klineDataList.Add(klinedata);
                lastData = klinedata;
            }
            //DataTransfer_Tick2KLine transfer = new DataTransfer_Tick2KLine(data, targetPeriod, opentime, yesterdayEndPrice);
            //return transfer.GetKLineData();
            return KLineData.Merge(klineDataList);
        }

        public static KLineData Transfer(TickData data, KLinePeriod targetPeriod, List<double[]> opentime)
        {
            return Transfer(data, targetPeriod, opentime, -1);
        }

        public static KLineData Transfer(TickData data, KLinePeriod targetPeriod, List<double[]> opentime, float yesterdayEndPrice)
        {
            DataTransfer_Tick2KLine transfer = new DataTransfer_Tick2KLine(data, targetPeriod, opentime, yesterdayEndPrice);
            return transfer.GetKLineData();
        }

        private TickData ticks;
        private KLinePeriod targetPeriod;
        //private List<double> timePeriods;
        private float yesterdayEndPrice = -1;
        private List<double[]> openTime;

        public DataTransfer_Tick2KLine(TickData ticks, KLinePeriod targetPeriod, List<double[]> openTime, float yesterdayEndPrice)
        {
            this.ticks = ticks;
            this.targetPeriod = targetPeriod;
            this.openTime = openTime;
            //this.timePeriods = TimeUtils.GetKLineTimes(opentime, targetPeriod);
            this.yesterdayEndPrice = yesterdayEndPrice;
        }

        public List<KLineChart> GetKLineCharts()
        {
            return DataTransfer_Tick2KLineGenerator.GenerateCharts(ticks, openTime, targetPeriod, yesterdayEndPrice);
        }

        public KLineData GetKLineData()
        {
            List<KLineChart> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(ticks, openTime, targetPeriod, yesterdayEndPrice);

            return GetCharts(charts);
        }

        public static KLineData GetCharts(List<KLineChart> charts)
        {
            KLineData data = new KLineData(charts.Count);
            for (int i = 0; i < charts.Count; i++)
            {
                KLineChart chart = charts[i];
                data.arr_time[i] = chart.FullTime;
                data.arr_start[i] = chart.Start;
                data.arr_high[i] = chart.High;
                data.arr_low[i] = chart.Low;
                data.arr_end[i] = chart.End;
                data.arr_mount[i] = chart.Mount;
                data.arr_money[i] = chart.Money;
                data.arr_hold[i] = chart.Hold;
            }
            return data;
        }
    }
}