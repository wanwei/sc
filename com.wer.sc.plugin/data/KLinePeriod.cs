using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线周期类
    /// 如1分钟、5分钟、日线都用该类表示
    /// </summary>
    public class KLinePeriod
    {
        public const int TYPE_SECOND = 0;

        public const int TYPE_MINUTE = 1;

        public const int TYPE_HOUR = 2;

        public const int TYPE_DAY = 3;

        public const int TYPE_WEEK = 4;

        private int periodType;

        public int PeriodType
        {
            get
            {
                return periodType;
            }

            set
            {
                periodType = value;
            }
        }

        public int Period;

        public KLinePeriod()
        {

        }

        public KLinePeriod(int periodType, int period)
        {
            this.PeriodType = periodType;
            this.Period = period;
        }

        public String ToEngString()
        {
            switch (PeriodType)
            {
                case TYPE_SECOND:
                    return Period + "second";
                case TYPE_MINUTE:
                    return Period + "minute";
                case TYPE_HOUR:
                    return Period + "hour";
                case TYPE_DAY:
                    return Period + "day";
                case TYPE_WEEK:
                    return Period + "week";
            }
            return "";
        }

        public override String ToString()
        {
            switch (PeriodType)
            {
                case TYPE_SECOND:
                    return Period + "秒钟";
                case TYPE_MINUTE:
                    return Period + "分钟";
                case TYPE_HOUR:
                    return Period + "小时";
                case TYPE_DAY:
                    return Period + "天";
                case TYPE_WEEK:
                    return Period + "周";
            }
            return "";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is KLinePeriod))
                return false;
            KLinePeriod period = (KLinePeriod)obj;
            if (this.PeriodType == period.PeriodType && this.Period == period.Period)
                return true;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Period * 10 + PeriodType;
        }

        private static KLinePeriod period_5second = new KLinePeriod(KLinePeriod.TYPE_SECOND, 5);
        private static KLinePeriod period_1minute = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1);
        private static KLinePeriod period_15minute = new KLinePeriod(KLinePeriod.TYPE_MINUTE, 15);
        private static KLinePeriod period_Hour = new KLinePeriod(KLinePeriod.TYPE_HOUR, 1);
        private static KLinePeriod period_Day = new KLinePeriod(KLinePeriod.TYPE_DAY, 1);

        public static KLinePeriod KLinePeriod_5Second
        {
            get { return period_5second; }
        }

        public static KLinePeriod KLinePeriod_1Minute
        {
            get { return period_1minute; }
        }

        public static KLinePeriod KLinePeriod_15Minute
        {
            get { return period_15minute; }
        }

        public static KLinePeriod KLinePeriod_1Hour
        {
            get { return period_Hour; }
        }

        public static KLinePeriod KLinePeriod_1Day
        {
            get { return period_Day; }
        }

     
    }
}