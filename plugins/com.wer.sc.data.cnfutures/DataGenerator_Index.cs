using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures
{
    /// <summary>
    /// 生成指数
    /// </summary>
    public class DataGenerator_Index
    {
        public bool Generator()
        {
            return true;
        }

        public TickData Generator(String variety, String date)
        {
            return null;
        }

        public TickData Generator(List<TickData> tickData, List<double[]> openTime)
        {
            List<double> times = TimeUtils.GetKLineTimes(openTime, new KLinePeriod(KLinePeriod.TYPE_SECOND, 1));
            TickData data = new TickData(times.Count);
            int[] currentIndeies = new int[tickData.Count];
            int[] lastIndeies = new int[tickData.Count];
            for (int i = 0; i < times.Count; i++)
            {
                double time = times[i];
                calcCurrent(data, i, tickData, null, null);
            }
            return data;
        }

        private void calcIndeies(List<TickData> data, int currentIndex, double currentTime, int[] currentIndeies, int[] lastIndeies)
        {
            for (int i = 0; i < data.Count; i++)
            {

            }
        }

        private int calcIndex(TickData data, int currentTickIndex, double currentTime)
        {
            double t1 = data.Time;
            double t2 = data.arr_time[currentTickIndex + 1];
            return -1;
        }

        private bool isNextOverTime(TickData data, int currentTickIndex, double time)
        {
            double currentTickTime = data.Time;
            double nextTickTime = NumberUtils.Xiaoshu(data.arr_time[currentTickIndex + 1]);
            bool isOverNight = nextTickTime < currentTickTime - 0.1;
            if (isOverNight)
            {
                return false;
            }
            else
            {
                return nextTickTime >= time;
            }
        }

        private void calcCurrent(TickData data, int index, List<TickData> currentData, int[] currentHolds, int[] mounts)
        {
            float price = 0;
            int mount = 0;
            int totalMount = 0;
            int hold = 0;
            for (int i = 0; i < currentData.Count; i++)
            {
                TickData tickdata = currentData[i];
                int currentHold = currentHolds[i];
                price += tickdata.Price * currentHold;
                mount += mounts[i];
                totalMount += tickdata.TotalMount;
                hold += currentHold;
            }
            price = (float)Math.Round((float)(price / hold), 2);
            data.arr_time[index] = 1;
            data.arr_price[index] = price;
            data.arr_mount[index] = mount;
            data.arr_totalMount[index] = totalMount;
        }
    }
}