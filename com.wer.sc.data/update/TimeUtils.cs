using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class TimeUtils
    {

        public static double AddSeconds(double time, int value)
        {
            return TimeConvert.ConvertToDoubleTime(TimeConvert.ConvertToDateTime(time).AddSeconds(value));
        }
        public static double AddMinutes(double time, int value)
        {
            return TimeConvert.ConvertToDoubleTime(TimeConvert.ConvertToDateTime(time).AddMinutes(value));
        }

        public static double AddHours(double time, int value)
        {
            return TimeConvert.ConvertToDoubleTime(TimeConvert.ConvertToDateTime(time).AddHours(value));
        }

        public static double AddDays(double time, int value)
        {
            return TimeConvert.ConvertToDoubleTime(TimeConvert.ConvertToDateTime(time).AddDays(value));
        }

        public static double AddTime(double time, int value, int timeType)
        {
            switch (timeType)
            {
                case KLinePeriod.TYPE_SECOND:
                    return AddSeconds(time, value);
                case KLinePeriod.TYPE_MINUTE:
                    return AddMinutes(time, value);
                case KLinePeriod.TYPE_HOUR:
                    return AddHours(time, value);
                case KLinePeriod.TYPE_DAY:
                    return AddDays(time, value);
            }
            return time;
        }

        public static TimeSpan Substract(double time1,double time2)
        {
            if (time1 < 1 && time2 < 1)
            {
                time1 += 20100101;
                time2 += 20100101;
            }
            DateTime t1 = TimeConvert.ConvertToDateTime(time1);
            DateTime t2 = TimeConvert.ConvertToDateTime(time2);
            return t1.Subtract(t2);
        }

        /// <summary>
        /// 根据起止时间获得一个当日k线数组
        /// </summary>
        /// <param name="openTimeList"></param>
        /// <returns></returns>
        public static List<double> GetKLineTimes(List<double[]> openTimeList, KLinePeriod period)
        {
            List<double> times = new List<double>();
            double offset = 0;
            for (int i = 0; i < openTimeList.Count; i++)
            {
                if (offset != 0)
                {
                    //两次开盘时间间隔超过4小时，则重新开始一个周期
                    if (openTimeList[i][0] - openTimeList[i - 1][1] >= 0.04)
                        offset = 0;
                }
                offset = GetTimeArr(times, openTimeList[i], period, offset);
            }
            return times;
        }

        private static double GetTimeArr(List<double> times, double[] openTime, KLinePeriod period, double offset)
        {
            double currentTime = 20100101 + openTime[0] + offset;
            double endTime = 20100101 + openTime[1];

            //看是否隔夜
            if (endTime < currentTime)
                endTime += 1;

            while (currentTime < endTime)
            {
                double time = currentTime - 20100101;
                if (time >= 1)
                    time -= 1;
                times.Add(Math.Round(time, 6));
                currentTime = AddTime(currentTime, period.Period, period.PeriodType);
            }

            return currentTime - endTime;
        }
    }
}