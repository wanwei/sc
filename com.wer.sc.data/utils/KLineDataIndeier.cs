using com.wer.sc.data.reader;
using System.Collections.Generic;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// K线索引器
    /// </summary>
    public class KLineDataIndeier
    {
        /// <summary>
        /// k线数据里面包含的所有日期
        /// </summary>
        private List<int> dates = new List<int>();

        private OpenDateCache openDateCache;

        /// <summary>
        /// k线数据里所有日期开始位置对应节点
        /// </summary>
        private Dictionary<int, int> dicDateStartIndex = new Dictionary<int, int>();

        private IKLineData klineData;

        private IOpenDateReader openDateReader;

        public KLineDataIndeier(IKLineData data, IOpenDateReader openDateReader)
        {
            this.klineData = data;
            this.openDateReader = openDateReader;
            this.DoIndex();
        }

        public IKLineData KLineData
        {
            get
            {
                return klineData;
            }
        }

        private void DoIndex()
        {
            KLineTimeGetter timeGetter = new KLineTimeGetter(this.klineData);
            List<SplitterResult> splitResults = DaySpliter.Split(timeGetter, openDateReader);
            for (int i = 0; i < splitResults.Count; i++)
            {
                SplitterResult result = splitResults[i];
                dates.Add(result.Date);
                dicDateStartIndex.Add(result.Date, result.Index);
                this.openDateCache = new OpenDateCache(dates);
            }
        }

        public int GetTimeDate(double time)
        {
            return DaySpliter.GetTimeDate(time, openDateCache);
        }

        public int GetTimeDateIndex(double time)
        {

            return -1;
        }

        public int GetTimeIndex(double time)
        {
            if (klineData.Period.PeriodType == KLinePeriod.TYPE_DAY)
            {
                //TODO 现在只支持1日线，2日及以上不支持
                int timeDate = (int)time;
                int index = GetTimeIndex(timeDate, 0);
                return DaySpliter.IsNight(time) ? index + 1 : index;
            }

            int date = GetTimeDate(time);
            int dateStartIndex = dicDateStartIndex[date];
            return GetTimeIndex(time, dateStartIndex);
        }

        private int GetTimeIndex(double time, int startIndex)
        {
            while (startIndex < klineData.Length)
            {
                double t = klineData.Arr_Time[startIndex];
                if (time < t)
                    return startIndex - 1;
                if (time == t)
                    return startIndex;
                startIndex++;
            }

            return -1;
        }
    }

    public class KLineTimeGetter : TimeGetter
    {
        private IKLineData klineData;
        public KLineTimeGetter(IKLineData klineData)
        {
            this.klineData = klineData;
        }

        public int Count
        {
            get
            {
                return klineData.Length;
            }
        }

        public double GetTime(int index)
        {
            return klineData.Arr_Time[index];
        }
    }
}