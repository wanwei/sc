using com.wer.sc.data.provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.transfer
{

    public class DataGenerator_Normal
    {
        private String dataSrcPath;

        private DataProvider_CodeInfo provider_CodeInfo;

        private DataProvider_OpenTime provider_OpenTime;

        private TickDataAdjust tickDataAdjust;

        public DataGenerator_Normal(String dataSrcPath, DataProvider_CodeInfo provider_CodeInfo, DataProvider_OpenTime provider_OpenTime)
        {
            this.dataSrcPath = dataSrcPath;
            this.provider_CodeInfo = provider_CodeInfo;
            this.provider_OpenTime = provider_OpenTime;
            this.tickDataAdjust = new TickDataAdjust();
        }

        public TickData Generate(String code, int date)
        {
            TickData data = GetOrignalTickData(code, date);
            if (data == null)
                return null;

            List<double[]> openTime = provider_OpenTime.GetOpenTime(code, date);
            tickDataAdjust.Adjust(data, openTime);
            return data;
        }

        public TickData GetOrignalTickData(string code, int date)
        {
            String path = GetCodePath(code, date);
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return ReadLinesToTickData(lines);
        }

        public String GetCodePath(String code, int date)
        {
            return dataSrcPath + "\\" + provider_CodeInfo.GetBelongMarket(code) + "\\" + date + "\\" + code + "_" + date + ".csv";
        }

        public static TickData ReadLinesToTickData(string[] lines)
        {
            int cnt = GetEmptyLines(lines);
            TickData data = new TickData(lines.Length - 1 - cnt);
            for (int i = 0; i < lines.Length - 1 - cnt; i++)
            {
                String line = lines[i + 1];
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                if (dataArr.Length < 5)
                    continue;

                String[] dateArr = dataArr[0].Split('-');
                double date = double.Parse(dateArr[0] + Fill(dateArr[1]) + Fill(dateArr[2]));
                String[] timeArr = dataArr[1].Split(':');
                double time = double.Parse(timeArr[0] + timeArr[1] + timeArr[2]);
                double fulltime = date + time / 1000000;

                data.arr_time[i] = fulltime;
                data.arr_price[i] = float.Parse(dataArr[2]);
                data.arr_mount[i] = int.Parse(dataArr[3]);
                data.arr_totalMount[i] = int.Parse(dataArr[4]);
                data.arr_add[i] = int.Parse(dataArr[5]);
                data.arr_buyPrice[i] = (int)float.Parse(dataArr[6]);
                data.arr_buyMount[i] = int.Parse(dataArr[7]);
                data.arr_sellPrice[i] = (int)float.Parse(dataArr[12]);
                data.arr_sellMount[i] = int.Parse(dataArr[13]);
                data.arr_isBuy[i] = dataArr[18].Equals("B");
            }
            return data;
        }

        private static String Fill(String s)
        {
            if (s.Length == 1)
                return "0" + s;
            return s;
        }

        private static int GetEmptyLines(string[] lines)
        {
            int cnt = 0;
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (lines[i].Trim().Equals(""))
                    cnt++;
                else
                    break;
            }
            return cnt;
        }
    }

    public class TickDataAdjust
    {
        public TickDataAdjust()
        {
        }

        /// <summary>
        /// 预处理：
        /// 1.处理掉一些肯定不正确的数据，如提前3分钟开盘，然后到开盘都没有数据了。
        /// 
        /// 三种调整算法：
        /// 1.开始时间和结束时间都向前或向后移了基本相同的时间，整体迁移。
        /// 2.开始时间或者结束时间出现大量重复，分两种情况：1.如果另一头出现了时间偏差，那么整体迁移；2.如果没有，则稀释
        /// </summary>
        /// <param name="data"></param>
        /// <param name="openTime"></param>
        public void Adjust(TickData data, List<double[]> openTime)
        {
            //小于500条数据就不调整了
            if (data.Length < 500)
                return;
            //2013年以后数据就不调整了
            if (data.Date > 20120000)
                return;

            //郑州20140827开始1分钟4个tick
            List<TickInfo_Period> periods = TickDataAnalysis.Analysis(data, openTime);
            if (periods[0].StartIndex == -1)
            {
                Adjust_Special(data, periods);
                return;
            }
            for (int i = 0; i < periods.Count; i++)
            {
                TickInfo_Period period = periods[i];
                Adjust(data, period);
            }
        }

        private void Adjust_Special(TickData data, List<TickInfo_Period> periods)
        {
            /**
             * 1.搜索出大量repeat的数据
             * 2.把repeat前的数据移到
             */

            return;
        }

        private int[] FindMaxRepeatIndex(TickData data)
        {
            return null;
        }

        /// <summary>
        /// 调整规则：
        /// 1.如果该开始和结束
        /// 
        /// 逻辑：
        /// 1.是否有repeat，如果没有repeat直接移动时间
        /// 2.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="period"></param>
        private void Adjust(TickData data, TickInfo_Period period)
        {
            TickPeriodAdjustInfo adjustInfo = period.adjustInfo;
            if (!adjustInfo.StartRepeat && !adjustInfo.EndRepeat)
                Adjust_NoRepeat(data, period, adjustInfo);
            else if (adjustInfo.StartRepeat && adjustInfo.EndRepeat)
                Adjust_AllRepeat(data, period, adjustInfo);
            //有时间偏移，首先根据偏移位置移正，再处理repeat
            else if (adjustInfo.HasTimeOffset())
                Adjust_HasTimeOffsetAndRepeat(data, period, adjustInfo);
            //起始位置repeat，末尾offset，且正好合拍
            else if (adjustInfo.HasRepeatOffset())
                Adjust_HasRepeatOffset(data, period, adjustInfo);
            //该数据段没有偏移，只有repeat
            else
                Adjust_NoOffsetOnlyRepeat(data, period, adjustInfo);
        }

        private void Adjust_NoRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            if (adjustInfo.HasTimeOffset())
                AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.GetTimeOffset());
            int adjustCount = AdjustPeriodStart(data, period);
            if (adjustCount > 2)
            {
                int startIndex = adjustInfo.IsOpen ? period.StartIndex + 1 : period.StartIndex;
                int endIndex = startIndex + adjustCount - 1;
                SpreadRepeatForward(data, period, startIndex, endIndex);
            }

            adjustCount = AdjustPeriodEnd(data, period);
            if (adjustCount > 2)
            {
                int startIndex = period.EndIndex - adjustCount + 1;
                int endIndex = period.EndIndex;
                SpreadRepeatBackward(data, period, startIndex, endIndex);
            }
        }

        private void Adjust_AllRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            int adjustCount = AdjustPeriodStart(data, period);
            int startIndex = adjustInfo.StartRepeatIndex - adjustCount;
            int endIndex = adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1;
            SpreadRepeatForward(data, period, startIndex, endIndex);

            adjustCount = AdjustPeriodEnd(data, period);
            startIndex = adjustInfo.EndRepeatIndex - adjustCount;
            endIndex = adjustInfo.EndRepeatIndex + adjustInfo.EndRepeatTimes - 1;
            SpreadRepeatBackward(data, period, startIndex, endIndex);
        }

        /// <summary>
        /// 既有偏移又有repeat的情况
        /// </summary>
        /// <param name="data"></param>
        /// <param name="period"></param>
        /// <param name="adjustInfo"></param>
        private void Adjust_HasTimeOffsetAndRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.GetTimeOffset());
            AdjustPeriodStart(data, period);
            if (adjustInfo.StartRepeat)
            {
                int startIndex = adjustInfo.StartRepeatIndex;
                int endIndex = adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1;
                //如果调整后向前移动的空间能够容纳下repeat，则向前填充
                if (-adjustInfo.GetTimeOffset() * 2 > adjustInfo.StartRepeatTimes)
                {
                    SpreadRepeatBackward(data, period, startIndex, endIndex);
                }
                else
                {
                    int mIndex = startIndex - adjustInfo.GetTimeOffset() * 2 - 4;
                    SpreadRepeatBackward(data, period, startIndex, mIndex);
                    SpreadRepeatForward(data, period, mIndex, endIndex);
                }
            }

            AdjustPeriodEnd(data, period);
            if (adjustInfo.EndRepeat)
            {
                int startIndex = adjustInfo.EndRepeatIndex;
                int endIndex = adjustInfo.EndRepeatIndex + adjustInfo.EndRepeatTimes - 1;
                //如果调整后向前移动的空间能够容纳下repeat，则向前填充
                if (adjustInfo.GetTimeOffset() * 2 > adjustInfo.EndRepeatTimes)
                {
                    SpreadRepeatForward(data, period, startIndex, endIndex);
                }
                else
                {
                    int mIndex = endIndex - adjustInfo.GetTimeOffset() * 2 + 4;
                    SpreadRepeatBackward(data, period, startIndex, mIndex);
                    SpreadRepeatForward(data, period, mIndex, endIndex);
                }
            }
        }

        private void Adjust_NoOffsetOnlyRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            //不设置偏移，直接调整
            int adjustCount = AdjustPeriodStart(data, period);
            if (adjustInfo.StartRepeat)
            {
                int startIndex = adjustInfo.StartRepeatIndex - adjustCount;
                int endIndex = adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1;
                SpreadRepeatForward(data, period, startIndex, endIndex);
            }

            adjustCount = AdjustPeriodEnd(data, period);
            if (adjustInfo.EndRepeat)
            {
                int startIndex = adjustInfo.EndRepeatIndex - adjustInfo.EndRepeatTimes + 1;
                int endIndex = adjustInfo.EndRepeatIndex + adjustCount;
                SpreadRepeatBackward(data, period, startIndex, endIndex);
            }
        }

        /// <summary>
        /// 例子：
        /// 20071017 m05 (13:30:00一共差不多70个)
        /// 2007-10-17,13:30:00,3226,502,459820,46,3226,10,0,0,0,0,3227,183,0,0,0,0,S
        /// 2007-10-17,13:30:00,3226,26,459846,-6,3225,789,0,0,0,0,3226,18,0,0,0,0,S
        /// ...
        /// 2007-10-17,13:30:00,3222,260,463172,-58,3222,1,0,0,0,0,3225,77,0,0,0,0,S
        /// 2007-10-17,13:30:00,3223,6,463178,-2,3223,1,0,0,0,0,3224,1,0,0,0,0,B
        /// ...
        /// 2007-10-17,14:59:15,3203,48,756442,-10,3202,528,0,0,0,0,3203,43,0,0,0,0,B
        /// </summary>
        /// <param name="data"></param>
        /// <param name="period"></param>
        /// <param name="adjustInfo"></param>
        private void Adjust_HasRepeatOffset(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            if (adjustInfo.StartRepeat)
            {
                AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.EndOffset);
                SpreadRepeatBackward(data, period, adjustInfo.StartRepeatIndex, adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1);
            }
            else
            {
                AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.StartOffset);
                SpreadRepeatForward(data, period, adjustInfo.EndRepeatIndex, adjustInfo.EndRepeatIndex + adjustInfo.EndRepeatTimes - 1);
            }
        }

        private void AdjustTime(TickData data, int startIndex, int endIndex, int offSecond)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                data.arr_time[i] = TimeUtils.AddSeconds(data.arr_time[i], offSecond);
            }
        }

        private int AdjustPeriodStart(TickData data, TickInfo_Period period)
        {
            //修改开始时间
            int startIndex = period.StartIndex;
            double auctionTime = TimeUtils.AddMinutes(data.Date + period.StartTime, -1);
            if (period.adjustInfo.IsOpen)
            {
                data.arr_time[period.StartIndex] = auctionTime;
                startIndex++;
            }
            double startTime = Math.Round(data.Date + period.StartTime, 6); ;
            double currentTime = data.arr_time[startIndex];
            int index = startIndex;
            while (currentTime < startTime)
            {
                data.arr_time[index] = startTime;
                index++;
                currentTime = data.arr_time[index];
            }
            return index - period.StartIndex - (period.adjustInfo.IsOpen ? 1 : 0);
        }

        private int AdjustPeriodEnd(TickData data, TickInfo_Period period)
        {
            //修改结束时间
            int endIndex = period.EndIndex;
            double endTime = Math.Round(data.Date + period.EndTime, 6); ;
            double currentTime = data.arr_time[endIndex];
            int index = endIndex;
            while (currentTime > endTime)
            {
                data.arr_time[index] = endTime;
                index--;
                currentTime = data.arr_time[index];
            }
            return endIndex - index;
        }

        /// <summary>
        /// 向前展开起始位置的重复数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repeatStartIndex"></param>
        /// <param name="repeatEndIndex"></param>
        /// <param name="isPrev"></param>
        private void SpreadRepeatForward(TickData data, TickInfo_Period period, int repeatStartIndex, int repeatEndIndex)
        {
            /**
             * 找到调整结束位置
             * 算法：
             * 1.从起始位置开始查找
             * 2.一直找到(结束时间-开始时间)*2>重复的时间数+
             * 
             * 090000
             * 090000
             * 090000
             * 090000
             * 090000
             * 090002
             * 090003
             * 090003
             */
            int timesEverySecond = 2;
            double timeRepeat = data.arr_time[repeatStartIndex];
            int repeatTimes = repeatEndIndex - repeatStartIndex + 1;
            int endIndex = -1;
            int capcity = 0;
            for (int i = repeatEndIndex + 1; i < data.Length; i++)
            {
                int currentDataCount = i - repeatEndIndex - 1 + repeatTimes;
                TimeSpan span = TimeUtils.Subtract(data.arr_time[i], timeRepeat);
                int currentCapcity = timesEverySecond * (span.Minutes * 60 + span.Seconds);
                if (currentCapcity >= currentDataCount)
                {
                    endIndex = i - 1;
                    capcity = currentCapcity;
                    break;
                }
            }

            int allIndex = endIndex - repeatStartIndex + 1;
            double rate = (double)capcity / allIndex / 2;

            double endTime = Math.Round((double)data.Date + period.EndTime, 6);
            double rptTime = data.arr_time[repeatStartIndex];
            for (int i = repeatStartIndex + 2; i <= endIndex; i++)
            {
                double time = TimeUtils.AddSeconds(rptTime, (int)(((double)(i - repeatStartIndex)) * rate));
                data.arr_time[i] = time < endTime ? time : endTime;
            }
        }

        private void SpreadRepeatBackward(TickData data, TickInfo_Period period, int repeatStartIndex, int repeatEndIndex)
        {
            /**
             * 
             * 145956
             * 145957
             * 145958
             * 145958
             * 150000
             * 150000
             * 150000
             * 150000
             * 150000
             */
            int timesEverySecond = 2;
            double timeRepeat = data.arr_time[repeatEndIndex];
            int repeatTimes = repeatEndIndex - repeatStartIndex + 1;
            int startIndex = -1;
            int capcity = 0;
            for (int i = repeatStartIndex - 1; i >= 0; i--)
            {
                int currentDataCount = repeatStartIndex - i - 1 + repeatTimes;
                TimeSpan span = TimeUtils.Subtract(timeRepeat, data.arr_time[i]);
                int currentCapcity = timesEverySecond * (span.Minutes * 60 + span.Seconds);
                if (currentCapcity >= currentDataCount)
                {
                    startIndex = i + 1;
                    capcity = currentCapcity;
                    break;
                }
            }

            int allIndex = repeatEndIndex - startIndex + 1;
            double rate = (double)capcity / allIndex / 2;

            double startTime = Math.Round((double)data.Date + period.StartTime, 6);
            double rptTime = data.arr_time[repeatStartIndex];
            for (int i = repeatEndIndex - 2; i >= startIndex; i--)
            {
                double time = TimeUtils.AddSeconds(rptTime, -(int)(((double)(repeatEndIndex - i)) * rate));
                data.arr_time[i] = time > startTime ? time : startTime;
            }
        }
    }


    /// <summary>
    /// 正确数据：
    /// 早9点晚9点
    ///     
    /// 一些典型数据错误：
    /// 1.时间出现偏移，如20100104的m05，出现41秒偏移。
    /// 2.开盘时间出现大量重复，收盘却提前，如20071017，提前45秒收盘
    /// 3.开盘有一个时间点莫名其妙提前了很多，如20061229，提前了快3分钟
    /// 
    /// </summary>
    public class TickDataAnalysis
    {
        public static List<TickInfo_Period> Analysis(TickData data, List<double[]> openTime)
        {
            return Analysis(data, openTime, 2);
        }

        public static List<TickInfo_Period> Analysis(TickData data, List<double[]> openTime, int countEverySecond)
        {
            List<TickInfo_Period> periods = GetPeriods(data, openTime);
            setAdjustInfo(periods, data, countEverySecond);
            return periods;
        }

        public static List<TickInfo_Period> GetPeriods(TickData data, List<double[]> openTime)
        {
            int date = data.Date;
            List<TickInfo_Period> adjustInfos = new List<TickInfo_Period>();
            int currentPeriodIndex = 0;

            TickInfo_Period period = NewPeriod(openTime, currentPeriodIndex, 0);
            adjustInfos.Add(period);

            //如果，说明第一个时间段没有
            double firstTime = data.arr_time[0];
            for (int i = 0; i < openTime.Count; i++)
            {
                if (date + openTime[i][1] < firstTime)
                {
                    currentPeriodIndex++;

                    adjustInfos[adjustInfos.Count - 1].StartIndex = -1;
                    adjustInfos[adjustInfos.Count - 1].EndIndex = -1;

                    period = NewPeriod(openTime, currentPeriodIndex, 0);
                    adjustInfos.Add(period);
                }
            }

            double periodSplit = date + GetPeriodSplit(openTime, currentPeriodIndex);
            for (int currentIndex = 1; currentIndex < data.Length; currentIndex++)
            {
                if (data.arr_time[currentIndex - 1] < periodSplit
                        && data.arr_time[currentIndex] > periodSplit)
                {
                    currentPeriodIndex++;
                    if (adjustInfos.Count != 0)
                        //设置上一个adjustinfo的结束index
                        adjustInfos[adjustInfos.Count - 1].EndIndex = currentIndex - 1;

                    period = NewPeriod(openTime, currentPeriodIndex, currentIndex);
                    adjustInfos.Add(period);

                    periodSplit = date + GetPeriodSplit(openTime, currentPeriodIndex);
                }
            }
            adjustInfos[adjustInfos.Count - 1].EndIndex = data.Length - 1;
            return adjustInfos;
        }

        private static TickInfo_Period NewPeriod(List<double[]> openTime, int currentPeriodIndex, int currentTickIndex)
        {
            TickInfo_Period period = new TickInfo_Period();
            period.StartTime = openTime[currentPeriodIndex][0];
            period.EndTime = openTime[currentPeriodIndex][1];
            period.StartIndex = currentTickIndex;
            period.PeriodIndex = currentPeriodIndex;
            return period;
        }

        private static double GetPeriodSplit(List<double[]> periods, int currentIndex)
        {
            return periods[currentIndex][1] + 0.0005;
        }

        private static void setAdjustInfo(List<TickInfo_Period> periods, TickData data, int countEverySecond)
        {
            for (int i = 0; i < periods.Count; i++)
            {
                setAdjustInfo(periods[i], data, countEverySecond);
            }
        }

        private static void setAdjustInfo(TickInfo_Period period, TickData data, int countEverySecond)
        {
            if (period.StartIndex == -1 || period.EndIndex == -1)
                return;

            setStartAdjustInfo(period, data, countEverySecond);
            setEndAdjustInfo(period, data, countEverySecond);
        }

        private static void setStartAdjustInfo(TickInfo_Period period, TickData data, int countEverySecond)
        {
            TickPeriodAdjustInfo adjustInfo = period.adjustInfo;
            int startIndex = period.StartIndex;
            if (period.StartTime == 0.09 || period.StartTime == 0.21)
            {
                startIndex++;
                adjustInfo.IsOpen = true;
            }

            int startRepeatIndex = FindStartTimeIndex(period, data, startIndex);
            if (startRepeatIndex >= 0)
            {
                int startRepeatTimes = FindStartRepeatTimes(startRepeatIndex, data);
                if (startRepeatTimes > countEverySecond)
                {
                    adjustInfo.StartRepeatIndex = startRepeatIndex;
                    adjustInfo.StartRepeatTimes = startRepeatTimes;
                    adjustInfo.StartRepeat = true;
                }
            }
            double time = Math.Round(period.StartTime + data.Date, 6);
            TimeSpan span = TimeUtils.Subtract(data.arr_time[startIndex], time);
            int timeDif = span.Minutes * 60 + span.Seconds;
            //开盘提前超过1分钟，认为该开盘时间可能有误
            if (timeDif < -60)
                adjustInfo.StartErrorData = true;
            adjustInfo.StartOffset = timeDif;
        }

        private static void setEndAdjustInfo(TickInfo_Period period, TickData data, int countEverySecond)
        {
            TickPeriodAdjustInfo adjustInfo = period.adjustInfo;
            int endIndex = period.EndIndex;

            int endRepeatIndex = FindEndTimeIndex(period, data);
            if (endRepeatIndex > 0)
            {
                int endRepeatTimes = FindEndRepeatTimes(endRepeatIndex, data);
                if (endRepeatTimes > countEverySecond)
                {
                    adjustInfo.EndRepeatIndex = endRepeatIndex;
                    adjustInfo.EndRepeatTimes = endRepeatTimes;
                    adjustInfo.EndRepeat = true;
                }
            }
            double time = Math.Round(period.EndTime + data.Date, 6);
            TimeSpan span = TimeUtils.Subtract(data.arr_time[endIndex], time);
            int timeDif = span.Minutes * 60 + span.Seconds;
            //收盘晚收超过1分钟，认为该收盘时间可能有误
            if (timeDif > 60)
                adjustInfo.EndErrorData = true;
            adjustInfo.EndOffset = timeDif;
        }

        private static int FindStartTimeIndex(TickInfo_Period period, TickData data, int startIndex)
        {
            double time = Math.Round(period.StartTime + data.Date, 6);
            int index = startIndex;
            //int index = period.StartIndex;
            double currentTime = data.arr_time[index];
            while (currentTime < time)
            {
                index++;
                currentTime = data.arr_time[index];
            }
            if (currentTime == time)
                return index;
            return -1;
        }

        private static int FindStartRepeatTimes(int startIndex, TickData data)
        {
            int repeatTimes = 1;
            double time = data.arr_time[startIndex];
            for (int i = startIndex + 1; i < data.Length; i++)
            {
                if (data.arr_time[i] == time)
                    repeatTimes++;
                else
                    break;
            }
            return repeatTimes;
        }

        private static int FindEndTimeIndex(TickInfo_Period period, TickData data)
        {
            double time = Math.Round(period.EndTime + data.Date, 6);
            int index = period.EndIndex;
            double currentTime = data.arr_time[index];
            while (currentTime > time)
            {
                index--;
                currentTime = data.arr_time[index];
            }
            if (currentTime == time)
                return index;
            return -1;
        }

        private static int FindEndRepeatTimes(int endIndex, TickData data)
        {
            int repeatTimes = 1;
            double time = data.arr_time[endIndex];
            for (int i = endIndex - 1; i > data.Length; i++)
            {
                if (data.arr_time[i] == time)
                    repeatTimes++;
                else
                    break;
            }
            return repeatTimes;
        }
    }

    public class TickInfo_Period
    {
        public int PeriodIndex;

        public double StartTime;

        public double EndTime;

        //该阶段在ticklist里的起始index
        public int StartIndex = -1;

        //该阶段在ticklist里的结束index
        public int EndIndex = -1;

        public TickPeriodAdjustInfo adjustInfo = new TickPeriodAdjustInfo();

        override
        public String ToString()
        {
            return PeriodIndex + "," + StartIndex + "," + EndIndex + "," + adjustInfo.ToString();
        }
    }

    public class TickPeriodAdjustInfo
    {
        /// <summary>
        /// 是否是开盘的周期，日盘9点和夜盘9点
        /// 开盘周期会多一个集合竞价
        /// </summary>
        public bool IsOpen;

        /// <summary>
        /// 开盘数据是否错误，提前开盘1分钟以上算错误
        /// </summary>
        public bool StartErrorData;

        /// <summary>
        /// 开盘是否出现重复数据
        /// </summary>
        public bool StartRepeat;

        /// <summary>
        /// 开盘重复数据起始位置
        /// </summary>
        public int StartRepeatIndex;

        /// <summary>
        /// 开盘重复次数
        /// </summary>
        public int StartRepeatTimes;

        /// <summary>
        /// 开盘错位的时间，提前，则为负数
        /// 如 
        /// 08:59:58 为-2
        /// 09:00:08 为8
        /// </summary>
        public int StartOffset;

        /// <summary>
        /// 开盘数据是否错误，推迟收盘1分钟以上算错误
        /// </summary>
        public bool EndErrorData;

        /// <summary>
        /// 尾盘是否重复
        /// </summary>
        public bool EndRepeat;

        /// <summary>
        /// 尾盘重复的结束index （尾盘按从后向前看）
        /// </summary>
        public int EndRepeatIndex;

        /// <summary>
        /// 尾盘重复时间次数
        /// </summary>
        public int EndRepeatTimes;

        /// <summary>
        /// 尾盘结束时间的偏移量
        /// </summary>
        public int EndOffset;

        /// <summary>
        /// 差距
        /// </summary>
        /// <returns></returns>
        public int GetTimeGap()
        {
            return StartOffset - EndOffset;
        }

        /// <summary>
        /// 该时间段是否出现了时间偏移
        /// </summary>
        /// <returns></returns>
        public bool HasTimeOffset()
        {
            return StartOffset != 0 && Math.Abs(GetTimeGap()) < 10;
        }

        public bool HasRepeatOffset()
        {
            if (StartRepeat && EndRepeat)
                return false;
            //多过10次repeat才处理偏移
            //if (StartRepeatTimes < 10 || EndRepeatTimes < 10)
            //    return false;
            int startTimeGap = -(StartRepeatTimes / 2 + EndOffset);
            bool startRepeatOffset = startTimeGap >= 0 && startTimeGap < 20;
            if (startRepeatOffset)
                return true;
            int endTimeGap = StartOffset - EndRepeatTimes / 2;
            return endTimeGap >= 0 && endTimeGap < 20;
        }

        private bool HasStartRepeatOffset()
        {
            int startTimeGap = -(StartRepeatTimes / 2 + EndOffset);
            bool startRepeatOffset = startTimeGap >= 0 && startTimeGap < 20;
            if (!startRepeatOffset)
                return false;
            //如果repeat正好能填补，或者开始重复次数大于10，可以认为需要
            if (startTimeGap < 2 || StartRepeatTimes > 15)
                return true;
            double rate = EndOffset / StartRepeatTimes;
            if (rate > 2)
                return false;
            return true;
        }

        /// <summary>
        /// 返回偏移量
        /// </summary>
        /// <returns></returns>
        public int GetTimeOffset()
        {
            return StartOffset;
        }

        /// <summary>
        /// 是否要把
        /// </summary>
        /// <returns></returns>
        public bool IsStartRepeatSpread()
        {
            return false;
        }

        public bool IsEndRepeatSpread()
        {
            return false;
        }

        override
        public String ToString()
        {
            return StartErrorData + "," + StartRepeat + "," + StartRepeatIndex + "," + StartRepeatTimes + "," + StartOffset
                + "," + EndErrorData + "," + EndRepeat + "," + EndRepeatIndex + "," + EndRepeatTimes + "," + EndOffset;
        }
    }

    /// <summary>
    /// 调整方法
    /// </summary>
    public class TickAdjustMethod
    {
        /// <summary>
        /// 该时间段是否出现了时间偏移
        /// 如果出现
        /// </summary>
        /// <returns></returns>
        public bool HasTimeOffset()
        {
            return false;
        }

        /// <summary>
        /// 返回偏移量
        /// </summary>
        /// <returns></returns>
        public double GetTimeOffset()
        {
            return -1;
        }

        /// <summary>
        /// 是否要把
        /// </summary>
        /// <returns></returns>
        public bool IsStartRepeatSpread()
        {
            return false;
        }

        public bool IsEndRepeatSpread()
        {
            return false;
        }
    }
}