
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate2 : IDataNavigate2
    {
        private DataReaderFactory factory;
        private int startDate;
        private int endDate;
        private string code;
        private double time;

        private KLineChartBuilder_AllPeriod chartBuilder;

        public DataNavigate2(DataReaderFactory factory, String code, double time)
        {
            this.code = code;
            this.time = time;
            int date = (int)time;
            //this.startDate = date - 200;
            //this.endDate = date + 200;
        }

        public DataNavigate2(DataReaderFactory factory, String code, double time, int startDate, int endDate)
        {
            this.code = code;
            this.time = time;
            int date = (int)time;
            this.startDate = startDate;
            this.endDate = endDate;
            //this.chartBuilder = new CurrentKLineChartBuilder();
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public double CurrentTime
        {
            get
            {
                return time;
            }
        }

        public int StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public int EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public event DataChangeEventHandler OnDataChangeHandler;

        public void ChangeData(string code, double time)
        {
            throw new NotImplementedException();
        }

        public void ChangeTime(double time)
        {
            throw new NotImplementedException();
        }

        public void Forward(KLinePeriod period, int len)
        {
            throw new NotImplementedException();
        }

        public void ForwardTick(int len)
        {
            throw new NotImplementedException();
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return null;
        }

        public IRealData GetRealData()
        {
            return null;
        }

        public ITickData GetTickData()
        {
            return null;
        }
    }
}