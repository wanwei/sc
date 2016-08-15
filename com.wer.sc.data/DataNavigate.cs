using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据导航
    /// 可指定时间并获取该时间的各种时间周期的K线数据
    /// </summary>
    public class DataNavigate
    {
        private DataReaderFactory dataReaderFac;

        private String code;

        private float currentTime;

        private KLineData data;

        private int startDate;

        private int endDate;

        private KLinePeriod period;

        private int currentIndex;

        private KLineChart currentChart;


        public DataNavigate(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
        }

        #region 修改数据

        /// <summary>
        /// 修改提供的数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="period"></param>
        public void ChangeData(String code, int startDate, int endDate, KLinePeriod period)
        {
            this.data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
            this.code = code;
            this.period = period;
            this.startDate = startDate;
            this.endDate = endDate;
            int currentDate = (int)currentTime;
        }

        public void ChangeCode(String code)
        {
            this.data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
            this.code = code;
            this.ChangeTime(currentTime);
        }

        /// <summary>
        /// 修改周期
        /// </summary>
        /// <param name="period"></param>
        public void ChangePeriod(KLinePeriod period)
        {
            this.data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
            this.period = period;
            this.ChangeTime(currentTime);
        }

        public void ChangeTime(float time)
        {
            this.currentTime = time;
        }

        public void NextChart()
        {

        }
        public void NextChart(int period)
        {

        }

        #endregion

        #region 获取数据

        public string Code
        {
            get
            {
                return code;
            }
        }

        public KLineData GetKLineData()
        {
            return this.data;
        }

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
        }

        public KLineChart GetKLineChart()
        {
            return currentChart;
        }

        #endregion
    }

    public class KLineChartBuilder
    {

    }

    /// <summary>
    /// 一分钟k线数据获得
    /// </summary>
    public class MinuteKLineChartBuilder
    {
        private KLineData klineData;

        private KLinePeriod period;

        private DataReaderFactory dataReaderFac;

        private KLineChartBuilder_FromTick klineChartBuilder_FromTick;

        private KLineChart currentChart = new KLineChart();

        private double currentTime;

        public MinuteKLineChartBuilder(DataReaderFactory dataReaderFac, KLineData minuteKlineData, double currentTime)
        {
            this.dataReaderFac = dataReaderFac;
            this.klineData = minuteKlineData;
            this.ChangeTime(currentTime);
        }

        private void ChangeTime(double time)
        {
            //比如 20150531140500
            //1分钟线可以正常显示
            //日线需要将

            int index = FindIndexInKLine(time);
            if (index < 0)
                return;

            KLineChart currentMinuteChart = klineChartBuilder_FromTick.GetCurrentChart();

            //this.currentTime = time;
            //double splitTime = Math.Round(currentTime, 4);
            //int splitIndex = tickDataIndeier.GetTickIndex(splitTime);
            //this.currentTickIndex = tickDataIndeier.GetTickIndex(currentTime);
            //currentChart.SetTime(splitTime);
            //ModifyChart(splitIndex, currentTickIndex, currentChart);
        }

        private void ModifyChart(KLineData klineData, KLineData todayMinuteChart, int currentKLineIndex, KLineChart currentMinuteChart, KLineChart currentChart)
        {
            
        }

        public KLineChart GetCurrentChart()
        {
            return currentChart;
        }

        private int FindIndexInKLine(double time)
        {
            double firstTime = klineData.arr_time[0];
            double endTime = klineData.arr_time[klineData.Length - 1];
            if (firstTime > time || time > endTime)
                return -1;
            for (int i = 1; i < klineData.Length; i++)
            {
                double t = klineData.arr_time[i];
                if (t >= time)
                    return i;
            }
            return -1;
        }
    }

    /// <summary>
    /// 当前的一分钟k线数据构造器，也可用于
    /// </summary>
    public class KLineChartBuilder_FromTick
    {
        private TickData tickData;

        private KLineData minuteKlineData;

        private Dictionary<double, int> dic = new Dictionary<double, int>();

        private double currentTime;

        private TickDataIndeier tickDataIndeier;

        private int currentTickIndex;

        private KLineChart currentChart = new KLineChart();

        public KLineChartBuilder_FromTick(KLineData minuteKlineData, TickData tickData, double currentTime)
        {
            this.minuteKlineData = minuteKlineData;
            this.tickData = tickData;
            this.tickDataIndeier = new TickDataIndeier(tickData, minuteKlineData);
            this.ChangeTime(currentTime);
        }

        public void ChangeTime(double time)
        {
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
                float p = tickData.arr_price[i];
                if (high < p)
                    high = p;
                if (low > p)
                    low = p;
                mount += tickData.arr_mount[i];
            }

            chart.SetCode(tickData.code);
            chart.SetStart(tickData.arr_price[tickStart]);
            chart.SetEnd(tickData.arr_price[tickEnd]);
            chart.SetHigh(high);
            chart.SetLow(low);
            chart.SetMount(mount);
        }

        private int GetTickIndexByTime(float time)
        {
            return -1;
        }

        public void NextTick()
        {
            //TODO        
        }

        public void PrevTick()
        {
            //TODO
        }
    }
}