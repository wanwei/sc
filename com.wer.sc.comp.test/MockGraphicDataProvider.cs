using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.test
{
    public class MockGraphicDataProvider : GraphicDataProvider
    {
        private String code;

        private int blockMount;

        private DataReaderFactory fac;

        private KLineData data;

        private float currentTime;

        private KLinePeriod period;

        private int startIndex = 200;

        private int endIndex = 300;        

        public MockGraphicDataProvider()
        {
            fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
        }

        public void Init(KLineData klineData)
        {
            this.data = klineData;
            this.code = klineData.code;
            this.period = klineData.Period;            
        }

        public KLineData GetKLineData()
        {
            return data;
        }
        public KLineChart GetCurrentChart()
        {
            return new KLineChart(data, endIndex);
        }
        private void InitCharts()
        {
            if (code == null || period == null)
                return;
            this.data = fac.KLineDataReader.GetData(code, 20100101, 20150101, period);
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
