using com.wer.sc.data.impl;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache.impl
{
    public class DayMinuteKLineDataGetter
    {
        private IKLineData klineData;

        private List<int> openDates = new List<int>();
        private Dictionary<int, int[]> dicDateStartEnd = new Dictionary<int, int[]>();

        public DayMinuteKLineDataGetter(IKLineData klineData)
        {
            this.klineData = klineData;
            this.initIndex();
        }

        private void initIndex()
        {
            KLineTimeGetter timeGetter = new KLineTimeGetter(klineData);
            DaySpliter spliter = new DaySpliter();
            List<SplitterResult> splitResults = spliter.Split(timeGetter);
            for (int i = 0; i < splitResults.Count; i++)
            {
                SplitterResult result = splitResults[i];
                openDates.Add(result.Date);
                int start = result.Index;
                int end = (i == splitResults.Count - 1) ? timeGetter.Count - 1 : splitResults[i + 1].Index;
                dicDateStartEnd.Add(result.Date, new int[] { start, end });
            }
        }

        public IKLineData GetMinuteKLineData(int date)
        {
            if (!dicDateStartEnd.ContainsKey(date))
                return null;
            int[] startEnd = dicDateStartEnd[date];
            return new KLineData_Sub(klineData, startEnd[0], startEnd[1]);
        }

        public List<int> OpenDates
        {
            get
            {
                return openDates;
            }
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
