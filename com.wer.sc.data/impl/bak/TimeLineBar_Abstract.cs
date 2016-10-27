using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public abstract class TimeLineBar_Abstract : ITimeLineBar
    {
        public abstract string Code { get; set; }
        public abstract int Hold { get; set; }
        public abstract int Mount { get; set; }
        public abstract float Price { get; set; }
        public abstract double Time { get; set; }
        public abstract float UpPercent { get; set; }
        public abstract float UpRange { get; set; }
    }
}
