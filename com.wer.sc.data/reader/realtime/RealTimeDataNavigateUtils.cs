using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    public class RealTimeDataNavigateUtils
    {
        //public static bool ForwardKLineDataByTickData(KLineData_RealTime klineData, ITickData todayTick, int length, ITickData nextDayTick, DataReaderFactory dataReaderFactory, KLineBar tmpCurrentKLineBar)
        //{
        //    if (todayTick == null)
        //        return ForwardKLineDataToNextDayOpenTime(klineData, nextDayTick, dataReaderFactory, tmpCurrentKLineBar);
        //    return ForwardKLineDataByForwardedTick(klineData, todayTick, todayTick.BarPos - length, todayTick.BarPos, tmpCurrentKLineBar);
        //}

        /// <summary>
        /// 前进到第二天开盘时间
        /// </summary>
        /// <param name="klineData"></param>
        /// <param name="dataReaderFactory"></param>
        /// <returns></returns>
        public static bool ForwardKLineDataToNextDayOpenTime(KLineData_RealTime klineData, ITickData nextDayTick, DataReaderFactory dataReaderFactory, KLineBar tmpCurrentKLineBar)
        {
            string code = klineData.Code;
            double currentTime = klineData.Time;
            int openDate = dataReaderFactory.OpenTimeReader.GetRecentOpenDate(code, currentTime);
            int nextOpenDate = dataReaderFactory.OpenDateReader.GetNextOpenDate(openDate);
            if (nextOpenDate < 0)
                return false;

            int forwardedBarPos;
            if (klineData.Period.PeriodType == KLineTimeType.DAY)
            {
                forwardedBarPos = klineData.BarPos + 1;
            }
            else
            {
                double nextOpenTime = dataReaderFactory.OpenTimeReader.GetOpenTime(code, nextOpenDate).Start;
                forwardedBarPos = klineData.BarPos + 1;             
                while (currentTime < nextOpenTime)
                {
                    currentTime = klineData.Arr_Time[forwardedBarPos];
                    forwardedBarPos++;
                }
            }
            nextDayTick.BarPos = 0;
            tmpCurrentKLineBar.Time = nextDayTick.Time;
            tmpCurrentKLineBar.Start = nextDayTick.Price;
            tmpCurrentKLineBar.High = nextDayTick.Price;
            tmpCurrentKLineBar.Low = nextDayTick.Price;
            tmpCurrentKLineBar.End = nextDayTick.Price;
            tmpCurrentKLineBar.Mount = nextDayTick.Mount;
            tmpCurrentKLineBar.Money = nextDayTick.Mount * nextDayTick.Price;
            tmpCurrentKLineBar.Hold = nextDayTick.Hold;
            klineData.SetRealTimeData(tmpCurrentKLineBar, forwardedBarPos);
            return true;
        }

        /// <summary>
        /// 该方法用于tick向前前进时修改对应的K线数据
        /// </summary>
        /// <param name="klineData">待修改的K线</param>
        /// <param name="forwardedTickData">正前进的tickData</param>
        /// <param name="lastTickIndex">之前的tickData索引号</param>
        /// <param name="currentTickIndex">前进后的tickData索引号,该索引号如果小于0或者超过tick数据长度，则认为时间到明天了</param>
        /// <returns>返回true表示成功前进</returns>
        public static bool ForwardKLineDataByForwardedTick(KLineData_RealTime klineData, ITickData forwardedTickData, int lastTickIndex, int currentTickIndex, KLineBar tmpCurrentKLineBar)
        {
            if (currentTickIndex < 0 || currentTickIndex > forwardedTickData.Length - 1)
                return false;

            double currentTickTime = forwardedTickData.Arr_Time[currentTickIndex];

            int lastKLineIndex = klineData.BarPos;
            int forwardedKLineIndex = GetForwardedKLineIndex(klineData, currentTickTime);
            if (forwardedKLineIndex == lastKLineIndex)
            {
                AggreKLineBar(tmpCurrentKLineBar, klineData.GetCurrentBar(), forwardedTickData, lastTickIndex, currentTickIndex);
            }
            else
            {
                double klineTime = klineData.Arr_Time[forwardedKLineIndex];
                lastTickIndex = FindPrevTickIndex(forwardedTickData, currentTickIndex, klineTime);
                AggreKLineBar(tmpCurrentKLineBar, forwardedTickData, lastTickIndex, currentTickIndex);
            }
            klineData.SetRealTimeData(tmpCurrentKLineBar, forwardedKLineIndex);
            return true;
        }

        public static int FindPrevTickIndex(ITickData tickData, int currentTickIndex, double time)
        {
            int prevTickIndex = tickData.BarPos - 1;
            while (prevTickIndex >= 0)
            {
                double prevTickTime = tickData.Arr_Time[prevTickIndex];
                if (prevTickTime <= time)
                    return prevTickIndex;
                prevTickIndex--;
            }
            return prevTickIndex;
        }

        public static void AggreKLineBar(KLineBar targetBar, IKLineBar klineBar, ITickData data, int lastTickIndex, int currentTickIndex)
        {
            targetBar.Time = data.Arr_Time[currentTickIndex];
            targetBar.Start = klineBar.Start;
            targetBar.End = data.Arr_Price[currentTickIndex];
            targetBar.Hold = data.Arr_Hold[currentTickIndex];

            float high = klineBar.High;
            float low = klineBar.Low;
            int mount = klineBar.Mount;
            float money = klineBar.Money;

            lastTickIndex = lastTickIndex < 0 ? 0 : lastTickIndex;
            for (int i = lastTickIndex; i <= currentTickIndex; i++)
            {
                float price = data.Arr_Price[i];
                if (price > high)
                    high = price;
                if (price < low)
                    low = price;
                mount += data.Arr_Mount[i];
                money += CalcMoney(price, mount);
            }
            targetBar.High = high;
            targetBar.Low = low;
            targetBar.Mount = mount;
            targetBar.Money = money;
        }

        public static void AggreKLineBar(KLineBar targetBar, ITickData data, int lastTickIndex, int currentTickIndex)
        {
            float price = data.Arr_Price[currentTickIndex];
            targetBar.Time = data.Arr_Time[currentTickIndex];
            targetBar.Start = price;
            targetBar.End = data.Arr_Price[currentTickIndex];
            targetBar.Hold = data.Arr_Hold[currentTickIndex];

            float high = price;
            float low = price;
            int mount = data.Arr_Mount[currentTickIndex];
            float money = CalcMoney(price, mount);

            lastTickIndex = lastTickIndex < 0 ? 0 : lastTickIndex;
            for (int i = lastTickIndex + 1; i <= currentTickIndex; i++)
            {
                price = data.Arr_Price[i];
                if (price > high)
                    high = price;
                if (price < low)
                    low = price;
                mount += data.Arr_Mount[i];
                money += CalcMoney(price, mount);
            }
            targetBar.High = high;
            targetBar.Low = low;
            targetBar.Mount = mount;
            targetBar.Money = money;
        }

        private static float CalcMoney(float price, int mount)
        {
            return price * mount / 10000;
        }

        private static int GetForwardedKLineIndex(KLineData_RealTime klineData, double time)
        {
            int forwardedKLineIndex = klineData.BarPos + 1;
            while (forwardedKLineIndex < klineData.Length - 1)
            {
                /*
                 * 例：
                 * 现在时间是09:00:59，1分钟K线Index是0
                 * tick数据进1格到09:01:00，和K线比较，K线应该进一格index，为1
                 */
                double nextKLineTime = klineData.Arr_Time[forwardedKLineIndex];
                if (time >= nextKLineTime)
                {
                    forwardedKLineIndex++;
                    continue;
                }
                return forwardedKLineIndex - 1;
            }
            return klineData.BarPos;
        }

        private static int FindKLineIndex(KLineData_RealTime klineData, double time)
        {
            int currentIndex = klineData.BarPos;
            double klineTime = klineData.Arr_Time[currentIndex];

            while (time > klineTime && currentIndex < klineData.Length)
            {
                currentIndex++;
                klineTime = klineData.Arr_Time[currentIndex];
            }
            return currentIndex - 1;
        }

        /// <summary>
        /// 该方法用于K线前进时，另一个K线
        /// </summary>
        /// <param name="klineData"></param>
        /// <param name="forwardKLineData"></param>
        /// <param name="lastKLineIndex"></param>
        /// <param name="currentKLineIndex"></param>
        public static void ForwardKLineDataByForwardedKLineData(KLineData_RealTime klineData, IKLineData forwardKLineData, int lastKLineIndex, int currentKLineIndex)
        {

        }

        /// <summary>
        /// 前进到第二天开盘第一个K线
        /// 该方法用于
        /// </summary>
        /// <param name="klineData"></param>
        /// <param name="dataReaderFactory"></param>
        /// <returns></returns>
        public static bool ForwardKLineDataToNextDay(KLineData_RealTime klineData, DataReaderFactory dataReaderFactory)
        {
            //dataReaderFactory.OpenTimeReader.g
            return true;
        }
    }
}