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
    /// 当前k线数据获得
    /// </summary>
    public class KLineChartBuilder_AllPeriod
    {
        private IDataCache_Code dataCache_Code;

        private IDataCache_CodeDate currentDataCache_CodeDate;

        private IKLineData klineData;

        private KLineChartBuilder_1Minute klineChartBuilder_1Minute;

        public KLineChartBuilder_1Minute ChartBuilder_FromTick
        {
            get { return klineChartBuilder_1Minute; }
        }

        private IKLineChart currentChart;

        private double currentTime;

        private int currentDate;

        public int CurrentDate
        {
            get
            {
                return currentDate;
            }
        }

        public string Code
        {
            get { return klineData.Code; }
        }

        public KLineChartBuilder_AllPeriod(IKLineData srcData, IDataCache_Code cache_Code, double currentTime)
        {
            this.dataCache_Code = cache_Code;
            this.klineData = srcData;
            this.ChangeTime(currentTime);
        }

        public void ChangeTime(double time)
        {
            if (currentTime == time)
                return;
            //比如 20150531140500
            //1分钟线可以正常显示
            //日线需要将            

            int date = DaySpliter.GetTimeDate(time, dataCache_Code.GetCodeOpenDateReader());
            if (currentDate != date)
            {
                //TODO 将以前生成的klineChartBuilder_1Minute cache下来
                this.currentDataCache_CodeDate = dataCache_Code.GetCache_CodeDate(date);
                this.klineChartBuilder_1Minute = new KLineChartBuilder_1Minute(currentDataCache_CodeDate, currentTime);
            }            

            int index = klineData.IndexOfTime(time);
            double t = klineData.Arr_Time[index];

            IKLineData todayMinuteKLineData = klineChartBuilder_1Minute.MinuteKlineData;
            KLineChart currentMinuteChart = klineChartBuilder_1Minute.GetCurrentChart();

            int startIndex;
            if (t == (int)t)
                startIndex = 0;
            else
                startIndex = todayMinuteKLineData.IndexOfTime(t);
            int endIndex = todayMinuteKLineData.IndexOfTime(currentMinuteChart.Time);

            //TODO 下面算法不好，在多线程下会有问题
            ((KLineData)todayMinuteKLineData).ChangeChart(currentMinuteChart, endIndex);
            this.currentChart = todayMinuteKLineData.GetAggrChart(startIndex, endIndex);
            ((KLineData)todayMinuteKLineData).ChangeChart(null);

            this.currentTime = time;
        }

        public IKLineChart GetCurrentChart()
        {
            return currentChart;
        }
    }
}
