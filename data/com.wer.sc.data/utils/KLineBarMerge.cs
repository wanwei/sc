using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class KLineBarMerge
    {
        public static void MergeTo(KLineBar bar1, IKLineBar bar2)
        {
            if (bar1.High < bar2.High)
                bar1.High = bar2.High;
            if (bar1.Low < bar2.Low)
                bar1.Low = bar2.Low;
            bar1.End = bar2.End;
            bar1.Mount = bar1.Mount + bar2.Mount;
            bar1.Money = bar1.Money + bar2.Money;
            bar1.Hold = bar2.Hold;
        }

        public static KLineBar Merge(IKLineBar bar1, IKLineBar bar2)
        {
            KLineBar bar = new KLineBar();
            bar.Code = bar1.Code;
            bar.Time = bar1.Time;
            bar.High = bar1.High < bar2.High ? bar2.High : bar1.High;
            bar.Low = bar1.Low < bar2.Low ? bar1.Low : bar2.Low;
            bar.End = bar2.End;
            bar.Mount = bar1.Mount + bar2.Mount;
            bar.Money = bar1.Money + bar2.Money;
            bar.Hold = bar2.Hold;
            return bar;
        }
    }
}