using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class DayDataCache
    {
        private DataReaderFactory dataReaderFac;
        public DayDataCache(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
        }

        public TickData GetTickData(String code, int date)
        {
            return dataReaderFac.TickDataReader.GetTickData(code, date);
        }

        public IKLineData GetKLineData(String code, int date)
        {
            return dataReaderFac.KLineDataReader.GetData(code, date, date, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
        }
    }

    public class DayDataKey
    {
        public string Code;

        public int Date;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
