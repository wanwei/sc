using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache.impl
{
    public class DataCache_CodeDate : IDataCache_CodeDate
    {
        private DataReaderFactory dataReaderFactory;

        private string code;

        private int date;

        private ITickData tickData;

        private IKLineData minuteKLineData;

        public DataCache_CodeDate(DataReaderFactory dataReaderFactory, string code, int date, IKLineData minuteKLineData)
        {
            this.dataReaderFactory = dataReaderFactory;
            this.code = code;
            this.date = date;
            this.minuteKLineData = minuteKLineData;
        }

        public String Code
        {
            get { return code; }
        }

        public int Date
        {
            get
            {
                return date;
            }
        }
        public IKLineData GetMinuteKLineData()
        {
            return minuteKLineData;
        }

        private Object lockObj_Tick = new object();


        public ITickData GetTickData()
        {
            if (tickData == null)
            {
                lock (lockObj_Tick)
                {
                    if (tickData == null)
                        tickData = dataReaderFactory.TickDataReader.GetTickData(code, date);
                }
            }
            return tickData;
        }

        public IRealData GetRealData()
        {
            return null;
        }
    }
}
