using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache.impl
{
    public class DataCache_Code : IDataCache_Code
    {
        private DataReaderFactory dataReaderFactory;

        private string code;

        private int startDate;

        private int endDate;

        private OpenDateCache cache;

        private MinuteKLineData_DateGetter minuteDataGetter;

        private IKLineData dayKLineData;

        private List<int> cachedDates = new List<int>();

        private Dictionary<int, DataCache_CodeDate> dicDateCache = new Dictionary<int, DataCache_CodeDate>();

        private int maxCacheDateCount = 20;        

        internal DataCache_Code(DataReaderFactory dataReaderFactory, string code)
        {
            IKLineDataReader dataReader = dataReaderFactory.KLineDataReader;
            KLinePeriod period = new KLinePeriod(KLinePeriod.TYPE_DAY, 1);
            int start = dataReader.GetFirstDate(code, period);
            int end = dataReader.GetLastDate(code, period);
            Init(dataReaderFactory, code, start, end);
        }

        internal DataCache_Code(DataReaderFactory dataReaderFactory, String code, int startDate, int endDate)
        {
            Init(dataReaderFactory, code, startDate, endDate);
        }

        private void Init(DataReaderFactory dataReaderFactory, string code, int startDate, int endDate)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.dataReaderFactory = dataReaderFactory;

            IKLineData minuteKLineData = dataReaderFactory.KLineDataReader.GetData(code, startDate, endDate, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
            this.minuteDataGetter = new MinuteKLineData_DateGetter(minuteKLineData);
            this.cache = new OpenDateCache(this.minuteDataGetter.GetOpenDates());
            this.dayKLineData = dataReaderFactory.KLineDataReader.GetData(code, startDate, endDate, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));
        }

        public int StartDate
        {
            get { return startDate; }
        }

        public int EndDate
        {
            get { return endDate; }
        }        

        public IOpenDateReader GetCodeOpenDateReader()
        {
            return cache;
        }

        public int MaxCacheDateCount
        {
            get
            {
                return maxCacheDateCount;
            }

            set
            {
                maxCacheDateCount = value;
            }
        }

        public IKLineData GetDayKLineData()
        {
            return dayKLineData;
        }

        private Object lockObj = new object();

        public IDataCache_CodeDate GetCache_CodeDate(int date)
        {
            if (dicDateCache.ContainsKey(date))
                return dicDateCache[date];
            lock (lockObj)
            {
                if (dicDateCache.ContainsKey(date))
                    return dicDateCache[date];

                IKLineData klineData = minuteDataGetter.GetKLineData(date);
                DataCache_CodeDate cache = new DataCache_CodeDate(dataReaderFactory, code, date, klineData);
                if (cachedDates.Count > maxCacheDateCount)
                {
                    int removeDate = cachedDates[0];
                    cachedDates.RemoveAt(0);
                    dicDateCache.Remove(removeDate);
                }
                cachedDates.Add(date);
                dicDateCache.Add(date, cache);
                return cache;
            }
        }
    }

    public class MinuteKLineData_DateGetter
    {
        private DayMinuteKLineDataGetter dataGetter;

        private Dictionary<int, IKLineData> dicDateKLineData = new Dictionary<int, IKLineData>();

        public MinuteKLineData_DateGetter(IKLineData klineData)
        {
            dataGetter = new DayMinuteKLineDataGetter(klineData);
        }

        public IKLineData GetKLineData(int date)
        {
            if (dicDateKLineData.ContainsKey(date))
                return dicDateKLineData[date];
            IKLineData klineData = dataGetter.GetMinuteKLineData(date);
            if (klineData == null)
                return null;
            dicDateKLineData.Add(date, klineData);
            return klineData;
        }

        public List<int> GetOpenDates()
        {
            return dataGetter.OpenDates;
        }
    }
}
