using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 该数据提供者能够让数据根据tick数据向前进
    /// </summary>
    public class GraphicDataProvider_Tick : GraphicDataProvider
    {
        private String code;

        private int blockMount;

        private DataReaderFactory dataReaderFac;

        private KLineData data;

        private float currentTime;

        private KLinePeriod period;

        private int startIndex = 200;

        private int endIndex = 300;        

        private TickData todayTickData;

        private KLineData todayMinuteData;

        private KLineChart currentChart = new KLineChart();

        private int startDate;

        private int endDate;

        public GraphicDataProvider_Tick(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
        }

        public void ChangeData(KLineData klineData)
        {
            this.data = klineData;
            this.code = klineData.Code;
            this.period = klineData.Period;
            this.startDate = (int)klineData.arr_time[0];
            this.endDate = (int)klineData.arr_time[klineData.Length - 1];
        }

        public void ChangeData(String code, int startDate, int endDate, KLinePeriod period)
        {
            this.data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
            this.code = code;
            this.period = period;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public KLineData GetKLineData()
        {
            return data;
        }

        public IKLineChart GetCurrentChart()
        {
            return new KLineChart_KLineData(data, endIndex);
        }

        private void InitCharts()
        {
            if (code == null || period == null)
                return;
            this.data = dataReaderFac.KLineDataReader.GetData(code, 20100101, 20150101, period);
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

            set
            {
                if (this.code != null && this.code.Equals(value))
                    return;
                this.code = value;
                InitCharts();
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }

            set
            {
                if (this.period != null && period.Equals(value))
                    return;
                period = value;
                InitCharts();
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

            set
            {
                currentTime = value;
                InitIndex();
            }
        }

        public event DataChangeHandler DataChange;
    }
}