using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 画图的数据提供者
    /// 可自由指定时间
    /// </summary>
    public class GraphicDataProvider_Time : GraphicDataProvider
    {
        private String code;

        private int startIndex = 200;

        private int endIndex = 300;

        private int blockMount;

        private DataReaderFactory dataReaderFac;

        private IKLineData data;

        private float currentTime;

        private KLinePeriod period;

        private MinuteKLineChartBuilder currentKLineChartBuilder;

        private int startDate;

        private int endDate;

        public GraphicDataProvider_Time(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
        }

        #region 修改数据

        /// <summary>
        /// 修改提供的数据
        /// </summary>
        /// <param name="klineData"></param>
        public void ChangeData(IKLineData klineData)
        {
            this.data = klineData;
            this.code = klineData.Code;
            this.period = klineData.Period;
            this.startDate = (int)klineData.Arr_Time[0];
            this.endDate = (int)klineData.Arr_Time[klineData.Length - 1];
            this.endIndex = data.Length - 1;
            this.InitIndex();
        }

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
            if (currentDate >= startDate && currentDate <= endDate)
            {
                this.ChangeTime(currentTime);
            }
            else
            {
                this.endIndex = data.Length - 1;
                this.InitIndex();
            }
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

        #endregion

        public IKLineData GetKLineData()
        {
            return data;
        }

        public IKLineChart GetCurrentChart()
        {
            return currentKLineChartBuilder.GetCurrentChart();
        }

        private void InitIndex()
        {
            startIndex = endIndex - blockMount + 1;
        }

        public int StartIndex
        {
            get
            {
                return startIndex;
            }
        }

        public int EndIndex
        {
            get
            {
                return endIndex;
            }
            set
            {
                this.endIndex = value;
                InitIndex();
            }
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }
        }

        public int BlockMount
        {
            get
            {
                return blockMount;
            }

            set
            {
                if (blockMount == value)
                    return;
                blockMount = value;
                InitIndex();
            }
        }

        public float CurrentTime
        {
            get
            {
                return currentTime;
            }
        }

        public event DataChangeHandler DataChange;
    }

  
}