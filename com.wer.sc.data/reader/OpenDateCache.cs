using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class OpenDateCache : IOpenDateReader
    {
        private List<int> openDatesList;

        private Dictionary<int, int> dicOpenDateIndex;

        public OpenDateCache(List<int> openDateList)
        {
            this.openDatesList = openDateList;
            this.dicOpenDateIndex = new Dictionary<int, int>();
            for (int i = 0; i < openDatesList.Count; i++)
            {
                dicOpenDateIndex.Add(openDatesList[i], i);
            }
        }

        public bool IsOpen(int date)
        {
            return dicOpenDateIndex.ContainsKey(date);
        }

        public List<int> GetAllOpenDates()
        {
            return openDatesList;
        }

        public int FirstOpenDate { get { return openDatesList[0]; } }

        public int LastOpenDate { get { return openDatesList[openDatesList.Count - 1]; } }

        public IList<int> GetOpenDates(int start, int end)
        {
            if (end < start)
                return ListUtils.EmptyIntList;

            int startIndex = GetOpenDateIndex(start, false);
            int endIndex = GetOpenDateIndex(end, true);

            int[] opendates = new int[endIndex - startIndex + 1];
            for (int i = startIndex; i <= endIndex; i++)
            {
                opendates[i - startIndex] = openDatesList[i];
            }
            return opendates;
        }

        /**
         * 获取两日间的所有开盘日
         * @param beginDate
         * @param endDate
         * @return
         */
        public int GetOpenDateCount(int beginDate, int endDate)
        {
            return GetOpenDateIndex(endDate, true) - GetOpenDateIndex(beginDate, false) + 1;
        }

        public int GetOpenDate(int index)
        {
            if (index < 0 || index >= openDatesList.Count)
                return -1;
            return openDatesList[index];
        }

        public int GetOpenDateIndex(int date)
        {
            if (dicOpenDateIndex.ContainsKey(date))
                return dicOpenDateIndex[date];
            return -1;
        }

        public int GetOpenDateIndex(int date, bool isFindPrev)
        {
            int index = GetOpenDateIndex(date);
            if (index >= 0)
                return index;
            date = GetRecentOpenDate(date, isFindPrev);
            return GetOpenDateIndex(date);
        }

        public int GetNextOpenDate(int date)
        {
            return GetNextOpenDate(date, 1);
        }

        public int GetNextOpenDate(int date, int length)
        {
            int nextOpenDate = GetRecentOpenDate(date, length < 0);
            if (nextOpenDate < 0)
                return -1;
            int index = GetOpenDateIndex(nextOpenDate);
            index += length;
            if (nextOpenDate != date)
                index += length < 0 ? 1 : -1;
            if (index < 0 || index >= openDatesList.Count)
                return -1;
            return openDatesList[index];
        }

        private int GetRecentOpenDate(int date, bool isFindPrev)
        {
            if (IsOpen(date))
                return date;
            int firstOpen = FirstOpenDate;
            if (date < firstOpen)
                return isFindPrev ? -1 : firstOpen;

            int lastOpen = LastOpenDate;
            if (date > lastOpen)
                return isFindPrev ? lastOpen : -1;

            int addPeriod = isFindPrev ? -1 : 1;
            while (!IsOpen(date))
                date = (int)TimeUtils.AddDays(date, addPeriod);
            return date;
        }

        public int GetPrevOpenDate(int date)
        {
            return GetPrevOpenDate(date, 1);
        }

        public int GetPrevOpenDate(int date, int length)
        {
            return GetNextOpenDate(date, -length);
        }
    }
}
