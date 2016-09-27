using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class KLineChartMerge
    {
        public static void Merge(KLineBar chart, IKLineBar chart2)
        {
            if (chart.High < chart2.High)
                chart.High = chart2.High;
            if (chart.Low < chart2.Low)
                chart.Low = chart2.Low;
            chart.End = chart2.End;
            chart.Mount = chart.Mount + chart2.Mount;
            chart.Money = chart.Money + chart2.Money;
            chart.Hold = chart2.Hold;
        }
    }
}