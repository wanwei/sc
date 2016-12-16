using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class OpenDateReader : IOpenDateReader
    {
        private OpenDateCache cache;

        public OpenDateReader(String path)
        {
            OpenDateStore store = new OpenDateStore(path);
            List<int> openDates = store.Load();
            cache = new OpenDateCache(openDates);
        }

        public bool IsOpen(int date)
        {
            return cache.IsOpen(date);
        }

        public List<int> GetAllOpenDates()
        {
            return cache.GetAllOpenDates();
        }

        public int FirstOpenDate { get { return cache.FirstOpenDate; } }

        public int LastOpenDate { get { return cache.LastOpenDate; } }

        public IList<int> GetOpenDates(int start, int end)
        {
            return cache.GetOpenDates(start, end);
        }

        /**
         * 获取两日间的所有开盘日
         * @param beginDate
         * @param endDate
         * @return
         */
        public int GetOpenDateCount(int beginDate, int endDate)
        {
            return cache.GetOpenDateCount(beginDate, endDate);
        }

        public int GetOpenDate(int index)
        {
            return cache.GetOpenDate(index);
        }

        public int GetOpenDateIndex(int date)
        {
            return cache.GetOpenDateIndex(date);
        }

        public int GetOpenDateIndex(int date, bool isFindPrev)
        {
            return cache.GetOpenDateIndex(date, isFindPrev);
        }

        public int GetNextOpenDate(int date)
        {
            return cache.GetNextOpenDate(date);
        }

        public int GetNextOpenDate(int date, int length)
        {
            return cache.GetNextOpenDate(date, length);
        }

        public int GetPrevOpenDate(int date)
        {
            return cache.GetPrevOpenDate(date);
        }

        public int GetPrevOpenDate(int date, int length)
        {
            return cache.GetPrevOpenDate(date, length);
        }

    }
}