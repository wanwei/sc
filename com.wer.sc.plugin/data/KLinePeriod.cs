using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLinePeriod
    {
        public const int TYPE_SECOND = 0;

        public const int TYPE_MINUTE = 1;

        public const int TYPE_HOUR = 2;

        public const int TYPE_DAY = 3;

        public const int TYPE_WEEK = 4;

        public int PeriodType;

        public int Period;

        public KLinePeriod()
        {

        }

        public KLinePeriod(int periodType, int period)
        {
            this.PeriodType = periodType;
            this.Period = period;
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
    }
}