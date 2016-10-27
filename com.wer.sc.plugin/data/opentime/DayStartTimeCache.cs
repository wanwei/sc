using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.opentime
{
    /// <summary>
    /// 开盘时间的缓存实现
    /// </summary>
    public class DayStartTimeCache : IDayStartTimeReader
    {
        private IOpenDateReader openDateCache;

        private List<DayStartTime> dayStartTimes;

        private List<int> openDates;

        private List<double> startTimes;

        private Dictionary<int, double> dicStartTimes;

        private Dictionary<double, int> dicOpenDates;

        public DayStartTimeCache(List<DayStartTime> dayStartTimes)
        {
            this.dayStartTimes = dayStartTimes;
            this.openDates = new List<int>(dayStartTimes.Count);
            this.startTimes = new List<double>(dayStartTimes.Count);
            this.dicStartTimes = new Dictionary<int, double>(dayStartTimes.Count);
            this.dicOpenDates = new Dictionary<double, int>(dayStartTimes.Count);
            for (int i = 0; i < dayStartTimes.Count; i++)
            {
                DayStartTime startTime = dayStartTimes[i];
                this.openDates.Add(startTime.Date);
                this.startTimes.Add(startTime.Start);
                this.dicStartTimes.Add(startTime.Date, startTime.Start);
                this.dicOpenDates.Add(startTime.Start, startTime.Date);
            }
        }

        public List<double> GetAllStartTimes()
        {
            return startTimes;
        }

        public IOpenDateReader GetOpenDateCache()
        {
            if (openDateCache == null)
                openDateCache = new OpenDateCache(openDates);
            return openDateCache;
        }

        public int GetOpenDate(double time)
        {
            int openDate = GetOpenDate2(time);
            if (openDate >= 0)
                return openDate;

            int date = (int)time;
            if (IsTimeInThisDay(date, time))
                return date;
            int nextdate = GetOpenDateCache().GetNextOpenDate(date);
            if (IsTimeInThisDay(nextdate, time))
                return nextdate;
            int prevdate = GetOpenDateCache().GetPrevOpenDate(date);
            if (IsTimeInThisDay(prevdate, time))
                return prevdate;
            return -1;
        }

        private bool IsTimeInThisDay(int date, double time)
        {
            if (date < 0)
                return false;
            double todayStartTime = GetStartTime(date);
            double nextStartTime = GetStartTime(GetOpenDateCache().GetNextOpenDate(date));
            return (time > todayStartTime && time < nextStartTime);
        }

        private int GetOpenDate2(double startTime)
        {
            int value;
            bool exist = dicOpenDates.TryGetValue(startTime, out value);
            return exist ? value : -1;
        }

        public bool IsStartTime(double time)
        {
            return dicOpenDates.Keys.Contains(time);
        }

        public double GetStartTime(int date)
        {
            double value;
            bool exist = dicStartTimes.TryGetValue(date, out value);
            return exist ? value : -1;
        }

        public List<double> GetStartTimes(int startDate)
        {
            IOpenDateReader openDateCache = GetOpenDateCache();
            int startIndex = openDateCache.GetOpenDateIndex(startDate);
            int count = openDates.Count - startIndex;
            return startTimes.GetRange(startIndex, count);
        }

        public List<double> GetStartTimes(int startDate, int endDate)
        {
            IOpenDateReader openDateCache = GetOpenDateCache();
            int startIndex = openDateCache.GetOpenDateIndex(startDate);
            int endIndex = openDateCache.GetOpenDateIndex(endDate);
            return startTimes.GetRange(startIndex, endIndex - startIndex + 1);
        }
    }
}
