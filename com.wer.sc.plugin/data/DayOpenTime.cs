using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 日开盘时间
    /// 该类记录了开盘日早上的开盘时间
    /// </summary>
    public class DayOpenTime
    {
        public int Date;

        public double Start;

        public double End;
        public DayOpenTime()
        {

        }
        public DayOpenTime(int date, double start, double end)
        {
            this.Date = date;
            this.Start = start;
            this.End = end;
        }

        public override string ToString()
        {
            return Date + "," + Start + "," + End;
        }
    }
}
