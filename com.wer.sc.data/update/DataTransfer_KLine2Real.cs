using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 将k线数据转换成分时线数据
    /// </summary>
    public class DataTransfer_KLine2Real
    {
        public static List<RealData> Convert(KLineData data, float lastEndPrice)
        {
            DaySpliter splitter = new DaySpliter();
            List<int[]> splitResult = splitter.Split(new KLineDataTimeGetter(data));

            List<RealData> realdataList = new List<RealData>(splitResult.Count);
            for (int i = 0; i < splitResult.Count; i++)
            {
                int[] split = splitResult[i];
                int date = split[0];
                int todayStartIndex = split[1];
                int todayEndIndex;
                if (i == splitResult.Count - 1)
                    todayEndIndex = data.Length - 1;
                else
                    todayEndIndex = splitResult[i + 1][1];
                int len = todayEndIndex - todayStartIndex + 1;
                RealData r = new RealData(date, lastEndPrice, len);
                Convert2RealData(data, todayStartIndex, todayEndIndex, r);
                realdataList.Add(r);
            }

            return realdataList;
        }

        private static void Convert2RealData(KLineData data, int startIndex, int endIndex, RealData r)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                int currentRealIndex = i - startIndex;
                r.arr_time[currentRealIndex] = data.arr_time[i];
                r.arr_price[currentRealIndex] = data.arr_end[i];
                r.arr_mount[currentRealIndex] = data.arr_mount[i];
                r.arr_hold[currentRealIndex] = data.arr_hold[i];
            }
        }

        class KLineDataTimeGetter : TimeGetter
        {
            private KLineData data;
            public KLineDataTimeGetter(KLineData data)
            {
                this.data = data;
            }

            public int Count
            {
                get { return data.Length; }
            }

            public double GetTime(int index)
            {
                return data.arr_time[index];
            }
        }
    }
}
