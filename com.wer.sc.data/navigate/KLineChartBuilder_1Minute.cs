using com.wer.sc.data.cache;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 当前的一分钟k线数据构造器
    /// </summary>
    public class KLineChartBuilder_1Minute
    {
        private ITickData tickData;

        private IKLineData minuteKlineData;        

        private double currentTime;

        private TickDataIndeier tickDataIndeier;

        private int currentTickIndex;

        private KLineChart currentChart = new KLineChart();

        public IKLineData MinuteKlineData
        {
            get
            {
                return minuteKlineData;
            }
        }

        public ITickData TickData
        {
            get
            {
                return tickData;
            }
        }

        public int CurrentTickIndex
        {
            get
            {
                return currentTickIndex;
            }
        }

        public KLineChartBuilder_1Minute(IDataCache_CodeDate cache_CodeDate, double currentTime)
        {            
            this.minuteKlineData = cache_CodeDate.GetMinuteKLineData(); 
            this.tickData = cache_CodeDate.GetTickData();
            this.tickDataIndeier = new TickDataIndeier(tickData, minuteKlineData);
            this.ChangeTime(currentTime);
        }

        public void ChangeTime(double time)
        {
            if (this.currentTime == time)
                return;
            this.currentTime = time;
            double splitTime = Math.Round(currentTime, 4);
            int splitIndex = tickDataIndeier.GetTickIndex(splitTime);
            this.currentTickIndex = tickDataIndeier.GetTickIndex(currentTime);
            currentChart.SetTime(splitTime);
            ModifyChart(splitIndex, currentTickIndex, currentChart);
        }

        public KLineChart GetCurrentChart()
        {
            return currentChart;
        }

        private void ModifyChart(int tickStart, int tickEnd, KLineChart chart)
        {
            float high = 0;
            float low = float.MaxValue;
            int mount = 0;
            for (int i = tickStart; i <= tickEnd; i++)
            {
                float p = tickData.Arr_Price[i];
                if (high < p)
                    high = p;
                if (low > p)
                    low = p;
                mount += tickData.Arr_Mount[i];
            }

            chart.SetCode(tickData.Code);
            chart.SetStart(tickData.Arr_Price[tickStart]);
            chart.SetEnd(tickData.Arr_Price[tickEnd]);
            chart.SetHigh(high);
            chart.SetLow(low);
            chart.SetMount(mount);
            chart.SetHold(tickData.Arr_Hold[tickEnd]);
        }

        public void NextTick()
        {
            //TODO        
        }
    }
}
